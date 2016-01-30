using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour {

	float timeOfDay = 0f; 
	float gameLength = 1f*10f; 
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

	public float GameLength ()
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
		
			if (Application.loadedLevel == 0) {
				Application.LoadLevel (1);
			} else if (Application.loadedLevel == 1) {
				Application.LoadLevel (2);
			} else if (Application.loadedLevel == 2) {
				Application.LoadLevel (3);
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
		approval.ChangeAmount (timeOfDay / 60, 0);
	}
	
}



