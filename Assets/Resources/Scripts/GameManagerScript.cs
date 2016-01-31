using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour {

	float timeOfDay = 0f; 
	float gameLength = 1f*60f; 
	int hours = 0; 
	int minutes = 0; 
	string timeString = ""; 
	float levelTime = 0f;
	public float actionTextDecay = 2f;

	private ApprovalScript approval;
	private Text clock;


	// Use this for initialization
	void Start ()
	{
		approval = GameObject.Find ("ApprovalPrefab").GetComponent<ApprovalScript> ();
		clock = GameObject.Find ("Clock").GetComponent<Text> ();
	}

	public float GetGameLength ()
	{
		return gameLength; 
	}

	// Update is called once per frame
	void Update ()
	{
		UpdateClock ();

		levelTime += Time.deltaTime; 

		//check endtime 
		if (levelTime >= (gameLength)) {

			if (Application.loadedLevel == 2) {
				Application.LoadLevel (3);
			} else if (Application.loadedLevel == 4) {
				Application.LoadLevel (5);
			} else if (Application.loadedLevel == 6) {
				Application.LoadLevel (7);
			}
		}
	}


	void UpdateClock ()
	{
		timeOfDay = levelTime / gameLength * 86400f;
		hours = (int)(timeOfDay / 3600) % 24;
		minutes = (int)(timeOfDay % 3600) / 60;

		if (minutes < 10) {
			timeString = hours + ":0" + minutes; 
		} else {
			timeString = hours + ":" + minutes; 
		}
		clock.text = timeString;
		PlayerScript playerscript = GameObject.Find ("PlayerPrefab").GetComponent<PlayerScript>();
		float playpoints = (float) playerscript.playerPoints;
		approval.ChangeAmount (timeOfDay / 60f, playpoints);
	}

}



