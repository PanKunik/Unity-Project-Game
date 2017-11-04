using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthBar;
    // public ImageConversion damageImage;
    // public AudioClip deathclip;
    // float flashSpeed = 5f;
    // public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

    Animator anim;
    // AudioSource playerAudio;
    PlayerMovement playerMovement;
    //PlayerShooting playerShooting;
    bool isDead;
    // bool damaged;

	// Use this for initialization
	void Awake () {
        anim = GetComponent<Animator>();
        // playerAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();
        currentHealth = startingHealth;
	}
	
	// Update is called once per frame
	void Update () {
		/*if( damaged )
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.detlaTime);
        }
        damaged = false;*/
	}

    public void TakeDamage( int amount )
    {
        // damaged = true;

        currentHealth -= amount;

        healthBar.value = currentHealth;

        // playerAudio.Play();

        if( currentHealth <= 0 && !isDead )
        {
            Death();
            anim.SetBool("IsDead", true);
        }
    }

    void Death()
    {
        isDead = true;

        //playerShooting.DisableEffects();

        anim.SetTrigger("Die");

        // playerAudio.clis = deathClip;
        // playerAudio.Play();

        playerMovement.enabled = false;
        // playerShooting.enabled = false;
    }
}
