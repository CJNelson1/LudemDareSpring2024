using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScreen : MonoBehaviour
{
    public void StartGame()
    {
        print("Starting game");
        SceneManager.LoadScene("Storefront");
    }
    public void QuitGame()
    {
        print("Quitting game");
        Application.Quit();
    }
}
