using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

    public GameObject deathParticles;

    public Slider healthBar;

    public new string name;

    [TextArea]
    public string speech;

    public bool introduced = true;

    public Texture trueFinalBossTexture;

    private float health;

    private Transform player;

    private GameObject speechBubble;

    private Text nameText;

    private Text speechText;

    private Animator anim;

    void Start() {
      player = GameObject.FindWithTag("Player").transform;
      anim = GetComponent<Animator>();

      if(healthBar != null){
        health = 1f;
        healthBar.gameObject.SetActive(true);
        healthBar.value = 1;

        nameText = healthBar.transform.Find("BossName").GetComponent<Text>();
        nameText.text = name;

        speechBubble = GameObject.Find("Overlay").transform.Find("SpeechBubble").gameObject;

        speechText = speechBubble.transform.Find("SpeechBubbleText").GetComponent<Text>();
        speechText.text = speech;
      }
      else
        health = 0.21f;
    }

    void Update() {
        if(!introduced)
          return;

        Vector3 playerPos = player.position;
        playerPos.y = transform.position.y;

        Vector3 newPos = Vector3.MoveTowards(transform.position, playerPos, 3 * Time.deltaTime);

        transform.position = newPos;
        transform.rotation = Quaternion.LookRotation(playerPos - transform.position);
    }

    public void TakeDamage() {
      health -= 0.03f;

      if(healthBar != null)
        healthBar.value = health;

      if(health <= 0)
        Die();
    }

    private void Die() {
      if(healthBar != null) { // It's a Boss
        healthBar.gameObject.SetActive(false);
        EnemyWave.current.Invoke("ShowEnvSlide", 1.5f);
      }

      GameObject.Find("Environment").GetComponent<Environment>().Invoke("ActivateSlowMotion", 0.15f);
      GameObject.Destroy(Instantiate(deathParticles, transform.position, transform.rotation), 3);
      EnemyWave.current.UpdateWeakEnemiesCount();
      Destroy(gameObject);
    }

    public void ShowSpeechBubble() {
      speechBubble.SetActive(true);
    }

    public void OnIntroductionFinished() {
      if(name == "The Final Boss") {
        anim.Play("EnemyTrueFace");
        return;
      }

      if(name == "The True Final Boss")
        transform.Find("Head").GetComponentInChildren<Renderer>().material.mainTexture = trueFinalBossTexture;

      FadingLayer fl = GameObject.Find("FadingLayer").GetComponent<FadingLayer>();

      GameObject.Find("Soundtrack").GetComponent<Soundtrack>().PlayBossSoundtrack();

      fl.FadeAndExecute(Color.white, () => {
        transform.Find("Camera").gameObject.SetActive(false);
        speechBubble.SetActive(false);
        introduced = true;
        anim.Play("EnemyWalking");
        GunScript.showCrosshair = true;
      });
    }

    public void UnveilTrueFinalBoss() {
      name = "The True Final Boss";
      nameText.text = name;
      speechText.text = "Quem ousa incomodar aquele que ocupa o topo da pirâmide?";
    }

}
