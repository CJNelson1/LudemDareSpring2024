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

    // sigil/demon id, best Score achieved for summoning this demon
    public Dictionary<int, Score> demondex;
    public int[] sigils;
    public int activeSigil;
    void Awake()
    {
        DontDestroyOnLoad(this);
        DontDestroyOnLoad(BGMChannel);
        DontDestroyOnLoad(VFXChannel);

        masterVolume = 0.5f;
        BGMvolume = 0.5f;
        VFXvolume = 0.5f;

        UpdateVolumes();

        demondex = new()
        {
            {1, Score.notUnlocked},
            {2, Score.notUnlocked},
            {3, Score.notUnlocked},
            {4, Score.notUnlocked},
            {5, Score.notUnlocked},
            {6, Score.notUnlocked},
            {7, Score.notUnlocked},
            {8, Score.notUnlocked},
            {9, Score.notUnlocked},
            {10, Score.notUnlocked},
            {11, Score.notUnlocked},
            {12, Score.notUnlocked},
            {13, Score.notUnlocked},
            {14, Score.notUnlocked},
            {15, Score.notUnlocked}
        };
    }

    public void UpdateVolumes()
    {
        BGMChannel.volume = masterVolume * BGMvolume;
        VFXChannel.volume = masterVolume * VFXvolume;
    }
}
