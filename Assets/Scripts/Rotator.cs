using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {
    public float speed = 1.2f;

    void Update() {
        transform.Rotate(new Vector3(0,0, speed * Time.deltaTime));
    }
}