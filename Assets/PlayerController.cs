using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	// Use this for initialization
	public bool isJumping = false;
	public bool isBeginning = true;
	float maxSpeed = 10f;
	public Text scoreLable;
	GameManager gameManager; 
	Renderer rend;
	SpriteRenderer spriteRender;

	Rigidbody2D rigid;
	void Awake () {
		rigid = GetComponent<Rigidbody2D> ();
		rend = GetComponent<Renderer> ();
		spriteRender = GetComponent<SpriteRenderer> ();
		gameManager = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame

	void Update(){
		// JUMP BUTTON
		if ((CrossPlatformInputManager.GetButtonDown ("Jump") && transform.parent != null) || (CrossPlatformInputManager.GetButtonDown ("Jump") && isBeginning)) {
			if (isBeginning) {
				isBeginning = false;
				rigid.gravityScale = 1;
				isJumping = true;
			} else {
				spriteRender.flipX = !spriteRender.flipX;
				rigid.gravityScale = 1;
				isJumping = true;
			}
		}

		// SIWTCH BUTTON 
		if ((CrossPlatformInputManager.GetButtonDown ("Switch") && transform.parent != null) || (Input.GetKeyDown(KeyCode.LeftShift) && transform.parent != null)) {
			Debug.Log ("SWITCH");
			if (IsFacingToRight ()) {
				spriteRender.flipX = true;
				transform.position = new Vector3 (transform.position.x + rend.bounds.size.x , transform.position.y, transform.position.z);
				Debug.Log ("right");
			} else {
				spriteRender.flipX = false;
				transform.position = new Vector3 (transform.position.x - rend.bounds.size.x , transform.position.y, transform.position.z);
				Debug.Log ("left");
			}
		}


		// UP BUTTON
		if ((Input.GetKeyDown (KeyCode.Space) && transform.parent != null) || (CrossPlatformInputManager.GetButtonDown("Up") && transform.parent != null)){
			float currentSpeed = rigid.velocity.magnitude;
			rigid.AddForce (Vector2.up * 150);
		}
	}

	void FixedUpdate () {

		// CHECK CONDITION AND UPDATE EVERY FARM

		if (isJumping) {
			if (IsFacingToRight ()) {
				transform.parent = null;
				Vector2 destination = new Vector2 (transform.position.x + 25, transform.position.y + 50);
				transform.position = Vector2.Lerp (transform.position, destination, 0.1f * Time.deltaTime);
			} else {
				transform.parent = null;
				Vector2 destination = new Vector2 (transform.position.x - 50, transform.position.y + 50);
				transform.position = Vector2.Lerp (transform.position, destination, 0.1f * Time.deltaTime);
			}
		}

		// Limit Player Speed
		if(rigid.velocity.magnitude > maxSpeed)
			rigid.velocity = rigid.velocity.normalized * maxSpeed;
	}

	public bool IsFacingToRight(){ 
		if (spriteRender.flipX)
			return false;
		return true;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "LoseBorder") {
			gameManager.isGameOver = true;
		} else if (other.gameObject.tag == "Coin") {
			Destroy (other.gameObject);
			int score = int.Parse (scoreLable.text);
			score++;
			scoreLable.text = score.ToString ();
		}
	}
}
