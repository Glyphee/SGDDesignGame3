using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Slider vol;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("music")) { PlayerPrefs.SetFloat("music", 1); Load(); }
        else { Load(); }
    }

    public void ChangeVolume()
    {
        AudioListener.volume = vol.value;
        Save();
    }

    void Save()
    {
        PlayerPrefs.SetFloat("music", vol.value);
    }

    void Load()
    {
        vol.value = PlayerPrefs.GetFloat("music");
    }
}
