using UnityEngine;
using System.Collections;

public class RocketController : MonoBehaviour {

	// Use this for initialization
	GameManager gameManager;
	public float maxSpeed = 10f;
	Rigidbody2D rigid;
	void Start () {
		gameManager = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager>();
		rigid = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "RocketDestroyer") {
			Destroy (gameObject);
		} else if (other.gameObject.tag == "Player") {
			gameManager.isGameOver = true;
		}
	}

	void UpdateMaxSpeed(){ 
		if (rigid.velocity.magnitude > maxSpeed)
			rigid.velocity = rigid.velocity.normalized * maxSpeed;
	}
}
