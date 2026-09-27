using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SlideShow : MonoBehaviour
{
    [Header("Слайды")]
    [SerializeField] private Sprite[] slides;
    [SerializeField] private string[] texts;
    

    [Header("Настройки")]
    [SerializeField] private Image slideImage;
    [SerializeField] private TMP_Text slideText;
    [SerializeField] private float slideDuration = 3f;   
    [SerializeField] private string nextScene = "SampleScene";  

    [Header("Пропуск")]
    [SerializeField] private KeyCode skipKey = KeyCode.Space;  
    [SerializeField] private bool allowSkip = true;

    private void Start()
    {
        StartCoroutine(PlaySlides());
    }

    private void Update()
    {
        if (allowSkip && Input.GetKeyDown(skipKey))
        {
            StopAllCoroutines();
            SceneManager.LoadScene(nextScene);
        }
    }

    private IEnumerator PlaySlides()
    {
        for (int i = 0; i < slides.Length; i++)
        {
            slideImage.sprite = slides[i];
            slideText.text = i < texts.Length ? texts[i] : "";
            yield return new WaitForSeconds(slideDuration);
            
        }

        SceneManager.LoadScene(nextScene);
    }
}