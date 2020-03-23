using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsScript : MonoBehaviour
{
    [SerializeField] bool continuous = true;
    [SerializeField] float fadeSpeed = 1f;
    [SerializeField] float imageAliveTime = 2f;
    [SerializeField] float timeBetweenImages = 0.5f;
    [SerializeField] Image[] creditsImages;
    int imageIndex = 0;



    private void Start()
    {
        if (continuous) {
            StartCoroutine(FadeContinuous(imageAliveTime, fadeSpeed, timeBetweenImages, creditsImages));
        } else {
            StartCoroutine(FadeIn(fadeSpeed, creditsImages[0]));
        }
    }

    private void Update() {
        if (Input.GetButtonDown("Fire1") && imageIndex <= creditsImages.Length) {
            StartCoroutine(NextImage(fadeSpeed, timeBetweenImages, creditsImages[imageIndex]));
            imageIndex++;
        } else if (imageIndex > creditsImages.Length) {
            imageIndex = 0;
        }
    }

    private IEnumerator FadeContinuous(float imageAliveTime, float fadeSpeed, float timeBetweenImages, Image[] creditsImages)
    {
        for (int i = 0; i < creditsImages.Length; i++) {
            yield return FadeIn(fadeSpeed, creditsImages[i]);
            yield return new WaitForSeconds(imageAliveTime);
            yield return FadeOut(fadeSpeed, creditsImages[i]);
            yield return new WaitForSeconds(timeBetweenImages);
        }
    }    
    
    private IEnumerator NextImage(float fadeSpeed, float timeBetweenImages, Image img)
    {
        yield return FadeOut(fadeSpeed, img);
        yield return new WaitForSeconds(timeBetweenImages);
        yield return FadeIn(fadeSpeed, img);
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
