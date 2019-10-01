using System.Collections;
using UnityEngine;

public class SlidesController : MonoBehaviour {

    public GameObject player;

    public GameObject levelProps;

    public Slide[] slides;

    private int currentSlideIndex = 0;

    RectTransform rt;

    int minX;

    Coroutine sliding;

    public bool isSliding {
      get { return (sliding != null); }
    }

    public Slide CurrentSlide {
      get { return slides[currentSlideIndex]; }
    }

    void Awake() {
      rt = transform.Find("PresentationContainer").GetComponent<RectTransform>();
      minX = slides.Length * -800;
    }

    void Start() {
      CurrentSlide.OnSlideEnter();
    }

    public void Slide(int dir) {
      if(sliding != null || (dir == 1 && rt.anchoredPosition.x >= 0))
        return;

      sliding = StartCoroutine(DoSlide(dir));
    }

    IEnumerator DoSlide(int dir) {
      Slide previousSlide = slides[currentSlideIndex];

      currentSlideIndex += (dir*-1);

      if(currentSlideIndex < slides.Length)
        slides[currentSlideIndex].OnSlideEnter();
      else
        this.enabled = false;

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

      if(rt.anchoredPosition.x <= minX && transform.parent.name == "SlidesCamera") {
        player.SetActive(true);
        levelProps.SetActive(true);
        transform.parent.gameObject.SetActive(false);
      }

      previousSlide.OnSlideExit();
      sliding = null;
    }

}
