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

	public void ChangeAmount(float elapsedMinutes)
	{
		float percent = (1440 - elapsedMinutes*10) / 1440;
		transform.position = new Vector2 (origPos.x + (width/2*percent), origPos.y);
		transform.localScale = new Vector2 (percent, 2); 
	}
}
