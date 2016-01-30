using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour {

	float timeOfDay = 0f; 
	float gameLenth = 5f*60f; 
	int hours = 0; 
	int minutes = 0; 
	string timeString = ""; 


	//string[] AsianTeaCeremonySolution = new string[4]; 


	// Use this for initialization
	void Start () {
	
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



