using System.Collections;
using UnityEngine;

public class Environment : MonoBehaviour {

  public GunInventory player;

  public Camera playerCamera;

  private FadingLayer fadingLayer;

  void Awake() {
    fadingLayer = GameObject.Find("FadingLayer").GetComponent<FadingLayer>();
  }

  void Start() {
    StartCoroutine(ChangeCameraBGColor());
  }

  IEnumerator ChangeCameraBGColor() {
    WaitForEndOfFrame wait = new WaitForEndOfFrame();

    Color startColor = playerCamera.backgroundColor;
    Color targetColor = new Color(0.15f, 0.39f, 0.53f);

    float ratio = 0;

    while(ratio < 1) {
      ratio += (0.12f * Time.deltaTime);
      playerCamera.backgroundColor = Color.Lerp(startColor, targetColor, ratio);
      yield return wait;
    }

    print("Pronto!");
  }


  void Update() {
    if(Time.timeScale < 1)
      Time.timeScale += 2.4f * Time.deltaTime;
  }

  void OnFinish() {
    player.SpawnWeapon();
  }

  void ActivateSlowMotion(){
    Time.timeScale = 0.01f;
  }

}