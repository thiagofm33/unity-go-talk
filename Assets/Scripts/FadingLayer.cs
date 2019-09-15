using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadingLayer : MonoBehaviour {

    private Image fadingImage;

    void Awake() {
      fadingImage = GetComponent<Image>();
    }

    public void FadeIn() {
        FadeIn(Color.black);
    }

    public void FadeIn(Color colorFrom) {
        Fade(colorFrom, 1);
    }

    public void FadeOut(Color colorTo) {
        Fade(colorTo, 0);
    }

    public void FadeOut() {
        FadeOut(Color.black);
    }

    public void Fade(int from) {
        Fade(Color.black, from);
    }

    public void Fade(Color color, int alphaFrom) {
        StartCoroutine(PerformFade(color, alphaFrom));
    }

    private IEnumerator PerformFade(Color color, int alphaFrom) {

        int alphaTo = 1 - alphaFrom;
        int currentAlpha = alphaFrom;
        float ratio = 0;

        color.a = currentAlpha;
        fadingImage.color = color;

        WaitForEndOfFrame wait = new WaitForEndOfFrame();

        while(ratio < 1) {
            ratio += 3 * Time.deltaTime;
            color.a = Mathf.Lerp(alphaFrom, alphaTo, ratio);
            fadingImage.color = color;
            yield return wait;
        }

        color.a = alphaTo;
        fadingImage.color = color;

    }

    public void FadeAndExecute(System.Action handler){
        FadeAndExecute(Color.black, handler);
    }

    public void FadeAndExecute(Color color, System.Action handler){
        StartCoroutine(PerformFadeAndExecute(color, handler));
    }

    private IEnumerator PerformFadeAndExecute(Color color, System.Action handler){
        yield return PerformFade(color, 0);
        handler();
        yield return PerformFade(color, 1);
    }

}
