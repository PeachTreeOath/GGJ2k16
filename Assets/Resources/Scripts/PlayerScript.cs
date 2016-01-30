using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public float speed;
	private Rigidbody2D body;
	private InteractableScript collided;

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D> ();
	}

	// Update is called once per frame
	void Update () {
		body.AddForce(new Vector2(Input.GetAxis("Horizontal") * speed,Input.GetAxis("Vertical") * speed)); 

		if (Input.GetKeyDown(KeyCode.Space)) {
			if (collided != null) {
				Interact ();
			}
		}

	}

	void OnTriggerEnter2D(Collider2D col)
	{
		collided = col.gameObject.GetComponent<InteractableScript>();
	}

	void OnTriggerExit2D(Collider2D col)
	{
		collided = null;
	}

	private void Interact()
	{
		switch(collided.name)
		{
			case "sink":
			{
				break;
			}
		}
	}
}
