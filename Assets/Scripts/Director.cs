using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Director : MonoBehaviour
{

    public AudioSource BGMChannel;
    public AudioSource VFXChannel;

    public float masterVolume;
    public float BGMvolume;
    public float VFXvolume;
    void Awake()
    {
        DontDestroyOnLoad(this);
        DontDestroyOnLoad(BGMChannel);
        DontDestroyOnLoad(VFXChannel);

        masterVolume = 0.5f;
        BGMvolume = 0.5f;
        VFXvolume = 0.5f;

        UpdateVolumes();
    }

    public void UpdateVolumes()
    {
        BGMChannel.volume = masterVolume * BGMvolume;
        VFXChannel.volume = masterVolume * VFXvolume;
    }
}
