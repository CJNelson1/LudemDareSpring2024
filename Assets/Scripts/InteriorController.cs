using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteriorController : MonoBehaviour
{
    public void GoToDrawingScene()
    {
        SceneManager.LoadScene("SigilDrawing");
    }
}
