using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {

    public int startingHealth = 100;
    public int currentHealth;
    //public Slider healthSlider;
    public Image damageImage;
    public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

    public Text healthText;

    Animator anim;
    AudioSource playerAudio;
    PlayerMovement playerMovement;
    PlayerShooting playerShooting;
    bool isDead;
    bool damaged;

    void Awake () {

        anim = GetComponent <Animator>();
        playerAudio = GetComponent <AudioSource>();
        playerMovement = GetComponent <PlayerMovement>();
        playerShooting = GetComponentInChildren <PlayerShooting>();
        currentHealth = startingHealth;

    }

    void Update () {

        damageImage.color = damaged ? flashColour : Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);

        //if (damaged)
        //{
        //    damageImage.color = flashColour;
        //}
        //else
        //{
        //    damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        //}

        damaged = false;

    }

    public void TakeDamage (int amount) {

        damaged = true;
        currentHealth -= amount;
        //healthSlider.value = currentHealth;
        healthText.text = currentHealth.ToString();
        healthText.color = (currentHealth <= 70 && currentHealth > 30) ? new Color(1f, 0.6f, 0f) : (currentHealth <= 30 ? Color.red : new Color(0f, 1f, 0f));
        playerAudio.Play();

        if(currentHealth <= 0 && !isDead) {

            Death();

        }

    }

    void Death () {

        isDead = true;

        playerShooting.DisableEffects ();

        anim.SetTrigger ("Die");

        playerAudio.clip = deathClip;
        playerAudio.Play ();

        playerMovement.enabled = false;
        playerShooting.enabled = false;

    }

    public void RestartLevel () {

        SceneManager.LoadScene(0);

    }

}
