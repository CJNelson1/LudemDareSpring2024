using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class Intro : MonoBehaviour
{
    public float textSpeed;
    public bool isTyping;
    public int currentLine;
    string[] introText;

    public TextMeshProUGUI textBox;
    public Button continueButton;

    bool buttonEnabled;
    void Awake()
    {
        isTyping = false;
        buttonEnabled = false;
        currentLine = 0;

        introText = new string[] { "The first line of dialogue in the intro but this also needs to be longer blah blahc blasdhfasdfasdfl;qkefdhaslkfasd",
        "The second line of dialogue should now be playing",
        "No matter what else has happened one must believe this is the third line of dialogue.",
        "Behold the dialoge goes on even farther, one wonders if it could ever end.",
        "And now we come to the end as all things must",
        "lmao asdkfajsdlkfjasd;lkfja;slkdjf;lkasdjf;lkasdjl;fasjd;lkfajsd;lkfjasdl;fjaskdfjla"};
    }

    void Update()
    {
        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) && currentLine < introText.Length)
        {
            if (isTyping)
            {
                StopAllCoroutines();
                textBox.text = introText[currentLine];
                isTyping=false;
            }
            else
            {
                currentLine++;
                StopAllCoroutines();
                if (currentLine < introText.Length)
                {
                    StartCoroutine(NextDialogueLine(introText[currentLine]));
                }
            }
        }

        if (!buttonEnabled && currentLine >= introText.Length - 1 && !isTyping) 
        { 
            StartCoroutine(EnableContinueButton());
        }
    }
    public void OnEnable()
    {
        StartCoroutine(NextDialogueLine(introText[currentLine]));
    }

    IEnumerator NextDialogueLine(string currentLine)
    {
        isTyping = true;
        textBox.text = string.Empty;
        foreach (char letter in currentLine)
        {
            textBox.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
        isTyping = false;
    }
    IEnumerator EnableContinueButton()
    {
        yield return new WaitForSeconds(1f);
        continueButton.gameObject.SetActive(true);
        buttonEnabled = true;
    }
}
