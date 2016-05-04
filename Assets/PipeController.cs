using UnityEngine;
using System.Collections;

public class PipeController : MonoBehaviour {

	// Use this for initialization
	public GameObject player;
	public GameObject rocket;
	PlayerController playerController;

	void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player");
		playerController = player.GetComponent<PlayerController> ();

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Move ();
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {

			player.GetComponent<Rigidbody2D> ().velocity = Vector3.zero;
			player.GetComponent<Rigidbody2D> ().angularVelocity = 0f;
			playerController.isJumping = false;

			// this is for making player stick to pipe
			var playerTransform = other.gameObject.transform;
			playerTransform.parent = transform;

		} else if (other.gameObject.tag == "PipeDestroyer") {
			Destroy (gameObject);
		}
	}

//	void OnCollisionEnter2D(Collision2D coll) {
//		if (coll.gameObject.tag == "Player") {
//
//			player.GetComponent<Rigidbody2D> ().velocity = Vector3.zero;
//			player.GetComponent<Rigidbody2D> ().angularVelocity = 0f;
//			playerController.isJumping = false;
//			playerController.isClampping = true;
//			playerController.canJump = false;
//		} else if (coll.gameObject.tag == "PipeDestroyer"){
//			Destroy (gameObject);
//		}
//	}

	public void Move(){ 
		transform.Translate (Vector3.left * 1f * Time.deltaTime);
	}

}
