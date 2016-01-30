using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ActionTextScript : MonoBehaviour {

	public float fadeSpeed = 1f;
	private Text actionTextObj;
	private Color origColor;
	private float currentFadeTime = 10;
	private float textFadeTime = 2;

	// Use this for initialization
	void Start () {
		actionTextObj = GetComponent<Text> ();
		origColor = actionTextObj.color;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (currentFadeTime);
		currentFadeTime += Time.deltaTime;
		if (currentFadeTime > textFadeTime) {
			actionTextObj.color = Color.Lerp(origColor, Color.clear, fadeSpeed * (currentFadeTime - textFadeTime));
		}
	}
		
	public void ShowText(string actionText)
	{
		actionTextObj.text = actionText;
		currentFadeTime = 0;
		actionTextObj.color = origColor;
	}
}
