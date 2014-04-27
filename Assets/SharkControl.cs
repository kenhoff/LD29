using UnityEngine;
using System.Collections;

public class SharkControl : MonoBehaviour {

	public float SharkTurnForce = 10.0f;

	public float SharkDistanceLimit = 50.0f;

	public float SharkIdleForce = 10.0f;
	public float SharkIdleSwitchTime = 10.0f;

	private enum SharkFacingDirection {
		Left, 
		Right
	};
	private SharkFacingDirection currentFacingDirection;
	private float SharkTimeOnCurrentDirection = 0.0f;

	public float PlayerDetectionRadius = 30.0f;
	public float SharkAttackForce = 4.0f;
	public float SharkAttackDamage = 5.0f;

	public float SharkMaxHealth = 5.0f;
	private float currentHealth;

	public GameObject DeadShark;

	enum AIState {
		Idle,
		Attack
	};

	private AIState currentState;

	private Vector3 currentTarget;

	private GameObject player;

	// Use this for initialization
	void Start () {
		currentHealth = SharkMaxHealth;
		player = GameObject.FindGameObjectsWithTag("Player")[0];
		currentState = AIState.Idle;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//Debug.Log(currentState);
		//Debug.Log((player.transform.position - transform.position).magnitude);
		if ((player.transform.position - transform.position).magnitude <= PlayerDetectionRadius) {
			currentState = AIState.Attack;
		}
		else {
			currentState = AIState.Idle;
		}
		switch (currentState) {
			case AIState.Idle:
				doIdleThings();
				break;
			case AIState.Attack:
				doAttackThings();
				break;
		}
	}

	void doIdleThings () {

		SharkTimeOnCurrentDirection += Time.deltaTime;

		if (SharkTimeOnCurrentDirection > SharkIdleSwitchTime) {
			if (Random.value >= 0.5) {
				currentFacingDirection = SharkFacingDirection.Right;
			}
			else {
				currentFacingDirection = SharkFacingDirection.Left;
			}
			SharkTimeOnCurrentDirection = 0.0f;
		}

		switch (currentFacingDirection) {
			case SharkFacingDirection.Left:
				currentTarget = transform.position + new Vector3(-1, 0, 0);
				break;
			case SharkFacingDirection.Right:
				currentTarget = transform.position + new Vector3(1, 0, 0);
				break;

		}

		rigidbody2D.AddForce(transform.up * SharkIdleForce * Time.deltaTime);

		if (transform.position.x > SharkDistanceLimit) {
			currentFacingDirection = SharkFacingDirection.Left;
		}
		if (transform.position.x < -SharkDistanceLimit) {

			currentFacingDirection = SharkFacingDirection.Right;
		}

		adjustHeading(currentTarget);

		// adjust heading towards target


	}

	void OnCollisionEnter2D (Collision2D collision) {
		if (currentState == AIState.Idle) {
			if (currentFacingDirection == SharkFacingDirection.Right) {
				currentFacingDirection = SharkFacingDirection.Left;
			}
			else {
				currentFacingDirection = SharkFacingDirection.Right;
			}
		}
		if (currentState == AIState.Attack) {
			if (collision.gameObject.tag == "Player") {
				Debug.Log("attacking player");
				collision.gameObject.GetComponent<PlayerControl>().Damage(Time.deltaTime * SharkAttackDamage);
			}
		}
	}



	void doAttackThings () {
		float facingAmount = adjustHeading(player.transform.position);
		rigidbody2D.AddForce(transform.up * SharkAttackForce * facingAmount * Time.deltaTime);
	}

	float adjustHeading (Vector3 target) {

		float player_angle = -transform.eulerAngles.z;

		Vector3 diff = target - transform.position;

		float diff_angle = Mathf.Atan2(diff.x, diff.y) * Mathf.Rad2Deg;

		float change_heading = Mathf.DeltaAngle(player_angle, diff_angle);

		float torque_force = SharkTurnForce * Mathf.Abs(change_heading);

		if (change_heading > 0) {
			torque_force = torque_force * -1;
		}

		rigidbody2D.AddTorque(torque_force * Time.deltaTime);

		return (180 - Mathf.Abs(change_heading))/180;

	}

	void updateHealth () {
		if (currentHealth <= 0) {
			SharkCounter.IncrementScore();
			GameObject deadShark = Instantiate(DeadShark, transform.position, Quaternion.identity) as GameObject;
			deadShark.rigidbody2D.velocity = rigidbody2D.velocity;
			Destroy(deadShark, 30.0f);
			Destroy(gameObject);
		}
	}

	public void Damage (float amount) {
		currentHealth -= amount;
	}

	void Update () {
		updateHealth();
		updateRendering();
	}

	void updateRendering () {
		if (transform.up.x < 0) {
			transform.localScale = new Vector3(1, 1, 1);
		}
		else {
			transform.localScale = new Vector3(-1, 1, 1);
		}
	}


}
