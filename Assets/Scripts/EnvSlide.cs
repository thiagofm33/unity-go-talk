using UnityEngine;

public class EnvSlide : MonoBehaviour {

    private GameObject cam;

    void Start() {
      cam = transform.Find("Camera").gameObject;
    }

    public void OnAnimationFinished() {
        FadingLayer fl = GameObject.Find("FadingLayer").GetComponent<FadingLayer>();

        GameObject.Find("Soundtrack").GetComponent<Soundtrack>().FadeOut();

        fl.FadeAndExecute(Color.white, () => {
            cam.SetActive(false);
            GunScript.showCrosshair = true;
        });
    }

}
