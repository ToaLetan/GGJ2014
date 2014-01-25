using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour 
{
	//Exposing CamTarget to be changed in Unity at runtime.
	public GameObject CamTarget = null;

	private Vector3 newCamPos = Vector3.zero;

	// Use this for initialization
	void Start () 
	{
		newCamPos = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		newCamPos = new Vector3(CamTarget.transform.position.x, CamTarget.transform.position.y, newCamPos.z);

		gameObject.transform.position = newCamPos;
	}
}
