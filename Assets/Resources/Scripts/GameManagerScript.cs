using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour {

	float timeOfDay = 0f; 
	float gameLength = 5f*60f; 
	int hours = 0; 
	int minutes = 0; 
	string timeString = ""; 
	public float actionTextDecay = 2f;

	private ApprovalScript approval;
	private Text clock;
	//string[] AsianTeaCeremonySolution = new string[4]; 


	// Use this for initialization
	void Start () {
		approval = GameObject.Find ("ApprovalPrefab").GetComponent<ApprovalScript> ();
		clock = GameObject.Find ("Clock").GetComponent<Text> ();
		GameObject.Find ("ActionText").GetComponent<Text> ().color = Color.clear;
		/*AsianTeaCeremonySolution[0] = "Mat";
		AsianTeaCeremonySolution[1] = "Sink";
		AsianTeaCeremonySolution[2] = "Table";
		AsianTeaCeremonySolution[3] = "Gong";*/

	}
	
	// Update is called once per frame
	void Update () {
		UpdateClock ();
	}


	void UpdateClock(){
		timeOfDay = Time.time / gameLength * 86400f;
		hours = (int) (timeOfDay / 3600) % 24;
		minutes = (int) (timeOfDay % 3600)/60;

		if (minutes < 10) {
			timeString = hours + ":0" + minutes; 
		} else {
			timeString = hours + ":" + minutes; 
		}
		clock.text = timeString;
		approval.ChangeAmount (timeOfDay / 60);
	}

}



