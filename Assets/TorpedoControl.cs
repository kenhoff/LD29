using UnityEngine;
using System.Collections;

public class TorpedoControl : MonoBehaviour {

	public float TorpedoThrustForce = 1.0f;
	public float TorpedoFuseTime = 2.0f;

	public GameObject Explosion;
	public float ExplosionDuration = 1.0f;

	public float ExplosionRadius = 5.0f;
	public float ExplosionDamage = 5.0f;
	public float ExplosionForce = 5.0f;

	private float RemainingFuse;

	// Use this for initialization
	void Start () {
		RemainingFuse = TorpedoFuseTime;
	}

	void Update () {
		RemainingFuse -= Time.deltaTime;
		if (RemainingFuse <= 0.0f) {
			BlowUp();
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		// torpedo facing stuff - can do later
		//Vector3 facingVec = new Vector3(transform.position) + new Vector3(rigidbody2D.velocity);

		//transform.LookAt(facingVec, Vector3.forward);
	
		rigidbody2D.AddForce(transform.up * TorpedoThrustForce * Time.deltaTime);

	}

	void OnCollisionEnter2D() {
		BlowUp();
	}

	void BlowUp() {

		Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, ExplosionRadius);

		for (int i = 0; i < hitColliders.Length; i++) {
			//Debug.Log(hitColliders[i].gameObject.tag);
			float dist = (hitColliders[i].transform.position - transform.position).magnitude;
			float distMultiplier = (ExplosionRadius - dist) / ExplosionRadius;
			if (hitColliders[i].gameObject.tag == "Player") {
				//distMultiplier = distMultiplier / 10;
			}
			hitColliders[i].rigidbody2D.AddForce(-(transform.position - hitColliders[i].transform.position).normalized * ExplosionForce * distMultiplier * Time.deltaTime);
			if (hitColliders[i].gameObject.tag == "Shark") {	
				hitColliders[i].gameObject.GetComponent<SharkControl>().Damage(ExplosionDamage * distMultiplier);
			}
		}

		GameObject explosion = Instantiate(Explosion, transform.position, Quaternion.identity) as GameObject;
		explosion.transform.localScale = Vector3.one * ExplosionRadius * 2;
		Destroy(explosion, ExplosionDuration);
		Destroy(gameObject);
	}

}
