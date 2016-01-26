using UnityEngine;
using System.Collections;

public class MageController : MonoBehaviour {
	//Public
	public float moveSpeed;// movement speed.
	public GameObject Enemy; //creating the enemy GameObject
	public GameObject Player; // finding the player
	public GameObject shot; // the bubble
	public Transform shotSpawn;// allows for a spawn location 
	public int scoreValue; //so it can add to the score
	public float fireRate; // fire rate
	public Rigidbody2D rb; //allows them to move
	public Transform playerSpawn;
	public GameObject player;
	//Private 

	private int randomNumber; // creates a random number
	bool go = true; //stops or allows motion of the NPC
	bool froze = false; //can freeze them
	private GameController gameController; // allows access the game Controller
	private float nextFire;// for the fire rate of the npc 
	void Start () 
	{
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");//finds the game controller
		if (gameControllerObject != null)//checks if it is there 
		{
			gameController = gameControllerObject.GetComponent <GameController> ();// allows access to the game controller by using getComponent
		}
		if (gameControllerObject == null) //checks if it isnt there
		{ 
			Debug.Log ("Cannot find 'GameController' script"); //complains at you
		}
		StartCoroutine (TimeCheck ());//start the timecheck function
	}
	IEnumerator TimeCheck()
	{
		while (go == true)//keeps the mage moving
		{
			yield return new WaitForSeconds (2);//waits 2 seconds
			randomNumber = Random.Range (1, 3); //picks to move right or left 


		}
		while (go == true)//keeps the mage moving
		{
			yield return new WaitForSeconds (2);//waits 2 seconds
			Instantiate (shot, shotSpawn.position, shotSpawn.rotation);//creates the bullet

		}
	}
	void OnTriggerEnter2D (Collider2D other)// checks if he was hit by a bubble
	{
		if (other.gameObject.tag == "Bolt") //checks if they were hit by a tag with  Bolt
		{
			go = false;//stops movement
			froze = true;//freezes them
			Destroy (other.gameObject);// destroys the bubble
		}

	}
	void OnCollisionEnter2D(Collision2D other) //When ever the player collides with something this happens
	{
		if (froze == true  && other.gameObject.tag == "Player")//checks when the player hits the enemy
		{
			gameController.AddScore (scoreValue); // add to the score
			Destroy (this.gameObject);//destroys the enemy
		}
		if (froze == false  && other.gameObject.tag == "Player")//checks when the player hits the enemy
		{
			gameController.Life (1);
			Destroy (other.gameObject);//destroys the enemy
			Instantiate (player, playerSpawn.position, playerSpawn.rotation);
		}

	}


	// Update is called once per frame
	void Update () {
		if (go == false)// stops movement
		{
			randomNumber = 0; //sets random to zero
		}
		if (randomNumber == 1)//moves right
		{
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
		} 
		else if (randomNumber == 2) //moves left
		{
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (-moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
		} 
	
	}
}

