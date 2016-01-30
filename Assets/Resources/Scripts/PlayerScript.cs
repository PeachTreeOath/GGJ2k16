using UnityEngine;
using System.Collections;
using System; 

public class PlayerScript : MonoBehaviour {

	public float speed;
	private Rigidbody2D body;
	public InteractableScript collided;
	private Collider2D[] colliders;
	float closestInteractiveDistance; 
	private float lastClosestInteractiveDistance;
	public bool checkTime; 

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

		//check endtime 
		if (Time.time >= (5f * 60f)) {
			Application.LoadLevel (1);
		}

	}

	void OnTriggerStay2D(Collider2D col)
	{
		float currentInteractiveDistance = Vector2.Distance( this.transform.position, col.gameObject.transform.position); 


		if (closestInteractiveDistance == 0) {
			collided = col.gameObject.GetComponent<InteractableScript> ();
		} else if (currentInteractiveDistance < closestInteractiveDistance) {
			collided = col.gameObject.GetComponent<InteractableScript> ();
		}

		closestInteractiveDistance = Math.Max(lastClosestInteractiveDistance, currentInteractiveDistance);
		lastClosestInteractiveDistance = currentInteractiveDistance; 

	}

	void OnTriggerExit2D(Collider2D col)
	{
		collided = null;
		closestInteractiveDistance = 0f; 
	}

	private void Interact()
	{

		if (collided == null)
			return; 

		startTargetTime = targetTimeToSeconds (collided.targetTime); 
		endTargetTime = targetTimeToSeconds (collided.targetTime + timeRange); 

		if (checkTime) {
			if (Time.time > startTargetTime && Time.time < endTargetTime) {
				inTimeRange = true; 
			} else {
				inTimeRange = false; 
			}
		} else {
			inTimeRange = true; 
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
