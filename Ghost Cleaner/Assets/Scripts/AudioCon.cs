using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCon : MonoBehaviour
{
    public static AudioCon sfx;

    [Header("Sound Clips")]
    [SerializeField] AudioClip possess;
    [SerializeField] AudioClip unpossess;
    [SerializeField] AudioClip tooFar;
    [SerializeField] AudioClip coin;


    void Awake()
    {
        if (sfx == null)
        {
            sfx = this;
        }
        else if (sfx != null)
        {
            Destroy(this.gameObject);
        }
    }

    public void PlayPossess()
    {
        GetComponent<AudioSource>().PlayOneShot(possess);
    }

    public void PlayUnpossess()
    {
        GetComponent<AudioSource>().PlayOneShot(unpossess);
    }

    public void PlayTooFar()
    {
        GetComponent<AudioSource>().PlayOneShot(tooFar);
    }

    public void PlayCoinGet()
    {
        GetComponent<AudioSource>().PlayOneShot(coin);
    }
}
