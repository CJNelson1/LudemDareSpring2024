using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MenuPopup : MonoBehaviour
{
    public GameObject menu;
    public GameObject menuBox;
    public GameObject creditsWindow;
    public GameObject quitPopup;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menu.activeSelf == true)
            {
                menuBox.SetActive(true);
                quitPopup.SetActive(false);
                creditsWindow.SetActive(false);
                menu.SetActive(false);
            }
            else menu.SetActive(true);
        }
    }
}
