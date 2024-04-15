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

    public Image background;
    public Image fakeBackground;
    bool introComplete;

    public GameObject dialogueBox;
    public TextMeshProUGUI dialogueText;
    public Image customerPhoto;

    public Sprite[] Customers;

    public GameObject menu;

    public void Start()
    {
        currentIndex = 0;
        inDialogue = false;
        background.color = new Color(1, 1, 1, 0);
        introComplete = false;
    }
    void Update()
    {
        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) && menu.activeSelf == false)
        {
            if (!introComplete)
            {
                background.color = new Color(1, 1, 1, 1);
                fakeBackground.enabled = false;
                ActivateDialogueBox();
            }
            if( inDialogue )
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
        currentIndex = 0;
        dialogueText.text = ActiveDialogue[currentIndex];
        dialogueBox.SetActive(true);
        inDialogue = true;
    }
    IEnumerator BringProtag()
    {
        for (float i = 0; i <= 1; i += Time.deltaTime)
        {
            background.color = new Color(1, 1, 1, i);
            yield return null;
        }
        fakeBackground.enabled = false;
        ActivateDialogueBox();
        introComplete = true;
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
        SetCustomerPortrait(Random.Range(0, Customers.Length));
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
}
