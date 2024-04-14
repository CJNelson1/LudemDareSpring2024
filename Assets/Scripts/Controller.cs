using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    public bool inDialogue;
    public int currentIndex;
    public List<string> ActiveDialogue;

    public GameObject dialogueBox;
    public TextMeshProUGUI dialogueText;
    public Image customerPhoto;

    public Sprite[] Customers;

    public void Start()
    {
        currentIndex = 0;
        inDialogue = false;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            if( inDialogue )
            {
                NextDialogueLine();
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
                    ActivateDialogueBox();
                    inDialogue = true;
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
    }
    void DeactivateDialogueBox()
    {
        dialogueBox.SetActive(false);
    }
    void NextDialogueLine()
    {
        currentIndex++;
        dialogueText.text = ActiveDialogue[currentIndex];
        if (currentIndex == ActiveDialogue.Count - 1)
        {
            inDialogue = false;
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
}
