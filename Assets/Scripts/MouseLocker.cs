using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLocker : MonoBehaviour {
 
  void Start() {
    Cursor.lockState = CursorLockMode.Locked;
  }

  void Update() {
    if(Input.GetKeyDown(KeyCode.Escape))
      Cursor.lockState = CursorLockMode.None;
    else if(Input.GetKeyDown(KeyCode.L))
	  Cursor.lockState = CursorLockMode.Locked;
  }

}
