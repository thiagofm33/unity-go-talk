using System.Collections;
using UnityEngine;

public class Environment : MonoBehaviour {

  public GunInventory player;

  private FadingLayer fadingLayer;

  void Awake() {
    fadingLayer = GameObject.Find("FadingLayer").GetComponent<FadingLayer>();
  }

  void OnFinish() {
    player.SpawnWeapon();
  }

}