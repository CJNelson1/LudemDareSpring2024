using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class InteriorController : MonoBehaviour
{
    public float textSpeed;
    public bool inDialogue;
    public bool isTyping;
    public int currentIndex;
    public List<string> ActiveDialogue;
    bool acceptInput;

    Vector2 lowPosition = new Vector2(775, 230);
    Vector2 highPosition = new Vector2(775, 820);

    public GameObject dialogueBox;
    public TextMeshProUGUI dialogueText;

    public GameObject blocker;
    public GameObject menu;

    List<List<string>> Dialogues;
    public Director director;
    public void Start()
    {
        director = GameObject.FindObjectOfType<Director>();
        SetDialogues();
        currentIndex = 0;
        inDialogue = false;
        isTyping = false;
        acceptInput = true;

        blocker.SetActive(true);
        //Familiar dialog when director index is 1, 6, and 11
        switch (director.currentDialogue)
        {
            case 1:
                ActiveDialogue = Dialogues[0];
                break;
            case 6:
                ActiveDialogue = Dialogues[1];
                break;
            case 11:
                ActiveDialogue = Dialogues[2];
                break;
            default:
                ActiveDialogue = null;
                acceptInput=false;
                blocker.SetActive(false);
                break;
        }
    }
    void Update()
    {
        if (acceptInput && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) && menu.activeSelf == false)
        {
            if (inDialogue)
            {
                if (isTyping)
                {
                    StopAllCoroutines();
                    isTyping = false;
                    if (currentIndex == -1) currentIndex++;
                    dialogueText.text = ActiveDialogue[currentIndex];
                }
                else
                {
                    StopAllCoroutines();
                    isTyping = true;
                    StartCoroutine(NextDialogueLine());
                }
            }
            else
            {
                if (isTyping)
                {
                    StopAllCoroutines();
                    isTyping = false;
                    dialogueText.text = ActiveDialogue[currentIndex];
                }
                else if (dialogueBox.activeSelf == true)
                {
                    DeactivateDialogueBox();
                    inDialogue = false;
                    acceptInput = false;
                }
                else
                {
                    inDialogue = true;
                    StartCoroutine(StartDialogue());
                }
            }
        }
    }

    IEnumerator NextDialogueLine()
    {
        dialogueText.text = string.Empty;
        currentIndex++;

        if (director.currentDialogue == 1 && (currentIndex >= 6 && currentIndex <= 9))
        {
            dialogueBox.transform.position = highPosition;
        }
        else
        {
            dialogueBox.transform.position = lowPosition;
        }

        if (currentIndex == ActiveDialogue.Count - 1)
        {
            inDialogue = false;
        }
        foreach (char letter in ActiveDialogue[currentIndex])
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
    }
    IEnumerator StartDialogue()
    {
        acceptInput = false;
        yield return new WaitForSeconds(0.2f);
        ActivateDialogueBox();
        acceptInput = true;
    }
    void ActivateDialogueBox()
    {
        currentIndex = -1;
        dialogueText.text = string.Empty;
        dialogueBox.SetActive(true);
        inDialogue = true;
    }
    void DeactivateDialogueBox()
    {
        dialogueBox.SetActive(false);
        blocker.SetActive(false);
    }
    void SetDialogues()
    {
        Dialogues = new List<List<string>>()
        {
            new List<string>()
            {
                "What a pair we are huh?",
                "You, begging for scraps from the riff raff of society. And me, a demon lord of the 6th circle now a lowly familiar.",
                "It's not all bad though. Now I've got a warm cot and three square a day.",
                "And while I'm here, I might as well teach you a thing or two about how to summon a proper strong demon.",
                "Remember what I taught you, before you can summon anything you need if you simply focus.",
                "None will be as powerful as me, but they will be able to help your customers with their 'problems.'",
                "Each demon has a summoning circle you must draw to complete the ritual. Some of them are trickier than others.",
                "Fortunately for you, your spell book has a comprehensive list of the circles of powerful demons, curated by yours truly.",
                "Select your book and the proper circle will appear. All you need to do is trace it with some arcane ink.",
                "And don't forget my lessons, if you want a strong demon you need to trace the circle as fast as you can and as accurately as possible.",
                "An incomplete or sloppy circle can lead to some bad results, just ask my cousin, [gutteral sounds].",
                "Now go ahead and summon up some of my pals. Who knows, if you do well enough people might start realizing how powerful you really are..."
            },
            new List<string>()
            {
                "Hey pal, you're doing alright. I knew you had the juice.",
                "Word on the streets paved with good intentions is that some of my strongest pals are rampaging around town.",
                "You know, little known fact about us demons is that our looks can be very deceiving..."
            },
            new List<string>()
            {
                "So are you figuring it out?",
                "Demons come in all shapes and sizes, but the strongest ones tend to be quite small.",
                "Most of you mortals would call us 'cute.'",
                "We prefer the term cosmically horrifying."
            }
        };
    }
    public void GoToDrawingScene()
    {
        SceneManager.LoadScene("SigilDrawing");
    }
}
