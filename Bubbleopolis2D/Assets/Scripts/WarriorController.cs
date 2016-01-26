using UnityEngine;
using System.Collections;

public class WarriorController : MonoBehaviour {
	//public
	public float moveSpeed; //movement speed
	public GameObject Enemy; //Enemy Game object
	public GameObject Player; //Player Game object
	public GameObject Bubble; // Bullet game object 
	public Rigidbody2D rb; // used to make the enemy move 
	public int scoreValue; //used to add to score
	public Transform playerSpawn;
	public GameObject player;
	//private
	private bool go = true; // will turn off the random movement when frozen
	public bool froze = false;// when frozen the player can destroy the npc
	private bool fallThrough;//checks if it is on ground or not
	private int randomNumber;// for random movement
	private GameController gameController; //importing GameController script

	void Start () 
	{
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");//creating where I can use the game controller script
		if (gameControllerObject != null)//checks for the script
		{
			gameController = gameControllerObject.GetComponent <GameController> ();//uses it
		}
		if (gameControllerObject == null) //checks for the script
		{
			Debug.Log ("Cannot find 'GameController' script"); // complains
		}
		StartCoroutine (TimeCheck ());// starts the IEnumerator timecheck
		rb = GetComponent<Rigidbody2D> ();
	}
	IEnumerator TimeCheck()// makes each movement last 2 seconds
	{
		while (go == true)
		{
			yield return new WaitForSeconds (2);
			randomNumber = Random.Range (1, 4);//creates a random movement

		}
	}
	void OnTriggerEnter2D (Collider2D other) // freezes the enemy
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

		if (other.gameObject.tag == "Cloud Platform")//checks if it is on a cloud platform so it can fall through
		{
			fallThrough = true;// lets them fall throguh
		}
		if (froze == true  && other.gameObject.tag == "Player")//checks when the player hits the enemy
		{
			gameController.AddScore (scoreValue); // add to the score
			Destroy (this.gameObject);//destroys the enemy
		}
		if (froze == false  && other.gameObject.tag == "Player")//checks when the player hits the enemy
		{
			gameController.Life (1);
			Instantiate (player, playerSpawn.position, playerSpawn.rotation);
			Destroy (other.gameObject);//destroys the enemy
		}
	}

	void OnCollisionExit2D(Collision2D other) // when they leave the object it turns collision back on
	{
		GetComponent<Collider2D> ().isTrigger = false; //unchecks trigger
	}
	// Update is called once per frame
	void Update () {
		if (go == false)// changes random number if frozen
		{
			randomNumber = 0;
		}
		if (randomNumber == 1)//lets them drop down
		{
			if (fallThrough == true)
			{
				GetComponent<Collider2D> ().isTrigger = true;
			}

		}
		else if (randomNumber == 2)// allows them to move right
		{
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (moveSpeed, GetComponent<Rigidbody2D> ().velocity.y);
			
		} 
		else if (randomNumber == 3) // allows them to move left.
		{
			
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (-moveSpeed, GetComponent<Rigidbody2D>().velocity.y);

		} 

	}
}

