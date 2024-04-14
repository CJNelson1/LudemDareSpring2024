using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public Slider masterVolume;
    public Slider BGMVolume;
    public Slider VFXVolume;

    public Director Director;
    public void Awake()
    {
        Director = GameObject.Find("Director").GetComponent<Director>();
        masterVolume.value = Director.masterVolume;
        BGMVolume.value = Director.BGMvolume;
        VFXVolume.value = Director.VFXvolume;
    }
    public void NewVolumes()
    {
        Director.masterVolume = masterVolume.value;
        Director.BGMvolume = BGMVolume.value;
        Director.VFXvolume = VFXVolume.value;

        Director.UpdateVolumes();
    }
}
