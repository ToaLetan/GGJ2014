using UnityEngine;
using System.Collections;

public class GameOverScript : MonoBehaviour 
{
	PlayerScript playerInfo = null;

	// Use this for initialization
	void Start () 
	{
		playerInfo = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerScript> ();
		playerInfo.GameOverEvent += OnGameOver;

		//gameObject.renderer.material.color = Color.red;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnGameOver()
	{
		Debug.Log ("GAME OVER");
	}
}
