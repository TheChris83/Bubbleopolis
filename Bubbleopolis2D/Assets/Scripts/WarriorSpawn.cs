using UnityEngine;
using System.Collections;

public class WarriorSpawn : MonoBehaviour
{
	public GameObject warrior;
	public Transform warriorSpawn;
	public float spawnRate;
	public float delay;

	void Start ()
	{
		InvokeRepeating ("Fire", delay, spawnRate);// slows down the spawn rate to have a delay
	}

	void Fire ()
	{
		Instantiate(warrior, warriorSpawn.position, warriorSpawn.rotation);//makes it where you can spawn the warrior
	}
}
