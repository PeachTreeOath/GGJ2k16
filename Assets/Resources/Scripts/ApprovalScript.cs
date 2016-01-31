using UnityEngine;
using System.Collections;

public class ApprovalScript : MonoBehaviour {

	public float amount;

	private Vector2 origPos;
	private float width;

	// Use this for initialization
	void Start () {
		origPos = transform.position;
		width = GetComponent<SpriteRenderer> ().bounds.size.x;


	}
	
	// Update is called once per frame
	void Update () {

	}

	public void ChangeAmount(float elapsedMinutes, float addMinutes)
	{
		float percent = (1440 - elapsedMinutes) / 1440;
		if (percent != 0f) {
			transform.position = new Vector2 (origPos.x + (width / 2 * percent), origPos.y);
			transform.localScale = new Vector2 (percent, 2); 
		}
		float addMinutes2 = (float) addMinutes;
		if (percent > 0f && addMinutes2 != 0f) {
			transform.localScale = new Vector2(percent + addMinutes, 2);
		}
	}
}
