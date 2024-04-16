using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Director : MonoBehaviour
{
    // Audio stuff
    public float masterVolume;
    public float BGMvolume;
    public float VFXvolume;
    public AudioSource BGMChannel;
    public AudioSource VFXChannel;
    // BGM
    public AudioClip titleScreenMusic;
    public AudioClip storefrontMusic;
    public AudioClip interiorMusic;
    public AudioClip sigilMusic;
    // VFX
    public AudioClip vfx1;
    public AudioClip vfx2;
    public AudioClip vfx3;
    public AudioClip vfx4;
    public delegate void VFXEventHandler(string soundName); // function pointer, you can invoke vfx in any other script using Director.playVFX?.Invoke("vfxName");
    public static event VFXEventHandler playVFX;

    //Converstion progression
    [SerializeField]
    List<int> DialogueOrder;
    public int currentDialogue;

    public List<int> SigilOrder;
    public int currentSigil;

    // sigil/demon id, best Score achieved for summoning this demon
    public Dictionary<int, Score> demondex;
    public Score latestScore;
    public int monsterIndex;

    void Awake()
    {
        DontDestroyOnLoad(this);
        DontDestroyOnLoad(BGMChannel);
        DontDestroyOnLoad(VFXChannel);

        // pub/sub where PlayMusic invokes every time the a new scene is loaded
        SceneManager.sceneLoaded += PlayMusic;
        Director.playVFX += PlayVFX;

        UpdateVolumes();

        CreateDialogueOrder();
        CreateSigilOrder();

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

    public void PlayMusic(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "TitleScreen":
                PlayMusicHelper(titleScreenMusic);
                break;
            case "Storefront":
                PlayMusicHelper(storefrontMusic);
                break;
            case "Interior":
                PlayMusicHelper(interiorMusic);
                break;
            case "SigilDrawing":
                PlayMusicHelper(sigilMusic);
                break;
        }
    }

    public void PlayMusicHelper(AudioClip musicClip)
    {
        BGMChannel.Stop();
        BGMChannel.clip = musicClip;
        BGMChannel.Play();
    }

    void PlayVFX(string vfxName)
    {
        switch (vfxName)
        {
            case "TODO 1":
                PlayVFXHelper(vfx1);
                break;
            case "TODO 2":
                PlayVFXHelper(vfx2);
                break;
            case "TODO 3":
                PlayVFXHelper(vfx3);
                break;
            case "TODO 4":
                PlayVFXHelper(vfx4);
                break;
        }
    }

    void PlayVFXHelper(AudioClip vfxClip)
    {
        VFXChannel.Stop();
        VFXChannel.clip = vfxClip;
        VFXChannel.Play();
    }

    void CreateDialogueOrder()
    {
        currentDialogue = 0;

        DialogueOrder = new List<int>();
        List<int> firstThird = new List<int>() { 0, 3, 6, 9, 12 };
        List<int> secondThird = new List<int>() { 1, 4, 7, 10, 13 };
        List<int> lastThird = new List<int>() { 2, 5, 8, 11, 14 };

        firstThird = firstThird.OrderBy(i => Random.value).ToList();
        secondThird = secondThird.OrderBy(i => Random.value).ToList();
        lastThird = lastThird.OrderBy(i => Random.value).ToList();

        DialogueOrder.AddRange(firstThird);
        DialogueOrder.AddRange(secondThird);
        DialogueOrder.AddRange(lastThird);
    }
    public int GetActiveDialogueIndex()
    {
        int index = currentDialogue;
        currentDialogue++;

        return DialogueOrder[currentDialogue];
    }
    void CreateSigilOrder()
    {
        currentSigil = 0;
        monsterIndex = 0;

        SigilOrder = new List<int>();
        List<int> firstThird = new List<int>() { 0, 1, 2, 3, 4 };
        List<int> secondThird = new List<int>() { 5, 6, 7, 8, 9 };
        List<int> lastThird = new List<int>() { 10, 11, 12, 13, 14 };

        firstThird = firstThird.OrderBy(i => Random.value).ToList();
        secondThird = secondThird.OrderBy(i => Random.value).ToList();
        lastThird = lastThird.OrderBy(i => Random.value).ToList();

        SigilOrder.AddRange(firstThird);
        SigilOrder.AddRange(secondThird);
        SigilOrder.AddRange(lastThird);
    }
    public int GetActiveSigilIndex()
    {
        int index = currentSigil;
        monsterIndex = currentSigil;
        currentSigil++;
        return SigilOrder[index];
    }
}
