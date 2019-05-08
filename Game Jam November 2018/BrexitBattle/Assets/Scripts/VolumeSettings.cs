using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour {
    
    bool muted = false;
    float currentVol = 0.5f;

    public Button mute;
    public Sprite speaker;
    public Sprite speakerMute;
    

    public void AdjustVolume(float musicVol)
    {
        AudioListener.volume = currentVol;

        if (currentVol <= 0.2f)
        {
            currentVol = 0.2f;
        }
    }

    public void MuteVolume()
    {
        if (!muted)
        {
            AudioListener.volume = 0;
            mute.image.sprite = speakerMute;
            muted = true;
        }
        else
        {
            AudioListener.volume = 0.5f;
            mute.image.sprite = speaker;
            muted = false;
        }
    }
}
