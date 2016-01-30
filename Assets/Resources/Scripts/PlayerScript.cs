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
	int endTargetTimePerfect = 0;
	int endTargetTimeGreat = 0; 
	int endTargetTimeGood; 
	bool inTimeRange = false; 

	private int playerPoints = 0; 
	enum pointAwards {Perfect = 30, Great = 20, Good = 10}; 

	public float timeRange = .75f; // in hours
	private ActionTextScript actionTextScript;

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D> ();
		actionTextScript = GameObject.Find ("ActionText").GetComponent<ActionTextScript> ();
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

			if (Application.loadedLevel == 0) {
				Application.LoadLevel (1);
			}else if (Application.loadedLevel == 1) {
				Application.LoadLevel (2);
			}
		}

	}

	int GetPlayerPoints(){
		return playerPoints; 
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
		endTargetTimePerfect = targetTimeToSeconds (collided.targetTime + timeRange/3);
		endTargetTimeGreat = targetTimeToSeconds (collided.targetTime + timeRange * 2 / 3);
		endTargetTimeGood = targetTimeToSeconds (collided.targetTime + timeRange);

		int AwardPoints = 0; 

		if (checkTime) {

			inTimeRange = true; 
			if (Time.time > startTargetTime && Time.time < endTargetTimePerfect) {
				AwardPoints = (int) pointAwards.Perfect; 
			} else if (Time.time > startTargetTime && Time.time < endTargetTimeGreat) {
				AwardPoints = (int) pointAwards.Great; 
			}else if (Time.time > startTargetTime && Time.time < endTargetTimeGood){
				AwardPoints = (int) pointAwards.Good; 
			} else {
				inTimeRange = false; 
			}

		} else {
			inTimeRange = true; 
		}

		if (inTimeRange) {
			String actionText = collided.gameObject.GetComponent<InteractableScript> ().actionText;

			switch (collided.name) {

			case "Sink":
				{
					transform.localScale *= .5f; 	
					playerPoints += AwardPoints;
					actionTextScript.ShowText(actionText);
					break; 
				}
			case "Mat":
				{
					transform.localScale *= 1.5f;
					playerPoints += AwardPoints;
					actionTextScript.ShowText(actionText);
					break; 
				}
			case "Gong":
				{
					transform.localScale *= .3333f;
					playerPoints += AwardPoints;
					actionTextScript.ShowText(actionText);
					break; 
				}
			case "Kettle": 
				{
					transform.localScale *= 2f; 
					playerPoints += AwardPoints;
					actionTextScript.ShowText(actionText);
					break; 
				}
			case "SticksTrigger":
				{
					transform.localScale *= .66f; 	
					playerPoints += AwardPoints;
					actionTextScript.ShowText(actionText);
					break; 
				}
			case "Bunny": 
				{
					transform.localScale *= 1.5f; 
					playerPoints += AwardPoints;
					actionTextScript.ShowText(actionText);
					break; 
				}
			case "KnifeTrigger":
				{
					transform.localScale *= .33f; 
					playerPoints += AwardPoints;
					actionTextScript.ShowText(actionText);
					break; 
				}
			case "Beartrap":
				{
					transform.localScale *= 2f; 	
					playerPoints += AwardPoints;
					actionTextScript.ShowText(actionText);
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
