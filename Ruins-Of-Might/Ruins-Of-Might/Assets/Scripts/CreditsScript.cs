using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreditsScript : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]
    [Tooltip("Name of the next scene to play.")]
    string nextScene = "";

    [SerializeField] float fadeSpeed = 1f;
    [SerializeField] float imageAliveTime = 2f;
    [SerializeField] float timeBetweenImages = 0.5f;

    [Header("Slide Show Content")]
    [SerializeField] Image[] creditsImages;
    int imageIndex = 0;



    private void Start() {
            StartCoroutine(FadeContinuous(imageAliveTime, fadeSpeed, timeBetweenImages, creditsImages));
    }

    private IEnumerator FadeContinuous(float imageAliveTime, float fadeSpeed, float timeBetweenImages, Image[] creditsImages)
    {
        for (int i = 0; i < creditsImages.Length; i++) {
            yield return FadeIn(fadeSpeed, creditsImages[i]);
            yield return new WaitForSeconds(imageAliveTime);
            yield return FadeOut(fadeSpeed, creditsImages[i]);
            yield return new WaitForSeconds(timeBetweenImages);
        }
        SceneManager.LoadScene(nextScene);
    }

    private IEnumerator FadeIn(float fadeSpeed, Image img)
    {
        img.color = new Color(img.color.r, img.color.g, img.color.b, 0f);
        while (img.color.a < 1.0f)
        {
            img.color = new Color(img.color.r, img.color.g, img.color.b, img.color.a + (fadeSpeed * Time.deltaTime));
            yield return null;
        }
    }
    private IEnumerator FadeOut(float fadeSpeed, Image img)
    {
        img.color = new Color(img.color.r, img.color.g, img.color.b, 1f);
        while (img.color.a > 0.0f)
        {
            img.color = new Color(img.color.r, img.color.g, img.color.b, img.color.a - (fadeSpeed * Time.deltaTime));
            yield return null;
        }
    }
}
