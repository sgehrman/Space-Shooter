using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour
{
	public GameObject explosion;
	public GameObject playerExplosion;
	public int scoreValue;
	public Transform gameOverEffects;
	private GameController gameController;
	private PlayerController playerController;

	void Start ()
	{
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController> (); 
		}
		
		if (gameController == null) {
			Debug.Log ("Cannot find GameController");
		}

		GameObject playerObject = GameObject.FindWithTag ("Player");
		if (playerObject == null) {
			Debug.Log ("Cannot find playerObject");
		}

		if (playerObject != null) {
			playerController = playerObject.GetComponent<PlayerController> (); 
		}
		
		if (playerController == null) {
			Debug.Log ("Cannot find playerController");
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Boundary") {
			return;
		}
		Instantiate (explosion, transform.position, transform.rotation);

		if (gameObject.tag == "Asteroid2") {
			playerController.DoubleFire ();
		}

		if (other.tag == "Player") {
			Instantiate (playerExplosion, other.gameObject.transform.position, other.gameObject.transform.rotation);

			gameController.GameOver ();

			Instantiate (gameOverEffects, new Vector3 (0, 0, 0), Quaternion.identity);

		}

		gameController.AddScore (scoreValue);
		Destroy (other.gameObject);
		Destroy (gameObject);
	}

}
