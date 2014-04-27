using UnityEngine;
using System.Collections;

public class DeadSharkControl : MonoBehaviour {

	public float SharkTurnForce = 10.0f;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		adjustHeading(transform.position + Vector3.up);	
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
}
