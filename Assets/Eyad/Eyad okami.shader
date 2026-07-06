Shader "Custom/OkamiWorldShader"
{
    Properties
    {
        // Unity 6 Render Graph strictly uses _BlitTexture for the screen buffer
        [HideInInspector] _BlitTexture("Blit Texture", 2D) = "white" {}
        
        [Header(Edge Settings)]
        _InkColor("Ink Color", Color) = (0.05, 0.05, 0.05, 1)
        _EdgeThickness("Edge Thickness", Range(0.5, 5.0)) = 1.5
        _DepthThreshold("Silhouette Sensitivity", Range(0.001, 1.0)) = 0.05
        _NormalThreshold("Crease Sensitivity", Range(0.01, 2.0)) = 0.2
        
        [Header(Jitter Noise Settings)]
        _NoiseTex("Noise Texture (R)", 2D) = "bump" {}
        _JitterSpeed("Jitter FPS", Range(1, 24)) = 10
        _JitterStrength("Jitter Strength", Range(0.0, 0.02)) = 0.0015
        _NoiseScale("Noise Scale", Range(0.1, 5.0)) = 1.5

        [Header(Paper Texturizing)]
        _PaperTex("Paper Texture (RGB)", 2D) = "white" {}
        _PaperStrength("Paper Intensity", Range(0.0, 1.0)) = 0.35
        [KeywordEnum(Multiply, Overlay, InkBleed)] _PaperStyle("Paper Blend Mode", Float) = 0
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" "RenderPipeline" = "UniversalPipeline"}
        LOD 100
        ZTest Always ZWrite Off Cull Off

        Pass
        {
            Name "OkamiPostProcessPass"

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma shader_feature_local _PAPERSTYLE_MULTIPLY _PAPERSTYLE_OVERLAY _PAPERSTYLE_INKBLEED

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareDepthTexture.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareNormalsTexture.hlsl"

            struct Attributes
            {
                uint vertexID : SV_VertexID;
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float2 uv         : TEXCOORD0;
            };

            // Unity 6 Render Graph standard
            TEXTURE2D_X(_BlitTexture);
            SAMPLER(sampler_BlitTexture);
            float4 _BlitTexture_TexelSize;

            TEXTURE2D(_NoiseTex);
            SAMPLER(sampler_NoiseTex);
            TEXTURE2D(_PaperTex);
            SAMPLER(sampler_PaperTex);

            float4 _InkColor;
            float _EdgeThickness;
            float _DepthThreshold;
            float _NormalThreshold;
            float _JitterSpeed;
            float _JitterStrength;
            float _NoiseScale;
            float _PaperStrength;

            Varyings vert(Attributes input)
            {
                Varyings output;
                output.positionCS = GetFullScreenTriangleVertexPosition(input.vertexID);
                output.uv = GetFullScreenTriangleTexCoord(input.vertexID);
                return output;
            }

            float4 frag(Varyings input) : SV_Target
            {
                // Handles cross-platform and VR/XR setups safely
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);

                float quantizedTime = floor(_Time.y * _JitterSpeed) / _JitterSpeed;

                float2 noiseUV = input.uv * _NoiseScale + float2(quantizedTime * 0.12, quantizedTime * 0.07);
                float2 noiseOffset = SAMPLE_TEXTURE2D(_NoiseTex, sampler_NoiseTex, noiseUV).rg * 2.0 - 1.0;
                float2 distortedUV = input.uv + noiseOffset * _JitterStrength;

                // Unity 6 syntax for full screen blitting
                float3 sceneColor = SAMPLE_TEXTURE2D_X(_BlitTexture, sampler_BlitTexture, distortedUV).rgb;

                // _BlitTexture_TexelSize strictly locks the lines to 1 pixel increments
                float2 texelOffset = _BlitTexture_TexelSize.xy * _EdgeThickness;

                float rawC = SampleSceneDepth(distortedUV);
                float linearC = LinearEyeDepth(rawC, _ZBufferParams);

                if (linearC > _ProjectionParams.z - 20.0)
                {
                    return float4(sceneColor, 1.0);
                }

                float rawT = SampleSceneDepth(distortedUV + float2(0, texelOffset.y));
                float rawB = SampleSceneDepth(distortedUV + float2(0, -texelOffset.y));
                float rawL = SampleSceneDepth(distortedUV + float2(-texelOffset.x, 0));
                float rawR = SampleSceneDepth(distortedUV + float2(texelOffset.x, 0));

                float rawLaplacian = abs((rawT + rawB + rawL + rawR) - 4.0 * rawC);
                
                // Scale distance mathematically so slider works intuitively
                float silhouetteEdge = rawLaplacian * linearC * 50.0;
                float silhouetteMask = smoothstep(_DepthThreshold, _DepthThreshold + 0.05, silhouetteEdge);

                float3 normC = SampleSceneNormals(distortedUV);
                float3 normT = SampleSceneNormals(distortedUV + float2(0, texelOffset.y));
                float3 normB = SampleSceneNormals(distortedUV + float2(0, -texelOffset.y));
                float3 normL = SampleSceneNormals(distortedUV + float2(-texelOffset.x, 0));
                float3 normR = SampleSceneNormals(distortedUV + float2(texelOffset.x, 0));

                float normalEdge = length(normC - normT) + length(normC - normB) + length(normC - normL) + length(normC - normR);
                float creaseMask = smoothstep(_NormalThreshold, _NormalThreshold + 0.1, normalEdge);

                float totalEdgeMask = max(silhouetteMask, creaseMask);

                float3 paperColor = SAMPLE_TEXTURE2D(_PaperTex, sampler_PaperTex, input.uv).rgb;
                float3 finalColor = sceneColor;

                #if defined(_PAPERSTYLE_MULTIPLY)
                    finalColor = lerp(sceneColor, sceneColor * paperColor, _PaperStrength);
                #elif defined(_PAPERSTYLE_OVERLAY)
                    float3 overlayRes = lerp(2.0 * sceneColor * paperColor, 1.0 - 2.0 * (1.0 - sceneColor) * (1.0 - paperColor), step(0.5, sceneColor));
                    finalColor = lerp(sceneColor, overlayRes, _PaperStrength);
                #elif defined(_PAPERSTYLE_INKBLEED)
                    float sceneLum = dot(sceneColor, float3(0.2126, 0.7152, 0.0722));
                    float3 overlayRes = lerp(2.0 * sceneColor * paperColor, 1.0 - 2.0 * (1.0 - sceneColor) * (1.0 - paperColor), step(0.5, sceneColor));
                    float3 blendedPaper = lerp(sceneColor * paperColor, overlayRes, sceneLum);
                    finalColor = lerp(sceneColor, blendedPaper, _PaperStrength);
                #endif

                finalColor = lerp(finalColor, _InkColor.rgb, totalEdgeMask * _InkColor.a);

                return float4(finalColor, 1.0);
            }
            ENDHLSL
        }
    }
    FallBack "None"
}