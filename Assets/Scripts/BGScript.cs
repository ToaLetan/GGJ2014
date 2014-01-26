using UnityEngine;
using System.Collections;

public class BGScript : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		if (gameObject.GetComponent<SpriteRenderer> () != null)
			gameObject.GetComponent<SpriteRenderer> ().sprite = GenerateRandBackground ();
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	private Sprite GenerateRandBackground()
	{
		int randNum = Random.Range (1, 5);

		return Resources.Load<Sprite>("Sprites/Backgrounds/BG" + randNum);
	}
}
