using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour 
{
	public delegate void PlayerEvent ();

	public event PlayerEvent GameOverEvent;

	private const float MAXVELOCITY = 0.5f;
	private const float HUNGERTIME = 2.0f;

	public float survivalTime = 0;

	//Exposing player movement variables to be modified at runtime in Unity
	public float currentvelocityX = 0;
	public float currentvelocityY = 0;
	public float acceleration = 2;
	public float deceleration = 2;

	public int hunger = 100;
	private float hungerTimer = 0;

	private bool isHungerTimerRunning = true;

	private InputManager.MoveDirection currentDirection = InputManager.MoveDirection.Idle;

	// Use this for initialization
	void Start () 
	{
		//Register to InputManager events
		InputManager.Instance.UpKey_Pressed += PlayerMove;
		InputManager.Instance.DownKey_Pressed += PlayerMove;
		InputManager.Instance.LeftKey_Pressed += PlayerMove;
		InputManager.Instance.RightKey_Pressed += PlayerMove;
		InputManager.Instance.ActionKey_Pressed += PlayerAction;

		InputManager.Instance.UpDownKeys_Released += ApplyDecelerationY;
		InputManager.Instance.LeftRightKeys_Released += ApplyDecelerationX;

		SpawnManager.Instance.AssembleMob();
	}

	// Update is called once per frame
	void Update () 
	{
		if(hunger > 0)
			survivalTime += Time.deltaTime;

		SpawnManager.Instance.Update();
		InputManager.Instance.Update();
		UpdateHunger();
	}

	private void UpdateHunger()
	{
		if (hunger < 0)
			hunger = 0;

		if(isHungerTimerRunning)
		{
			hungerTimer += Time.deltaTime;

			if(hungerTimer >= HUNGERTIME)
			{
				if(hunger > 0)
					hunger -= 10;
				else
				{
					isHungerTimerRunning = false;

					if(GameOverEvent != null)
						GameOverEvent();
				}

				hungerTimer = 0;
			}
		}
	}

	private void PlayerMove(InputManager.MoveDirection movementdir)
	{
		Vector2 newPlayerPos = gameObject.transform.position;

		int directionModifier = 0;

		currentDirection = movementdir;

		if (movementdir == InputManager.MoveDirection.Up || movementdir == InputManager.MoveDirection.Down) 
		{
			if(movementdir == InputManager.MoveDirection.Up)
				directionModifier = 1;
			else
				directionModifier = -1;

			if (currentvelocityY <= MAXVELOCITY) 
				currentvelocityY += acceleration * Time.deltaTime;

			newPlayerPos.y += currentvelocityY * directionModifier;
		}
			
		if (movementdir == InputManager.MoveDirection.Left || movementdir == InputManager.MoveDirection.Right) 
		{
			if(movementdir == InputManager.MoveDirection.Left)
				directionModifier = -1;
			else
				directionModifier = 1;

			if (currentvelocityX <= MAXVELOCITY) 
				currentvelocityX += acceleration * Time.deltaTime;

			newPlayerPos.x += currentvelocityX * directionModifier;
		}

		gameObject.transform.position = newPlayerPos;
	}

	private void ApplyDecelerationX()
	{
		Vector2 newPlayerPos = gameObject.transform.position;

		int directionModifier = 0;

		if(currentDirection == InputManager.MoveDirection.Left || currentDirection == InputManager.MoveDirection.Right)
		{
			if(currentDirection == InputManager.MoveDirection.Left)
				directionModifier = -1;
			else
				directionModifier = 1;

			if(currentvelocityX > 0)
				currentvelocityX -= deceleration * Time.deltaTime;
			else
				currentvelocityX = 0;

			newPlayerPos.x += currentvelocityX * directionModifier;
		}
		
		gameObject.transform.position = newPlayerPos;
	}

	private void ApplyDecelerationY()
	{
		Vector2 newPlayerPos = gameObject.transform.position;
		
		int directionModifier = 0;

		if(currentDirection == InputManager.MoveDirection.Up || currentDirection == InputManager.MoveDirection.Down)
		{
			if(currentDirection == InputManager.MoveDirection.Up)
				directionModifier = 1;
			else
				directionModifier = -1;
			
			if(currentvelocityY > 0)
				currentvelocityY -= deceleration * Time.deltaTime;
			else
				currentvelocityY = 0;
			
			newPlayerPos.y += currentvelocityY * directionModifier;
		}

		gameObject.transform.position = newPlayerPos;
	}

	private void PlayerAction()
	{
		//If player is right next to an enemy, do a size comparison.
		GameObject target = GetClosestEnemy ();

		if(target != null && hunger > 0)
		{
			Renderer playerRenderer = gameObject.transform.FindChild("Body").GetComponent<Renderer>();
			Renderer targetRenderer = target.transform.FindChild("Body").GetComponent<Renderer>();

			if(playerRenderer != null && targetRenderer != null)
			{
				if(playerRenderer.bounds.Intersects(targetRenderer.bounds))
				{
					float playerCombinedSize = playerRenderer.bounds.size.x + playerRenderer.bounds.size.y + playerRenderer.bounds.size.z;
					float targetCombinedSize = targetRenderer.bounds.size.x + targetRenderer.bounds.size.y + targetRenderer.bounds.size.z;

					//If player is larger than the enemy, consume them. Don't consume enemies if at max hunger. Increase player size.
					if(playerCombinedSize >= targetCombinedSize && hunger < 100)
					{
						//Replenish hunger
						if(hunger < 100)
							hunger += 10;

						gameObject.transform.localScale += target.transform.localScale/8;
						GameObject.Destroy(target);
					}
				}
			}
		}

	}

	private GameObject GetClosestEnemy()
	{
			GameObject[] enemyArray = GameObject.FindGameObjectsWithTag ("Enemy");
			GameObject closestEnemy = null;

			float shortestDist = Mathf.Infinity;
			float currentDist = 0;
			
			for (int i = 0; i < enemyArray.Length; i++) 
			{
				currentDist = Vector3.Distance(gameObject.transform.position, enemyArray[i].transform.position);

				if(currentDist < shortestDist)
				{
					shortestDist = currentDist;
					closestEnemy = enemyArray[i];
				}
			}

			return closestEnemy;
	}

}
