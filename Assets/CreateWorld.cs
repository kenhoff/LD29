using UnityEngine;
using System.Collections;

public class CreateWorld : MonoBehaviour {

	public GameObject Player;

	public GameObject WorldBound;

	public float WorldWidth = 100;
	public float WorldHeight = 100;

	public GameObject SeaParticle;

	public int SeaParticleCount = 100;
	public float SeaParticleBackDepth = 20;
	public float SeaParticleFrontDepth = 10;

	public GameObject Shark;

	public float TimeBetweenSharkSpawns = 5;
	public int MaxSharks = 10;
	public float SharkSpawnDistFromPlayer = 50;
	private float TimeSinceLastSharkSpawn = 0.0f;
	public float DifficultyScalar = 10.0f;


	private GameObject player;

	public float CameraBoundingDistance = 8.0f;

	// Use this for initialization
	void Start () {
		player = Instantiate(Player) as GameObject;
		//createSeaParticles();	
		//createWorldBounds();
	}
	
	// Update is called once per frame
	void Update () {
		if (TimeSinceLastSharkSpawn > TimeBetweenSharkSpawns) {
			SpawnShark();
			TimeSinceLastSharkSpawn = 0;
		}
		TimeSinceLastSharkSpawn += Time.deltaTime;
		boundCamera();
		MaxSharks = (int) (DifficultyScalar * Time.realtimeSinceStartup);
		Debug.Log("Sharks: " + MaxSharks);

	}

	void boundCamera () {
		// vertical
		if (Camera.main.transform.position.y < (-WorldHeight/2 + CameraBoundingDistance)) {
			Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, (-WorldHeight/2) + CameraBoundingDistance, Camera.main.transform.position.z);
		}
		if (Camera.main.transform.position.y > (WorldHeight/2 - CameraBoundingDistance)) {
			Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, (WorldHeight/2) - CameraBoundingDistance, Camera.main.transform.position.z);
		}
		// horizontal
		if (Camera.main.transform.position.x < (-WorldWidth/2 + CameraBoundingDistance)) {
			Camera.main.transform.position = new Vector3((-WorldWidth/2) + CameraBoundingDistance, Camera.main.transform.position.y, Camera.main.transform.position.z);
		}
		if (Camera.main.transform.position.x > (WorldWidth/2 - CameraBoundingDistance)) {
			Camera.main.transform.position = new Vector3((WorldWidth/2) - CameraBoundingDistance,Camera.main.transform.position.y, Camera.main.transform.position.z);
		}
	}

	void SpawnShark () {
		// on unit circle * 100 away from player - if beyond world bounds in x or y direction, flip sign
		// spawn 1 shark every 5 seconds until 10 sharks
		GameObject[] sharks = GameObject.FindGameObjectsWithTag("Shark");
		if (sharks.Length < MaxSharks) {
			Vector2 unitCircleLocation = Random.insideUnitCircle.normalized * SharkSpawnDistFromPlayer;
			Vector2 playerPos = player.transform.position;


			if (((unitCircleLocation + playerPos).x >= WorldWidth/2) || ((unitCircleLocation + playerPos).x <= -WorldWidth/2)) {
				unitCircleLocation = new Vector2(-unitCircleLocation.x, unitCircleLocation.y);
			}
			if (((unitCircleLocation + playerPos).y >= WorldHeight/2) || ((unitCircleLocation + playerPos).y <= -WorldHeight/2)) {
				unitCircleLocation = new Vector2(unitCircleLocation.x, -unitCircleLocation.y);
			}

			Vector2 finalSpawnPoint = unitCircleLocation + playerPos;

			Instantiate(Shark, finalSpawnPoint, Quaternion.identity);
		}


	}

	void createWorldBounds () {

		for (float i = -WorldWidth/2; i < WorldWidth/2; i++) {
			GameObject bound = Instantiate(WorldBound, new Vector3(i, -WorldHeight/2, 0), Quaternion.identity) as GameObject;
			bound.transform.parent = transform;
			GameObject bound4 = Instantiate(WorldBound, new Vector3(i, WorldHeight/2, 0), Quaternion.identity) as GameObject;
			bound4.transform.parent = transform;
		}

		for (float i = -WorldHeight/2; i < WorldHeight/2; i++) {
			GameObject bound2 = Instantiate(WorldBound, new Vector3(WorldWidth/2, i, 0), Quaternion.identity) as GameObject;
			bound2.transform.parent = transform;
			GameObject bound3 = Instantiate(WorldBound, new Vector3(-WorldWidth/2, i, 0), Quaternion.identity) as GameObject;
			bound3.transform.parent = transform;
		}

	}

	void createSeaParticles () {
		for (int i = 0; i < SeaParticleCount; i++) {
			GameObject particle = Instantiate(SeaParticle, new Vector3(Random.Range(-WorldWidth/2, WorldWidth/2), Random.Range(-WorldHeight/2, WorldHeight/2), Random.Range(-SeaParticleFrontDepth, SeaParticleBackDepth)), Quaternion.identity) as GameObject;
			particle.transform.parent = transform;
			particle.renderer.material.color = new Color(Random.Range(0, 0.25f), Random.value, 1, 0.5f);
			particle.rigidbody2D.velocity = Random.insideUnitCircle;
		}
	}

}


