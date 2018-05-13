using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    public Image damageImage;
    public AudioClip deathClip;
    public float flashSpeed = 5f;
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);
    float timer = 0f;
    Animator anim;
    PlayerMovement playerMovement;
    AudioSource playerAudio;
    PlayerShooting playerShooting;
    bool isDead = false;
    bool damaged = false;
	
    void Awake ()
    {
        anim = GetComponent <Animator> ();
        playerAudio = GetComponent <AudioSource> ();
        playerMovement = GetComponent <PlayerMovement> ();
        playerShooting = GetComponentInChildren <PlayerShooting> ();
        currentHealth = startingHealth;
    }

    void Update ()
    {
        if(damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;

		if (PlayerMovement.shieldactive) {
			timer += Time.deltaTime;
			if (timer > 10f) {
				PlayerMovement.shieldactive = false;
				timer = 0;
			}
		}
    }

    public void TakeDamage (int amount)
    {  
		if (PlayerMovement.shieldactive == false) {
			damaged = true;
			currentHealth -= amount;
			healthSlider.value = currentHealth;
			playerAudio.Play ();

			if (currentHealth <= 0 && !isDead) {
				Death ();
			}
		}
    }


    void Death ()
    {
        isDead = true;

        playerShooting.DisableEffects ();
        anim.SetTrigger ("Die");
        playerAudio.clip = deathClip;
        playerAudio.Play ();
        playerMovement.enabled = false;
        playerShooting.enabled = false;
    }


    public void RestartLevel ()
    {
        SceneManager.LoadScene (0);
    }
}
