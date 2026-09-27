using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    [SerializeField] private string menuScene = "MainMenu";

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene(menuScene);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(menuScene);
    }
}