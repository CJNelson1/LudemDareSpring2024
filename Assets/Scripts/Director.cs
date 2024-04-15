using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Director : MonoBehaviour
{

    public AudioSource BGMChannel;
    public AudioSource VFXChannel;
    void Awake()
    {
        DontDestroyOnLoad(this);
        DontDestroyOnLoad(BGMChannel);
        DontDestroyOnLoad(VFXChannel);
    }
}
