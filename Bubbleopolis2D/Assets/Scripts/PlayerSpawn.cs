using UnityEngine;
using System.Collections;

public class PlayerSpawn : MonoBehaviour
{
	public GameObject player;
	public Transform playerSpawn;
	public float spawnRate;
	public float delay;

	void Start ()
	{
		InvokeRepeating ("Fire", delay, spawnRate);// slows down the spawn rate to have a delay
	}

	void Fire ()
	{
		Instantiate(player, playerSpawn.position, playerSpawn.rotation);//makes it where you can spawn the warrior
	}
}
