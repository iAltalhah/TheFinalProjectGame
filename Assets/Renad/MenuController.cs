using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Rendering;
using System.Runtime.Serialization;
using System;

public class MenuController : MonoBehaviour
{
    public string _Scene;
    [SerializeField] private TMP_Text VolumeValue = null;
    [SerializeField] private Slider VolumeSlider = null;
    [SerializeField] private float defaultVolume= 1.0f;
    [SerializeField] private GameObject comfirmationPromp = null;

    [SerializeField] private TMP_Text ControllerSenTextValue = null;
    [SerializeField] private Slider ControllerSenSlider = null;
    [SerializeField] private int defaultSen= 4;
    public int mainControllerSen=4;
    [SerializeField] private Toggle InvertYToggle = null;

    [SerializeField] private TMP_Text BrightnessValue = null;
    [SerializeField] private Slider BrightnessVolume = null;
    [SerializeField] private float defaultBrightness= 1;

    private int _qualityLevel;
    private bool _isFullScreen;
    private float _brightnessLevel;

    public Dropdown Resolutiondropdown;
    private Resolution[] resolutions;
    void Start()
    {
        VolumeSlider.onValueChanged.AddListener(Setvolume);
        ControllerSenSlider.onValueChanged.AddListener(SetControllerSen);

        resolutions=Screen.resolutions;
        Resolutiondropdown.ClearOptions();

        List<String> options = new List<string>();
        int currentResolutionIndex=0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            String option = resolutions[i].width + " × "+resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex=i;
            }
        }

    }

    public void Play()
    {
        SceneManager.LoadScene(_Scene);
    }


    public void Exit()
    {
        Application.Quit();

    }

    public void Setvolume(float volume)
    {
        AudioListener.volume = volume;
        VolumeValue.text = volume.ToString("0.0");
    }

    public void VolumeApply()
    {
        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
        StartCoroutine(ComfirmationBox());
    }

    public void ResetButton(string MuneType)
    {
        if (MuneType == "Audio")
        {
            AudioListener.volume = defaultVolume;
            VolumeSlider.value=defaultVolume;
            VolumeValue.text = defaultVolume.ToString("0.0");
            VolumeApply();
        }
        if (MuneType == "GamePlay")
        {
            ControllerSenTextValue.text=defaultSen.ToString("0");
            ControllerSenSlider.value=defaultSen;
            mainControllerSen=defaultSen;
            InvertYToggle.isOn=false;
            GamePlayApply();


        }
    }

    public void SetControllerSen(float sensitivity)
    {
        mainControllerSen=Mathf.RoundToInt(sensitivity);
        ControllerSenTextValue.text=sensitivity.ToString("0");
    }

    public void GamePlayApply()
    {
        if (InvertYToggle.isOn)
        {
            PlayerPrefs.SetInt("masterInvertY",1);
        }
        else
        {
             PlayerPrefs.SetInt("masterInvertY",0);
        }

         PlayerPrefs.SetFloat("masterSen",mainControllerSen);
         StartCoroutine(ComfirmationBox());

    }

    public void SetBrightness(float brightness)
    {
        _brightnessLevel=brightness;
        BrightnessValue.text=brightness.ToString("0.0");
    }
    public void SetFullScreen(bool isFullScreen)
    {
        _isFullScreen=isFullScreen;

    }

    public void SetQuality(int qualityIndex)
    {
        _qualityLevel=qualityIndex;
    }

    public void GrpahicsApply()
    {
        PlayerPrefs.SetFloat("masterBrightness",_brightnessLevel);

        PlayerPrefs.SetInt("masterQuality",_qualityLevel);
        QualitySettings.SetQualityLevel(_qualityLevel);

        PlayerPrefs.SetInt("masterFullscreen",(_isFullScreen ? 1:0));
        Screen.fullScreen=_isFullScreen;

        StartCoroutine(ComfirmationBox());

    }

    public IEnumerator ComfirmationBox()
    {
        comfirmationPromp.SetActive(true);
        yield return new WaitForSeconds(2);
        comfirmationPromp.SetActive(false);

    }

}