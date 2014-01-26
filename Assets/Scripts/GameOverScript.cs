using UnityEngine;
using System.Collections;

public class GameOverScript : MonoBehaviour 
{
	PlayerScript playerInfo = null;

	Transform[] gameOverObjects = null;

	// Use this for initialization
	void Start () 
	{
		playerInfo = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerScript> ();
		playerInfo.GameOverEvent += OnGameOver;

		gameOverObjects = gameObject.GetComponentsInChildren<Transform> ();

		for (int i = 0; i < gameOverObjects.Length; i++) 
		{
			if(gameOverObjects[i].GetComponent<Renderer>() != null)
			{
				Color objColor = gameOverObjects[i].GetComponent<Renderer>().material.color;
				objColor.a = 0;
				gameOverObjects[i].GetComponent<Renderer>().material.color = objColor;
			}
		}


	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnGameOver()
	{
		Debug.Log ("GAME OVER");

		for (int i = 0; i < gameOverObjects.Length; i++) 
		{
			if(gameOverObjects[i].name == "ResultsText")
			{
				gameOverObjects[i].GetComponent<TextMesh> ().text = "You lasted for: " + Mathf.CeilToInt(playerInfo.survivalTime) + " seconds!";
				Debug.Log(playerInfo.survivalTime);
			}
		}

		for (int i = 0; i < gameOverObjects.Length; i++) 
		{
			if(gameOverObjects[i].GetComponent<Renderer>() != null)
			{
				Color objColor = gameOverObjects[i].GetComponent<Renderer>().material.color;
				objColor.a = 1;
				gameOverObjects[i].GetComponent<Renderer>().material.color = objColor;
			}
		}
	}
}
