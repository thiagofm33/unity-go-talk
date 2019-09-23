using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slide : MonoBehaviour
{

    public Graphic[] steps;

    private int currentStep;

    private Coroutine graphicFading;

    public bool StepAvailable {
        get { return graphicFading == null; }
    }

    public void OnSlideEnter() {
        currentStep = -1;

        foreach(Graphic graphic in steps)
            graphic.color = graphic.color * new Color(1,1,1,0);
    }

    public bool NextStep() {
        if(currentStep == (steps.Length - 1))
            return false;

        currentStep++;
        graphicFading = StartCoroutine(FadeGraphicIn(steps[currentStep]));

        return true;
    }

    IEnumerator FadeGraphicIn(Graphic graphic) {

        WaitForEndOfFrame wait = new WaitForEndOfFrame();

        Color newColor = graphic.color * new Color(1,1,1,0);

        graphic.color = newColor;

        float ratio = 0;

        while(ratio < 1) {
            ratio += 0.75f * Time.deltaTime;
            newColor.a = Mathf.Lerp(0f,1f,ratio);
            graphic.color = newColor;
            yield return wait;
        }

        graphicFading = null;

    }

}
