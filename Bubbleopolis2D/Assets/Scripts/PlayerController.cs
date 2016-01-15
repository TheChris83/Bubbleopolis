using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public class Boundary 
{
	public float xMin, xMax, yMin, yMax;
}

public class PlayerController : MonoBehaviour {
	public float moveSpeed;
	public float jumpHeight;
	public float groundCheckDistance = 1f;
	public LayerMask groundCheckMask;
	public Boundary boundary;
	public float time = 1f;
	private int jumpCount;
	public int jumpMax = 2;
	public float fireRate;
	private float nextFire;
	public GameObject shot;
	public Transform shotSpawn;
	public int lives;
	public Text losingText;

	bool onGround;

	void OnCollisionEnter2D(Collision2D coll) //When ever the player collides with something this happens
	{
		jumpCount = 0; //To reset the amount of jumps the player can do
		onGround = true;
	}
	void OnCollisionExit2D(Collision2D coll)
	{
		onGround = false;
		GetComponent<Collider2D> ().isTrigger = false;
		Debug.Log ("false");

	}
	void Start () 
	{
	}
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.J) && jumpCount < jumpMax)
		{
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (GetComponent<Rigidbody2D>().velocity.x, jumpHeight);
			jumpCount++;
		}
		else if (Input.GetKey (KeyCode.D)) 
		{
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
		}
		else if(Input.GetKey (KeyCode.A)) 
		{
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (-moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
		}
		else if(Input.GetKey (KeyCode.S) && Input.GetKey (KeyCode.J)) 
		{
			if (onGround) 
			{
				RaycastHit2D hitInfo = Physics2D.Raycast (transform.position, -Vector2.up, groundCheckDistance, groundCheckMask);
				if (hitInfo.transform) 
				{
					if(hitInfo.transform.CompareTag("Cloud Platform")) 
					{
						GetComponent<Collider2D> ().isTrigger = true;
						GetComponent<Rigidbody2D> ().velocity = new Vector2 (GetComponent<Rigidbody2D> ().velocity.x, -jumpHeight);
					}
				}
			}
		}
		if (Input.GetKeyDown(KeyCode.K) && Time.time > nextFire) 
		{
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
		}
		if (lives == 0) 
		{
			//losingText.text = "you lose";
		}
	}


}
