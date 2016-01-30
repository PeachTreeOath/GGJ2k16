using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public float speed;
	private Rigidbody2D body;
	public InteractableScript collided;

	int startTargetTime = 0; 
	int endTargetTime = 0; 
	bool inTimeRange = false; 

	public float timeRange = .25f; // in hours

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D> ();
	}

	// Update is called once per frame
	void Update () {
		body.AddForce(new Vector2(Input.GetAxis("Horizontal") * speed,Input.GetAxis("Vertical") * speed)); 

		if (Input.GetKeyDown(KeyCode.Space)) {

			//check colliding
			Interact(); 



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

		startTargetTime = targetTimeToSeconds (collided.targetTime); 
		endTargetTime = targetTimeToSeconds (collided.targetTime + timeRange); 

		if (Time.time > startTargetTime && Time.time < endTargetTime) {
			inTimeRange = true; 
		} else {
			inTimeRange = false; 
		}

		if (inTimeRange) {

			switch (collided.name) {

			case "Sink":
				{
					transform.localScale *= .5f; 	
					break; 
				}
			case "Mat":
				{
					transform.localScale *= 2f;
					break; 
				}
			case "Gong":
				{
					transform.localScale *= .3333f;
					break; 
				}
			case "Table": 
				{
					transform.localScale *= 3f; 
					break; 
				}
			}
		} else {

			Debug.Log ("You're out of the time range.");
		}
	}



	int targetTimeToSeconds(float targetHour){

		return (int) (targetHour * 3600f); 
	}
}
