using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Rendering;
using System.Runtime.Serialization;

public class MenuController : MonoBehaviour
{
    public string _Scene;
    [SerializeField] private TMP_Text VolumeValue = null;
    [SerializeField] private Slider VolumeSlider = null;
    [SerializeField] private float defaultVolume= 1.0f;
    [SerializeField] private GameObject comfirmationPromp = null;

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
    }

    public IEnumerator ComfirmationBox()
    {
        comfirmationPromp.SetActive(true);
        yield return new WaitForSeconds(2);
        comfirmationPromp.SetActive(false);

    }

}