using UnityEngine;
using System.Collections;

public class TransitionScript : MonoBehaviour {

	public GameObject obj1;
	public GameObject obj2;
	public float startFadeTime = 2;
	public float fadeSpeed = .2f;

	private float currFade;
	private SpriteRenderer spr1;
	private SpriteRenderer spr2;

	// Use this for initialization
	void Start () {
		//GameObject sprObj1 = (GameObject)Instantiate (obj1, Vector2.zero, Quaternion.identity);
		//GameObject sprObj2 = (GameObject)Instantiate (obj2, Vector2.zero, Quaternion.identity);
		/*obj1.GetComponent<PlayerScript> ().enabled = false;
		obj2.GetComponent<PlayerScript> ().enabled = false;
		obj1.GetComponent<Rigidbody2D> ().enabled = false;
		obj2.GetComponent<Rigidbody2D> ().enabled = false;*/

		GameObject[] objs2 = GameObject.FindGameObjectsWithTag ("P2");
		foreach(GameObject obj in objs2)
		{
			SpriteRenderer spr = obj.GetComponent<SpriteRenderer> ();
			spr.color = new Color (spr.color.r, spr.color.g, spr.color.b, 0); 
		}
		startFadeTime += Time.time;

	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > startFadeTime) {
			currFade += Time.deltaTime * fadeSpeed;

			GameObject[] objs1 = GameObject.FindGameObjectsWithTag ("P1");
			GameObject[] objs2 = GameObject.FindGameObjectsWithTag ("P2");

			foreach(GameObject obj in objs1)
			{
				SpriteRenderer spr = obj.GetComponent<SpriteRenderer> ();
				spr.color = new Color (spr.color.r, spr.color.g, spr.color.b, 1 - currFade); 
			}
			foreach(GameObject obj in objs2)
			{
				SpriteRenderer spr = obj.GetComponent<SpriteRenderer> ();
				spr.color = new Color (spr.color.r, spr.color.g, spr.color.b, currFade); 
			}
		}
	}
}
