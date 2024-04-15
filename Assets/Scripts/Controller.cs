using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting.Antlr3.Runtime;

public class Controller : MonoBehaviour
{
    public float textSpeed;
    public bool inDialogue;
    public int currentIndex;
    public List<string> ActiveDialogue;
    int activeDialogueIndex;

    public Image background;
    public Image fakeBackground;
    bool acceptInput;

    public GameObject dialogueBox;
    public TextMeshProUGUI dialogueText;
    public Image customerPhoto;

    public Sprite[] Customers;

    public GameObject menu;

    List<List<string>> Dialogues;
    public Director director;

    public void Start()
    {
        director = GameObject.FindObjectOfType<Director>();
        SetDialogues();
        currentIndex = 0;
        inDialogue = false;
        background.color = new Color(1, 1, 1, 0);
        acceptInput = true;

        activeDialogueIndex = director.GetActiveDialogueIndex();
        if (director.GetActiveDialogueIndex() >= 15)
        {
            //End game
        }
    }
    void Update()
    {
        if (acceptInput && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) && menu.activeSelf == false)
        {
            if ( inDialogue )
            {
                StopAllCoroutines();
                StartCoroutine(NextDialogueLine());
            }
            else
            {

                if (dialogueBox.activeSelf == true)
                {
                    DeactivateDialogueBox();
                    inDialogue = false;
                    StartCoroutine(SceneChange());
                }
                else
                {
                    inDialogue = true;
                    StartCoroutine(BringProtag());
                }
            }
        }
    }
    void SetCustomerPortrait(int index)
    {
        customerPhoto.sprite = Customers[index];
    }
    void ActivateDialogueBox()
    {
        ChooseActiveDialogue();
        currentIndex = -1;
        dialogueText.text = string.Empty;
        dialogueBox.SetActive(true);
        inDialogue = true;
    }
    IEnumerator BringProtag()
    {
        acceptInput = false;
        for (float i = 0; i <= 1; i += Time.deltaTime)
        {
            background.color = new Color(1, 1, 1, i);
            yield return null;
        }
        fakeBackground.enabled = false;
        yield return new WaitForSeconds(1f);
        ActivateDialogueBox();
        acceptInput = true;
    }
    void DeactivateDialogueBox()
    {
        dialogueBox.SetActive(false);
    }
    IEnumerator NextDialogueLine()
    {
        dialogueText.text = string.Empty;
        currentIndex++;
        if (currentIndex == ActiveDialogue.Count - 1)
        {
            inDialogue = false;
        }
        foreach(char letter in ActiveDialogue[currentIndex])
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
    }
    void ChooseActiveDialogue()
    {
        ActiveDialogue = Dialogues[activeDialogueIndex];

        if (activeDialogueIndex >= 0 && activeDialogueIndex <= 2)
        {
            SetCustomerPortrait(0);
        }
        else if (activeDialogueIndex >= 3 && activeDialogueIndex <= 5)
        {
            SetCustomerPortrait(1);
        }
        else if (activeDialogueIndex >= 6 && activeDialogueIndex <= 8)
        {
            SetCustomerPortrait(2);
        }
        else if (activeDialogueIndex >= 9 && activeDialogueIndex <= 11)
        {
            SetCustomerPortrait(3);
        }
        else if (activeDialogueIndex >= 12 && activeDialogueIndex <= 14)
        {
            SetCustomerPortrait(4);
        }
        else
        {
            SetCustomerPortrait(0);
        }

    }
    IEnumerator SceneChange()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Interior");
    }
    public void QuitGame()
    {
        print("Quitting game");
        Application.Quit();
    }

    // All the text entries
    void SetDialogues()
    {
        // First 3 dialogs are little boy, next 3 are pretty lady, next 3 are business man, next 3 are bearded man, last 3 are old lady
        Dialogues = new List<List<string>>(){
            new List<string>(){
                "Hi mister, er um, misses? I've been having some trouble at school.",
                "There's this really mean kid who keeps shovin' me around and makin' fun of me.",
                "Is there any way you can help me?"
            },
            new List<string>(){
                "Hello sir or madam.",
                "I've been having a real tough time in school lately, and was wondering if you had something that could help me study?"
            },
            new List<string>(){
                "I got all A's on my latest report card!",
                "And my mom said we can do something special, but I don't really have any friends to bring...",
                "Could you give me someone to come along?"
            },
            new List<string>(){
                "I was wondering if you could help me?",
                "I'm flat broke and I need to feed my poor kitties, they havn't eaten in a couple days!",
                "Oh kitties, mommas gonna figure it out..."
            },
            new List<string>(){
                "I'm just trying to live my life, and my neighbors keep filing noise complaints.",
                "It's really pissing me off, can you help me get them off my back?"
            },
            new List<string>(){
                "Heyyy",
                "I was wondering if you could help me out. I've been seeing this guy, and its really not working out.",
                "What can you do for me?"
            },
            new List<string>(){
                "Hello. I was wondering if you could help me out.",
                "I have a date tonight and I'd really like to impress her.",
                "Think you've got something that could cook a great meal for me?"
            },
            new List<string>(){
                "So, your help earlier went really well, and I've got another date coming up.",
                "Do you have something that can help me really impress her?"
            },
            new List<string>(){
                "I got dumped.",
                "I thought it was going so well and then...BAM! it's over.",
                "Do you have something that can help me forget and move on?"
            },
            new List<string>(){
                "My grandmother just passed away and she was a real hoarder.",
                "I was wondering if you could help me clean up all the junk?"
            },
            new List<string>(){
                "I've been trying to clear out the woods around my house.",
                "Lemme tell ya, I am not winning that fight.",
                "Do you have something that packs some punch?"
            },
            new List<string>(){
                "So the house is looking great, but it doesn't quite feel like a home.",
                "Do you have anything that might specialize in interior design?"
            },
            new List<string>(){
                "I'm looking for some help with my yard, these old bones can't keep up with nature anymore.",
                "It's just a mess these days and the HOA is starting to get real nasty.",
                "Think you've got somethin' for me?"
            },
            new List<string>(){
                "Hello young whipper-snapper, I was wondering if you could help me out?",
                "I've got an important appointment coming up and my car decided to break down.",
                "I don't know how I'm going to get there."
            },
            new List<string>(){
                "It's me and my husbands 63rd anniversary coming up!",
                "I want to do something special for him...",
                "What kind of spice have you got in the back?"
            },
        };
    }
}
