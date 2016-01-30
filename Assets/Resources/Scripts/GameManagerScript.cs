using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour {

	float timeOfDay = 0f; 
	float gameLenth = 5f*60f; 
	int hours = 0; 
	int minutes = 0; 
	string timeString = ""; 

	// Use this for initialization
	void Start () {
	



	}
	
	// Update is called once per frame
	void Update () {

		UpdateClock ();

	}


	void UpdateClock(){

		timeOfDay = Time.time / gameLenth * 86400f;
		hours = (int) timeOfDay / 3600;
		minutes = (int) (timeOfDay % 3600)/60;

		if (minutes < 10) {
			timeString = hours + ":0" + minutes; 
		} else {
			timeString = hours + ":" + minutes; 
		}
		GameObject.Find ("Clock").GetComponent<Text> ().text = timeString;

	}

}



