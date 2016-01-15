using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	public float moveSpeed;
	public float jumpHeight;
	public float groundCheckDistance = 1f;
	public LayerMask groundCheckMask;
	private int randomNumber;
	public float movementTime;
	private float movementTimer;
	private bool go = true;
	void Start () {
		movementTimer = Time.time - movementTime;
		InvokeRepeating ("RandomNumberGen", 2.0f, 2.0f);
	}
	int RandomNumberGen()
	{
		randomNumber = Random.Range(1,4);
		return randomNumber;
	}
	void randomMovement()
	{
		if (RandomNumberGen () == 1 && movementTimer < movementTime) {
			movementTimer = Time.time - movementTime;
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (GetComponent<Rigidbody2D> ().velocity.x, jumpHeight);
		} else if (RandomNumberGen () == 2 && movementTimer < movementTime) {
			movementTimer = Time.time - movementTime;
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (moveSpeed, GetComponent<Rigidbody2D> ().velocity.y);

		} else if (RandomNumberGen () == 3 && movementTimer < movementTime) {
			movementTimer = Time.time - movementTime;
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (-moveSpeed, GetComponent<Rigidbody2D> ().velocity.y);
		} else if (RandomNumberGen () == 4 && movementTimer < movementTime) {
			movementTimer = Time.time - movementTime;
			RaycastHit2D hitInfo = Physics2D.Raycast (transform.position, -Vector2.up, groundCheckDistance, groundCheckMask);
			if (hitInfo.transform) {
				if (hitInfo.transform.CompareTag ("Cloud Platform")) {
					GetComponent<Collider2D> ().isTrigger = true;
					GetComponent<Rigidbody2D> ().velocity = new Vector2 (GetComponent<Rigidbody2D> ().velocity.x, -jumpHeight);
				}
			}

		}
		//yield return new WaitForSeconds(2);
	}
	// Update is called once per frame
	void FixedUpdate () {
		RandomNumberGen ();
		randomMovement();
	}
	}
