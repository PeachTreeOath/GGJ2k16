using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InteractableScript : MonoBehaviour {

	public int targetTime;
	public string actionText;

	private Text actionTextObj;
	private GameManagerScript gameMgr;
	private SpriteRenderer sprite;
	private float textTime;
	private float textFadeTime;
	private float textDisappearTime;

	// Use this for initialization
	void Start () {
		actionTextObj = GameObject.Find ("ActionText").GetComponent<Text> ();
		gameMgr = GameObject.Find ("GameManager").GetComponent<GameManagerScript> ();
		sprite = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		textTime += Time.deltaTime;

		if (textTime > textDisappearTime) {
			sprite.color = new Color (sprite.color.r, sprite.color.g, sprite.color.b, 0);
		}
		else if (textTime > textFadeTime) {
			float elapsedPercent = (textTime - textFadeTime) / (textDisappearTime - textFadeTime);
			sprite.color = new Color (sprite.color.r, sprite.color.g, sprite.color.b, 1-elapsedPercent);
		}

	}

	public void ShowText()
	{
		actionTextObj.text = actionText;
		textTime = Time.time;
		sprite.color = new Color (sprite.color.r, sprite.color.g, sprite.color.b, 1);
	}
}
