using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public float speed;
	private Rigidbody2D body;
	private GameObject collided;

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D> ();
	}

	// Update is called once per frame
	void Update () {
		body.AddForce(new Vector2(Input.GetAxis("Horizontal") * speed,Input.GetAxis("Vertical") * speed)); 

		if (Input.GetKeyDown(KeyCode.Space)) {
			//check colliding



			if (collided != null) {
				transform.localScale *= 2;
			}
		}

	}

	void OnTriggerEnter2D(Collider2D col)
	{
		collided = col.gameObject;
	}

	void OnTriggerExit2D(Collider2D col)
	{
		collided = null;
	}
}
