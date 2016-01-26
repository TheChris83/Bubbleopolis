using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour 
{
	public float speed;
	void Update()
	{
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (speed, GetComponent<Rigidbody2D> ().velocity.y);//moves the bullets along
	}

}
