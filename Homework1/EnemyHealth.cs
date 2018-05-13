using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
	public int startingHealth = 100;         
	public int currentHealth;                
	public float sinkSpeed = 2.5f;           
	public int scoreValue = 10;               
	public AudioClip deathClip;              // The sound to play when the enemy dies.

	Animator anim;                              // Reference to the animator.
	AudioSource enemyAudio;              // Reference to the audio source.
	ParticleSystem hitParticles;             // Reference to the particle system that plays when the enemy is damaged.
	CapsuleCollider capsuleCollider;     // Reference to the capsule collider.
	bool isDead;                                  // Whether the enemy is dead.
	bool isSinking;                               // Whether the enemy has started sinking through the floor.

	void Awake ()
	{
		// Setting up the references.
		anim = GetComponent <Animator> ();
		enemyAudio = GetComponent <AudioSource> ();
		hitParticles = GetComponentInChildren <ParticleSystem> ();
		capsuleCollider = GetComponent <CapsuleCollider> ();

		// Setting the current health when the enemy first spawns.
		currentHealth = startingHealth;
	}

	void Update ()
	{
		// If the enemy should be sinking...
		if(isSinking)
		{
			// ... move the enemy down by the sinkSpeed per second.
			transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
		}
	}

	public void TakeDamage (int amount, Vector3 hitPoint)
	{
		// If the enemy is dead...
		if(isDead)
			// ... no need to take damage so exit the function.
			return;

		// Play the hurt sound effect.
		enemyAudio.Play ();

		// Reduce the current health by the amount of damage sustained.
		currentHealth -= amount;

		// Set the position of the particle system to where the hit was sustained.
		hitParticles.transform.position = hitPoint;

		// And play the particles.
		hitParticles.Play();
		if(currentHealth <= 0)
		{
		
			Death ();
		}
	}

	void Death ()
	{
		capsuleCollider.isTrigger = true;
		isDead = true;
		anim.SetTrigger ("Dead");
		enemyAudio.clip = deathClip;
		enemyAudio.Play ();
	}

	public void StartSinking ()
	{
		// Find and disable the Nav Mesh Agent.
		GetComponent <NavMeshAgent> ().enabled = false;
		GetComponent <Rigidbody> ().isKinematic = true;

		ScoreManager.score += scoreValue;

		isSinking = true;		
		Destroy (gameObject, 2f);
	}
}