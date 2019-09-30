using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Slide : MonoBehaviour {

    public Graphic[] steps;

    private VideoPlayer video;
    private int currentStep;
    private Coroutine graphicFading;

    public bool StepAvailable {
        get { return graphicFading == null; }
    }

    void Start() {
        Transform videoTransform = transform.Find("Video");
        if(videoTransform != null)
            video = videoTransform.GetComponent<VideoPlayer>();
    }

    void Update() {
        if(video != null) {
            if(Input.GetKeyDown(KeyCode.Joystick1Button0)) {
                if(video.isPlaying)
                    video.Pause();
                else
                    video.Play();
            }
        }
    }

    public void OnSlideEnter() {
        currentStep = -1;

        if(video != null)
            video.enabled = true;

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
            ratio += 1.8f * Time.deltaTime;
            newColor.a = Mathf.Lerp(0f,1f,ratio);
            graphic.color = newColor;
            yield return wait;
        }

        graphicFading = null;

    }

}
