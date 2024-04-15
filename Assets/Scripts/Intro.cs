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

        introText = new string[] { "It has been 7 long years since you first decided to learn demon summoning.",
            "With just a book on demon sigils, an evil glint in your eye, and a dream, you set forth to achieve the ultimate goal: World Domination.",
            "But world domination can only start once you have mastered the art of summoning demons- and you can’t figure out how to stop summoning such cutesy, tiny, unintimidating ones.",
            "The ‘starving artist’ that you are, you decided to take demon summoning commissions with a shopfront in the meantime. Somebody’s got to pay the rent, you know?",
            "And this is all more practice for you in trying to summon that one super scary demon that will really bring the destruction of everything as we know it.",
            "Today is a new day, and your shop has customers waiting…"};
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
