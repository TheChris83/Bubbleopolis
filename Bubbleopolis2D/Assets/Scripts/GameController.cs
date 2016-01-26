using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	//Publics
	public Transform[] mageSpawnPoints;//to create random spawns
	public Transform[] warriorSpawnPoints;
	public float spawnWait;// to tell how long to wait between spawns
	public float startWait;// how long to wait before you start spawning
	public float waveWait; // how long you wait for the next wave to come in 
	//public GUIText scoreText;
	//public GUIText gameOverText;
	public GameObject warrior; //warrior object used for spawning
	public Transform warriorSpawn; //^^
	public GameObject mage;// mage object used for spawning
	public Transform mageSpawn;// ^^
	public int playerLives; // player lives
	public float speedIncrease;
	public float fireRateIncrease;
	public int totalMobs;//  the number of mobs you want to spawn
	//Privates
	private int score;
	private bool gameOver; // so I can end the game
	private int randomNumber;// to randomly spawn a mob
	private int totalLives; // for lives 
	private WarriorController warriorController; //getting access to warrior controller
	private MageController mageController; // getting access to mage controller
	// Use this for initialization
	void Start () {
		GameObject mageControllerObject = GameObject.FindGameObjectWithTag ("Enemy");//creating a way to access it
		GameObject warriorControllerObject = GameObject.FindGameObjectWithTag ("Enemy");//creating a way to access it
		gameOver = false;//starting stuff
		//gameOverText.text = "";
		score = 0;
		UpdateScore ();
		StartCoroutine (SpawnWaves ()); // starts the spawning
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (gameOver.ToString ());
	}
	IEnumerator SpawnWaves()//Spawning
	{
		yield return new WaitForSeconds (startWait); // waits before spawning the first wave
		while (gameOver == true) // to keep it going
		{
			for (int i = 0; i < totalMobs; i++)//counts how many total mobs and random picks which ones to spawn
			{
				randomNumber = Random.Range (1, 2);
				if (randomNumber == 1)
				{
					Vector3 warriorPos = warriorSpawnPoints [Random.Range (0, warriorSpawnPoints.Length)].position; //creating it to randomly spawn the enemies at a random spawn point
					Instantiate (warrior, warriorPos, warriorSpawn.rotation);// spawns the warrior
				}
				else
				{
					Vector3 magePos = mageSpawnPoints[Random.Range (0, mageSpawnPoints.Length)].position;// creating a random spwan point for mage
					Instantiate (mage, magePos, mageSpawn.rotation);// spawns the mage
				}
				yield return new WaitForSeconds (spawnWait);// waits between each spawn
			}
			yield return new WaitForSeconds (waveWait); // wait between each wave 
			mageController.moveSpeed += speedIncrease; // increase mage speed each wave
			mageController.fireRate -= fireRateIncrease;//increase the amount the mage fires each wave
			warriorController.moveSpeed += speedIncrease; // increase the speed of the warrior each wave


		}
	}

	public void AddScore (int scoreValue)//adds scores together
	{
		score += scoreValue;
		UpdateScore ();
	}

	public void UpdateScore()//updates the score
	{
		//scoreText.text = "Score: " + score;
	}
	public void Life(int minusLife)//checks the lives
	{

		totalLives += minusLife;
		if (totalLives == playerLives)
		{
			//gameOverText.text = "Game Over!";
			gameOver = true;
		}

	}

}
