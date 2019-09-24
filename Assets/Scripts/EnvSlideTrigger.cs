using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvSlideTrigger : MonoBehaviour {

    public SlidesController slidesController;

    private Transform playerGunCamera;

    private Coroutine rotatingPlayerGun;

    private SlidesInputController sic;

    void Start() {
        sic = GameObject.Find("SlidesInputController").GetComponent<SlidesInputController>();
        playerGunCamera = GameObject.FindWithTag("Player").transform.Find("Main Camera/Camera");
    }

    void OnTriggerEnter(Collider other) {
        sic.SetCurrentSlidesController(slidesController);

        if(other.CompareTag("Player")) {
            if(rotatingPlayerGun != null)
              StopCoroutine(rotatingPlayerGun);

            rotatingPlayerGun = StartCoroutine(RotatePlayerGun(-24f));

            GunScript.showCrosshair = false;
        }
    }

    void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player")) {
            if(rotatingPlayerGun != null)
              StopCoroutine(rotatingPlayerGun);

            rotatingPlayerGun = StartCoroutine(RotatePlayerGun());

            GunScript.showCrosshair = true;
        }
    }

    private IEnumerator RotatePlayerGun(float targetX = 0) {

        WaitForEndOfFrame wait = new WaitForEndOfFrame();

        float ratio = 0;

        float startX = (targetX == 0)? -24f : 0;
        float currentX = startX;

        while(ratio < 1) {
            ratio += 7.5f * Time.deltaTime;
            currentX = Mathf.Lerp(startX, targetX, ratio);
            playerGunCamera.localRotation = Quaternion.Euler(currentX, 0, 0);

            yield return wait;
        }

        rotatingPlayerGun = null;

    }

}
