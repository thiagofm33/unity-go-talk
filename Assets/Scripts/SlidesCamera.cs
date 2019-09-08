using System.Collections;
using UnityEngine;

public class SlidesCamera : MonoBehaviour {

    public GameObject player;

    public GameObject levelProps;

    RectTransform rt;

    int minX;

    Coroutine sliding;

    void Awake() {
      rt = transform.Find("Canvas/PresentationContainer").GetComponent<RectTransform>();
      minX = (GameObject.FindGameObjectsWithTag("Slide").Length) * -800;
    }

    private void Slide(int dir) {
      if(sliding != null || (dir == 1 && rt.anchoredPosition.x >= 0))
        return;

      sliding = StartCoroutine(DoSlide(dir));
    }

    void Update() {
      if(Input.GetAxis("L2") > 0) Slide(1);
      if(Input.GetAxis("R2") > 0) Slide(-1);
    }

    IEnumerator DoSlide(int dir) {
      WaitForEndOfFrame wait = new WaitForEndOfFrame();

      float originX = rt.anchoredPosition.x;
      float targetX = rt.anchoredPosition.x + (800 * dir);
      float ratio = 0;

      float currentX = originX;

      while(ratio < 1) {
          ratio += 6 * Time.deltaTime;
          currentX = Mathf.Lerp(originX, targetX, ratio);
          rt.anchoredPosition = new Vector2(currentX, rt.anchoredPosition.y);
          yield return wait;
      }

      if(rt.anchoredPosition.x <= minX) {
        player.SetActive(true);
        levelProps.SetActive(true);
        gameObject.SetActive(false);
      }

      sliding = null;
    }

}
