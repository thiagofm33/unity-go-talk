using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWave : MonoBehaviour
{

    public GameObject[] weakEnemies;

    public GameObject boss;

    public static EnemyWave current;

    public GameObject envSlide;

    private int weakEnemiesCount;

    void Start() {
        weakEnemiesCount = weakEnemies.Length;
    }

    void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")) {
            ActivateWeakEnemies();
            current = this;
            gameObject.GetComponent<Collider>().enabled = false;
        }
    }

    public void UpdateWeakEnemiesCount() {
        if(weakEnemiesCount <= 0)
            return;

        weakEnemiesCount -= 1;

        if(weakEnemiesCount <= 0) {
            GameObject.Find("Soundtrack").GetComponent<Soundtrack>().FadeOut(0.09f);
            Invoke("ActivateBoss", 1.5f);
        }
    }

    private void ActivateWeakEnemies() {
        GameObject.Find("Soundtrack").GetComponent<Soundtrack>().PlayEnemySoundtrack();

        foreach(GameObject enemy in weakEnemies)
            enemy.SetActive(true);
    }

    private void ActivateBoss() {
        FadingLayer fl = GameObject.Find("FadingLayer").GetComponent<FadingLayer>();

        fl.FadeAndExecute(() => {
            boss.SetActive(true);
            boss.GetComponent<Animator>().Play("EnemyIntroduction");
            GunScript.showCrosshair = false;
        });
    }

    private void ShowEnvSlide() {
        FadingLayer fl = GameObject.Find("FadingLayer").GetComponent<FadingLayer>();

        fl.FadeAndExecute(() => {
            envSlide.SetActive(true);
            GunScript.showCrosshair = false;
        });
    }

}
