using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidesInputController : MonoBehaviour {

    public SlidesController currentSlidesController;

    void Update() {
      if(!currentSlidesController.enabled)
        return;

      if(Input.GetAxis("L2") > 0)
          currentSlidesController.Slide(1);

      if(Input.GetAxis("R2") > 0 && currentSlidesController.CurrentSlide.StepAvailable) {
        if(!currentSlidesController.CurrentSlide.NextStep())
          currentSlidesController.Slide(-1);
      }
    }

    public void SetCurrentSlidesController(SlidesController instance) {
      currentSlidesController = instance;
    }

}
