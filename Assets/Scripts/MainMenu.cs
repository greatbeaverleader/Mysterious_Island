using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayerPressed()
    {
        SceneManager.LoadScene("Intro");
    }

    public void CreditsPressed()      
    {
        SceneManager.LoadScene("Credits");
    }

    public void ExitPeleased()
    {
        Application.Quit();
    }
}
