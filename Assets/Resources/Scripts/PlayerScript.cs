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
	public GameObject head; 

	float startTargetTime = 0f; 
	float endTargetTimePerfect = 0f;
	float endTargetTimeGreat = 0f; 
	float endTargetTimeGood = 0f; 
	bool inTimeRange = false; 
	float levelTime = 0f;
	int level = 0; 
	bool actionAllowed = true;

	Animator playerAnimator; 
	SpriteRenderer playerSpriteRenderer; 
	SpriteRenderer playerHeadSpriteRenderer; 

	Sprite happyFace; 
	Sprite frownFace; 
	Sprite neutralFace; 
	Sprite neutralBody; 

	public decimal playerPoints = 0M; 

	private float lastActionTime;
	public float actionCooldown = 2;

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
		playerSpriteRenderer = this.GetComponent<SpriteRenderer> ();
		playerHeadSpriteRenderer = head.GetComponent<SpriteRenderer> (); 
		level = Application.loadedLevel; 

		if (level == 0) {
			happyFace = Resources.Load <Sprite> ("Images/teahouseplayerhappyhead");
			frownFace =  Resources.Load <Sprite>("Images/teahouseplayerfrownhead");
			neutralFace = Resources.Load <Sprite>("Images/teahouseplayerneutralhead");
			neutralBody = Resources.Load <Sprite>("Images/teahouseplayerbody");
		} else if (level == 1) {
			happyFace = Resources.Load <Sprite>("Images/wildernessplayerhappyhead");
			frownFace = Resources.Load <Sprite>("Images/wildernessplayerfrownhead");
			neutralFace = Resources.Load <Sprite>("Images/wildernessplayerneutralhead");
			neutralBody = Resources.Load <Sprite>("Images/wildernessplayerbody");
		} else if (level == 2) {
			happyFace = Resources.Load <Sprite>("Images/spaceshipplayerhappyhead");
			frownFace = Resources.Load <Sprite>("Images/spaceshipplayerfrownhead");
			neutralFace = Resources.Load <Sprite>("Images/spaceshipplayerneutralhead");
			neutralBody = Resources.Load <Sprite>("Images/spaceshipplayerbody");
		}
		Debug.Log (frownFace); 
	}

	// Update is called once per frame
	void Update () {
		body.AddForce(new Vector2(Input.GetAxis("Horizontal") * speed,Input.GetAxis("Vertical") * speed)); 

		if (Input.GetKeyDown(KeyCode.Space) && actionAllowed) {
				lastActionTime = Time.time;
			actionAllowed = false;
				//check colliding
				Interact();
		}

		if (Time.time > lastActionTime + actionCooldown && !actionAllowed) {
			actionAllowed = true;
		playerAnimator.runtimeAnimatorController = null; 
		playerHeadSpriteRenderer.sprite = neutralFace;
		playerSpriteRenderer.sprite = neutralBody; 

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

		
		if (level == 0) {
			playerAnimator.runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load ("Animations/Kimono"); 
		} else if (level == 1) {
			playerAnimator.runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load ("Animations/CaveBody");
		} else if (level == 2) {
			playerAnimator.runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load ("Animations/Alien");
		}

		if (inTimeRange) {
			playerPoints += AwardPoints;
			playerHeadSpriteRenderer.sprite = happyFace;

		} else {

			playerHeadSpriteRenderer.sprite = frownFace; 
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
