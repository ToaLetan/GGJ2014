using UnityEngine;
using System.Collections;

public class InputManager
{
	public enum MoveDirection { Idle, Up, Down, Left, Right };

	public delegate void InputEvent(MoveDirection movementdir);

	//Key pressed events
	public event InputEvent UpKey_Pressed;
	public event InputEvent DownKey_Pressed;
	public event InputEvent LeftKey_Pressed;
	public event InputEvent RightKey_Pressed;
	public event InputEvent ActionKey_Pressed;

	//Key released events
	public event InputEvent UpDownKeys_Released;
	public event InputEvent LeftRightKeys_Released;
	public event InputEvent ActionKey_Released;
	
	private KeyCode UpKey;
	private KeyCode DownKey;
	private KeyCode LeftKey;
	private KeyCode RightKey;
	private KeyCode ActionKey;

	private static InputManager instance = null;

	public static InputManager Instance
	{
		get
		{
			if(instance == null)
				instance = new InputManager();
			return instance;
		}
	}


	// Use this for initialization
	private InputManager() 
	{
		//Establish key defaults.
		UpKey = KeyCode.W;
		DownKey = KeyCode.S;
		LeftKey = KeyCode.A;
		RightKey = KeyCode.D;
		ActionKey = KeyCode.Space;
	}
	
	// Update is called once per frame
	public void Update () 
	{
		CheckInput();
	}

	private void CheckInput()
	{
		//Key pressed
		if(Input.GetKey(UpKey))
		{
			if(UpKey_Pressed != null)
				UpKey_Pressed(MoveDirection.Up);
		}
		if(Input.GetKey(DownKey))
		{
			if(DownKey_Pressed != null)
				DownKey_Pressed(MoveDirection.Down);
		}
		if(Input.GetKey(LeftKey))
		{
			if(LeftKey_Pressed != null)
				LeftKey_Pressed(MoveDirection.Left);
		}
		if(Input.GetKey(RightKey))
		{
			if(RightKey_Pressed != null)
				RightKey_Pressed(MoveDirection.Right);
		}
		if(Input.GetKey(ActionKey))
		{
			if(ActionKey_Pressed != null)
				ActionKey_Pressed(MoveDirection.Idle);
		}

		//Key released
		if(!Input.GetKey(UpKey) && !Input.GetKey(DownKey))
		{
			if(UpDownKeys_Released != null)
				UpDownKeys_Released(MoveDirection.Idle);
		}


		if(!Input.GetKey(LeftKey) && !Input.GetKey(RightKey))
		{
			if(LeftRightKeys_Released != null)
				LeftRightKeys_Released(MoveDirection.Idle);
		}

		if(!Input.GetKey(ActionKey))
		{
			if(ActionKey_Released != null)
				ActionKey_Released(MoveDirection.Idle);
		}
	}
}
