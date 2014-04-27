using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	public float CameraMoveSpeed = 0.05f;

	public float TurnForce = 0.25f;
	public float ThrustForce = 1.0f;

	public GameObject Bubbles;
	private ParticleSystem particles;

	public float TorpedoLaunchForce = 1000.0f;
	public float TimeBetweenTorpedoes = 1.0f;

	public GameObject Torpedo;

	private float TimeSinceLastTorpedo = 1000.0f;

	public float MaxHealth = 100.0f;
	public float HealthRegenRate = 5.0f;
	public float HealthRegenCooldown = 3.0f;
	private float currentHealth;
	private float timeSinceLastDamage = 1000.0f;

	// Use this for initialization
	void Start () {
		currentHealth = MaxHealth;
		particles = Bubbles.particleSystem;
	}
	
	// Update is called once per frame
	void Update () {
		updateHealth();
		updateRendering();
	}

	void updateRendering () {
		if (transform.up.x > 0) {
			transform.localScale = new Vector3(1, 1, 1);
		}
		else {
			transform.localScale = new Vector3(-1, 1, 1);
		}
	}

	public void Damage (float amount = 5.0f) {
		currentHealth -= amount;
		timeSinceLastDamage = 0.0f;
	}

	void updateHealth () {
		Debug.Log(currentHealth);
		timeSinceLastDamage += Time.deltaTime;
		if (timeSinceLastDamage > HealthRegenCooldown) {
			currentHealth += Time.deltaTime * HealthRegenRate;
		}

		if (currentHealth > MaxHealth) {
			currentHealth = MaxHealth;
		}

		if (currentHealth <= 0) {
			Die();
		}
	}

	void Die() {
		Application.LoadLevel(0);
		//Destroy(gameObject);
	}

	void FixedUpdate () {
		updateFacing();
		cameraFollow();
		applyThrust();
		shoot();
	}

	void shoot () {
		if (Input.GetMouseButton(0) && (TimeSinceLastTorpedo >= TimeBetweenTorpedoes)) {
			Vector3 front_pos = transform.position + (transform.up * 1.1f);
			GameObject torpedo = Instantiate(Torpedo, front_pos, transform.rotation) as GameObject;
			torpedo.rigidbody2D.velocity = rigidbody2D.velocity;
			torpedo.rigidbody2D.AddForce(transform.up * Time.deltaTime * TorpedoLaunchForce);
			TimeSinceLastTorpedo = 0.0f;
		}
		TimeSinceLastTorpedo += Time.deltaTime;
	}

	void cameraFollow () {
	}

	void applyThrust () {
		if (Input.GetMouseButton(1)) {
			rigidbody2D.AddForce(transform.up * ThrustForce * Time.deltaTime);
			particles.Emit(1);
		}
	} 

	void updateFacing () {
		Vector3 mousePos = Input.mousePosition;
		mousePos.z = -Camera.main.transform.position.z;
		Vector3 target = Camera.main.ScreenToWorldPoint(mousePos);

		Vector3 pointBetweenPlayerAndTarget = (transform.position + target) / 2;

		Vector3 dest = new Vector3(pointBetweenPlayerAndTarget.x, pointBetweenPlayerAndTarget.y, Camera.main.transform.position.z);
		Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, dest, CameraMoveSpeed);

		//Debug.Log(target);

		float player_angle = -transform.eulerAngles.z;

		Vector3 diff = target - transform.position;

		float diff_angle = Mathf.Atan2(diff.x, diff.y) * Mathf.Rad2Deg;

		float change_heading = Mathf.DeltaAngle(player_angle, diff_angle);
		//Debug.Log((180 - Mathf.Abs(change_heading))/180);
		float torque_force = TurnForce * Mathf.Abs(change_heading);

		if (change_heading > 0) {
			torque_force = torque_force * -1;
		}

		rigidbody2D.AddTorque(torque_force * Time.deltaTime);
	}
}
