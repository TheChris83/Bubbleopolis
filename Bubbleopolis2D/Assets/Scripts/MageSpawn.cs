using UnityEngine;
using System.Collections;

public class MageSpawn : MonoBehaviour 
	{
		public GameObject mage;
		public Transform mageSpawn;
		public float spawnRate;
		public float delay;

		void Start ()
		{
			InvokeRepeating ("Fire", delay, spawnRate);//slows down the spawn rate
		}

		void Fire ()
		{
		Instantiate(mage, mageSpawn.position, mageSpawn.rotation);//allows it to be created
		}
	}
