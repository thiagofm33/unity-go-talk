using System.Collections;
using UnityEngine;

public class Environment : MonoBehaviour {

  public GunInventory player;
  public GameObject dummies;

  void OnFinish() {
    player.SpawnWeapon();
    dummies.SetActive(true);
  }

}
