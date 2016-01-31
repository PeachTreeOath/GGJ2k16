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
	public int playerPoints = 0; 

	enum pointAwards {Perfect = 30, Great = 20, Good = 10}; 

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

		int AwardPoints = 0; 

		inTimeRange = true; 
		if (levelTime > startTargetTime &&  levelTime < endTargetTimePerfect) {
			AwardPoints = (int) pointAwards.Perfect; 
		} else if (levelTime > startTargetTime && levelTime < endTargetTimeGreat) {
			AwardPoints = (int) pointAwards.Great; 
		}else if (levelTime > startTargetTime && levelTime < endTargetTimeGood){
			AwardPoints = (int) pointAwards.Good; 
		} else {
			inTimeRange = false; 
		}

		
		if (Application.loadedLevel == 0) {
			playerAnimator.runtimeAnimatorController = (RuntimeAnimatorController) Resources.Load("Animations/Kimono"); 
		}else if (Application.loadedLevel == 1) {
			playerAnimator.runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load ("Animations/CaveBody");
		}

		if (inTimeRange) {

			String actionText = collided.gameObject.GetComponent<InteractableScript> ().actionText;
			playerPoints += AwardPoints;
			actionTextScript.ShowText(actionText);

				
		} else {

			Debug.Log ("You're out of the time range.");
		}
	}



	float targetTimeToSeconds(float targetHour){
		GameManagerScript gamemanagerscript = GameObject.Find ("GameManager").GetComponent<GameManagerScript> ();
		return (float) (targetHour/24 * gamemanagerscript.GetGameLength()); 
	}


}
