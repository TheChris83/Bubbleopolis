using UnityEngine;
using System.Collections;

public class DestroyByBoundary : MonoBehaviour {

	// Use this for initialization
	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.tag == "Bolt")
		{
			Destroy (other.gameObject);
		}
	}
}
