using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;            //speed 
	public Vector3 jump;
    Vector3 movement;                   
    Animator anim;                      
    Rigidbody playerRigidbody;          
    int floorMask;                      
    float camRayLength = 100f;          
	public float jumpSpeed = 1f;
	public int count = 0;
	public int used = 0;
	bool canjump = false;
	bool grounded = true;
	float timer = 0f;
	public static bool shieldactive = false;
//	ScoreManager scoremanage;
    void Awake ()
    {
        floorMask = LayerMask.GetMask ("Floor");
		anim = GetComponent <Animator> ();
        playerRigidbody = GetComponent <Rigidbody> ();
		jump = new Vector3 (0.0f, 0.1f, 0.0f);

    }

	void OnCollisionStay()
	{
		grounded = true;
	}

    void FixedUpdate ()
    {
        //input axes.
        float h = Input.GetAxisRaw ("Horizontal");
        float v = Input.GetAxisRaw ("Vertical");
        Move (h ,v);
        Turning ();
        Animating (h, v);
    }

	void Update(){
		if (canjump) {
			timer += Time.deltaTime;
			if (timer > 10f) {
				canjump = false;
				transform.position = new Vector3 (transform.position.x+1, 0, transform.position.z+2);
				timer = 0;
			}
		}
		//Input.GetKeyDown(
		if (Input.GetKeyDown (KeyCode.Space) && canjump) {
			playerRigidbody.AddRelativeForce (Vector3.up * jumpSpeed, ForceMode.Impulse);
			grounded = false;
		}
	}


    void Move (float h, float v)
    {
		movement.Set (h, 0f,v);
        movement = movement.normalized * speed * Time.deltaTime;
        playerRigidbody.MovePosition (transform.position + movement);
    }

    void Turning ()
    {
        Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
        RaycastHit floorHit;
		if(Physics.Raycast (camRay, out floorHit, camRayLength, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
			// playerToMouse.y = 0f;
			Quaternion newRotation = Quaternion.LookRotation (playerToMouse);
			playerRigidbody.MoveRotation (newRotation);
        }
    }

    void Animating (float h, float v)
    {
        bool walking = h != 0f || v != 0f;
		anim.SetBool ("IsWalking", walking);
    }

	void OnTriggerEnter(Collider other)
	{
		
		if (other.gameObject.CompareTag ("Pick Up")) 
		{
			count = count + 1;
			other.gameObject.SetActive (false);
			if (ScoreManager.score > (50 * used) && used < count) 
			{
				used = used + 1;
				PlayerHealth phealth = GetComponent<PlayerHealth> ();
				phealth.TakeDamage (-50);
			}
		}
		if (other.gameObject.CompareTag ("Damage")) 
		{
			other.gameObject.SetActive (false);
			PlayerShooting.damagePerShot = 100;
		}
		if (other.gameObject.CompareTag ("Shield")) 
		{
			other.gameObject.SetActive (false);
			//EnemyAttack.attackDamage = 0;
			shieldactive = true;
		}
		if (other.gameObject.CompareTag ("Jump")) {
			other.gameObject.SetActive (false);
			canjump = true;
		}

	}
}
/*
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6		f;            // The speed that the player will move at.

    Vector3 movement;                   // The vector to store the direction of the player's movement.
    Animator anim;                      // Reference to the animator component.
    Rigidbody playerRigidbody;          // Reference to the player's rigidbody.
    int floorMask;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
    float camRayLength = 100f;          // The length of the ray from the camera into the scene.

    void Awake ()
    {
        // Create a layer mask for the floor layer.
        floorMask = LayerMask.GetMask ("Floor");

        // Set up references.
        anim = GetComponent <Animator> ();
        playerRigidbody = GetComponent <Rigidbody> ();
    }


    void FixedUpdate ()
    {
        //input axes.
        float h = Input.GetAxisRaw ("Horizontal");
        float v = Input.GetAxisRaw ("Vertical");
        Move (h, v);
        // Turn the player to face the mouse cursor.
        Turning ();
        Animating (h, v);
    }

    void Move (float h, float v)
    {
        // Set the movement vector based on the axis input.
        movement.Set (h, 0f, v);
        // Normalise the movement vector and make it proportional to the speed per second.
        movement = movement.normalized * speed * Time.deltaTime;
        // Move the player to it's current position plus the movement.
        playerRigidbody.MovePosition (transform.position + movement);
    }

    void Turning ()
    {
        // Create a ray from the mouse cursor on screen in the direction of the camera.
        Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);

        // Create a RaycastHit variable to store information about what was hit by the ray.
        RaycastHit floorHit;

        // Perform the raycast and if it hits something on the floor layer...
        if(Physics.Raycast (camRay, out floorHit, camRayLength, floorMask))
        {
            // Create a vector from the player to the point on the floor the raycast from the mouse hit.
            Vector3 playerToMouse = floorHit.point - transform.position;

            // Ensure the vector is entirely along the floor plane.
            playerToMouse.y = 0f;

            // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
            Quaternion newRotation = Quaternion.LookRotation (playerToMouse);

            // Set the player's rotation to this new rotation.
            playerRigidbody.MoveRotation (newRotation);
        }
    }

    void Animating (float h, float v)
    {
        // Create a boolean that is true if either of the input axes is non-zero.
        bool walking = h != 0f || v != 0f;

        // Tell the animator whether or not the player is walking.
        anim.SetBool ("IsWalking", walking);
    }
} */