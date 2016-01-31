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

	float startTargetTime = 0f; 
	float endTargetTimePerfect = 0f;
	float endTargetTimeGreat = 0f; 
	float endTargetTimeGood = 0f; 
	bool inTimeRange = false; 
	float levelTime = 0f;

	Animator playerAnimator; 
	public decimal playerPoints = 0M; 

	decimal Perfect = 0.25M;
	decimal Great = 0.1M;
	decimal Good = 0.05M; 

	public float timeRange = .75f; // in hours
	private ActionTextScript actionTextScript;

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D> ();
		actionTextScript = GameObject.Find ("ActionText").GetComponent<ActionTextScript> ();
		playerAnimator = this.GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () {
		body.AddForce(new Vector2(Input.GetAxis("Horizontal") * speed,Input.GetAxis("Vertical") * speed)); 

		if (Input.GetKeyDown(KeyCode.Space)) {
			//check colliding
			Interact(); 
		}

		levelTime += Time.deltaTime; 

	}

	decimal GetPlayerPoints(){
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
		playerAnimator.runtimeAnimatorController = null; 
	}

	private void Interact()
	{

		if (collided == null)
			return; 

		startTargetTime = targetTimeToSeconds (collided.targetTime); 
		endTargetTimePerfect = targetTimeToSeconds (collided.targetTime + timeRange/3);
		endTargetTimeGreat = targetTimeToSeconds (collided.targetTime + timeRange * 2 / 3);
		endTargetTimeGood = targetTimeToSeconds (collided.targetTime + timeRange);

		decimal AwardPoints = 0M; 

		inTimeRange = true; 
		if (levelTime > startTargetTime &&  levelTime < endTargetTimePerfect) {
			AwardPoints = (decimal) Perfect; 
		} else if (levelTime > startTargetTime && levelTime < endTargetTimeGreat) {
			AwardPoints = (decimal) Great; 
		}else if (levelTime > startTargetTime && levelTime < endTargetTimeGood){
			AwardPoints = (decimal) Good; 
		} else {
			inTimeRange = false; 
		}

		
		if (Application.loadedLevel == 0) {
			playerAnimator.runtimeAnimatorController = (RuntimeAnimatorController) Resources.Load("Animations/Kimono"); 
		}else if (Application.loadedLevel == 1) {
			playerAnimator.runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load ("Animations/CaveBody");
		}

		if (inTimeRange) {
			playerPoints += AwardPoints;
		} else {

			Debug.Log ("You're out of the time range.");
		}
		String actionText = collided.gameObject.GetComponent<InteractableScript> ().actionText;
		actionTextScript.ShowText(actionText);
	}



	float targetTimeToSeconds(float targetHour){
		GameManagerScript gamemanagerscript = GameObject.Find ("GameManager").GetComponent<GameManagerScript> ();
		return (float) (targetHour/24 * gamemanagerscript.GetGameLength()); 
	}


}
