using UnityEngine;
using System.Collections;
using System;

[System.Serializable]
public class Boundary {
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {
	public float speed;
	public Boundary boundary;
	public float tilt;
	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;

	private float nextFire = 0.0F;
	private bool doubleFire=false;
	private float expireTime;
	private float originalFireRate;

	void Start() {
		originalFireRate = fireRate;
	}

	void Update() {
		if (doubleFire) {
			if (Time.time > expireTime) {
				fireRate = originalFireRate;
				doubleFire = false;
			}
		}

		if (Helpers.FireButtonDown() && Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			// GameObject clone =
			Instantiate (shot, shotSpawn.position, shotSpawn.rotation); // as GameObject;

			audio.Play ();
		}

	}

	public void DoubleFire() {
		fireRate = .1f;
		doubleFire = true;
		expireTime = Time.time + 3;
	}

	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		float horizontalTilt = (float) Math.Round((float) Input.acceleration.x, 2);
		float verticalTilt = (float) Math.Round((float) Input.acceleration.y, 2);

		if (verticalTilt != 0) {
			verticalTilt = 1 + (verticalTilt * 2);
			verticalTilt = Mathf.Clamp (verticalTilt, -1, 1);
		}
//		Debug.Log("moveHorizontal: " + moveHorizontal + 
//		          "moveVertical: " + moveVertical +
//		          "horizontalTilt: " + horizontalTilt +
//		          "verticalTilt: " + verticalTilt);


		moveHorizontal += horizontalTilt;
		moveVertical += verticalTilt;

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rigidbody.velocity = movement * speed;

		rigidbody.position = new Vector3(
			Mathf.Clamp (rigidbody.position.x, boundary.xMin, boundary.xMax),
			0.0f,
			Mathf.Clamp(rigidbody.position.z, boundary.zMin, boundary.zMax)
			);

		rigidbody.rotation = Quaternion.Euler (0.0f, 0.0f, rigidbody.velocity.x * -tilt);
	}
}
