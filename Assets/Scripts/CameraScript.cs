using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour 
{
	//Exposing CamTarget to be changed in Unity at runtime.
	public GameObject CamTarget = null;

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		if(CamTarget != null)
			gameObject.transform.position = new Vector3(CamTarget.transform.position.x, CamTarget.transform.position.y, gameObject.transform.position.z);
	}
}
