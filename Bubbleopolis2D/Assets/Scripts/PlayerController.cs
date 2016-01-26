using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour {
	//Public Variables
	public float moveSpeed;//movement speed
	public float jumpHeight;//jump height
	public int jumpMax = 2;//allows for a double jump
	public float fireRate;//used for the fire rate
	public GameObject shot; //used for fireing bubbles
	public Transform shotSpawn;//spawns the bubbles
	public Transform playerSpawn;
	public GameObject player;
	public int lives; //lives for the player
//creates the player object
	//Private Variables
	private int jumpCount; //checks the amount of jumps
	private float nextFire;// fire rate
	private bool onGround; //checks if its on ground
	private GameController gameController;//imports game controller
	private WarriorController warriorController;
	private float groundCheckDistance = 1f;//checks the ground distance
	private LayerMask groundCheckMask; //checks for the ground
	void Start () 
	{
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");//importing game controller
		GameObject warriorControllerObject = GameObject.FindGameObjectWithTag ("Enemy");//creating a way to access it
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent <GameController> ();
		}
		if (gameControllerObject == null)
		{
			Debug.Log ("Cannot find 'GameController' script");
		}

	}

	void OnCollisionEnter2D(Collision2D other) //When ever the player collides with something this happens
	{

		onGround = true;// seetting ground
		jumpCount = 0; //To reset the amount of jumps the player can do
		if (other.gameObject.tag == "Bolt") //checks if they were hit by a tag with  Bolt
		{
			gameController.Life (1);
			Destroy (other.gameObject);// destroys the bubble
			Instantiate (player, playerSpawn.position, playerSpawn.rotation);
		}

	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Enemy")//checks when the player hits the enemy
		{

			Destroy (other.gameObject);//destroys the enemy
		}

	}
	void OnTriggerExit2D(Collider2D other)//when the player exits a collider it will turn his trigger back to false that way he can stop
	{
		onGround = false;//not on the ground
		GetComponent<Collider2D> ().isTrigger = false;//collision on

	}

	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.J) && jumpCount < jumpMax) // IF they press jump and jumpCount is less than Jump Max this will happen
		{
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (GetComponent<Rigidbody2D> ().velocity.x, jumpHeight);
			jumpCount++; // Adding ot jump count
			RaycastHit2D hitInfo = Physics2D.Raycast (transform.position, Vector2.up, groundCheckDistance, groundCheckMask); // this is part to make it where they are to ground level
			if (hitInfo.transform) // so hit it hits ground it go through
			{
				if(hitInfo.transform.CompareTag("Cloud Platform")) // compares the platform to only let them go through cloud platform
				{
					GetComponent<Collider2D> ().isTrigger = true;//setting nocollide
				}
			} 
		}
		if (Input.GetKey (KeyCode.D)) // D makes the move the right
		{
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
		}
		if (Input.GetKey (KeyCode.A)) // A makes the move the left
		{
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (-moveSpeed, GetComponent<Rigidbody2D>().velocity.y);

		}
		if (Input.GetKey (KeyCode.S)) // S makes them drop down through the ground
		{
			if (onGround) // Checks if they are on ground
			{
				RaycastHit2D hitInfo = Physics2D.Raycast (transform.position, -Vector2.up, groundCheckDistance, groundCheckMask); // this is part to make it where they are to ground level
				if (hitInfo.transform) // so hit it hits ground it go through
				{
					if(hitInfo.transform.CompareTag("Cloud Platform")) // compares the platform to only let them go through cloud platform
					{
						GetComponent<Collider2D> ().isTrigger = true;// sets no collide
						GetComponent<Rigidbody2D> ().velocity = new Vector2 (GetComponent<Rigidbody2D> ().velocity.x, -jumpHeight);//allows them to fall through
					}
				} 
			}
		}
		if (Input.GetKeyDown(KeyCode.K) && Time.time > nextFire)//checks if k is pressed and they can fire again 
		{
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);//creates the bullet
		}
	}


}
