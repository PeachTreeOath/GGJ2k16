using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public float speed;
	private Rigidbody2D body;

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D> ();
	}

	// Update is called once per frame
	void Update () {
		body.AddForce(new Vector2(Input.GetAxis("Horizontal") * speed,Input.GetAxis("Vertical") * speed)); 
	}
}
