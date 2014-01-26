using UnityEngine;
using System.Collections;

public class HUDScript : MonoBehaviour 
{
	private GameObject hungerFill = null;
	private GameObject player = null;

	private float initialXScale = 0;

	// Use this for initialization
	void Start () 
	{
		hungerFill = gameObject.transform.FindChild("HungerFill").gameObject;
		player = GameObject.FindGameObjectWithTag("Player") as GameObject;

		initialXScale = hungerFill.transform.localScale.x;
	}
	
	// Update is called once per frame
	void Update () 
	{
		UpdateHungerBar();
	}

	void UpdateHungerBar()
	{
		//Access the player's hunger.
		int hunger = player.GetComponent<PlayerScript>().hunger;

		Vector3 newBarSize = hungerFill.transform.localScale;
		newBarSize.x = hunger * 0.05f;

		hungerFill.transform.localScale = newBarSize;
	}
}
