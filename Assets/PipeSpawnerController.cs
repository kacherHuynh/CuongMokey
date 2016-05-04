using UnityEngine;
using System.Collections;

public class PipeSpawnerController : MonoBehaviour {

	public Object pipePrefab;
	public float gapTime;
	float startTime;
	// Use this for initialization

	void Start () {
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		Spawn ();
	}

	void Spawn(){ 
		if (Time.time - startTime >= gapTime) {
			startTime = Time.time;
			GameObject pipe = GameObject.Instantiate (pipePrefab) as GameObject;
			int coinIndex = Random.Range (1, 3);
			GameObject coins = pipe.transform.FindChild (coinIndex.ToString ()).gameObject;
			coins.SetActive (true);
			float maxY = transform.position.y;
			float pipeY = Random.Range (maxY - 3, maxY);
			pipe.transform.position = new Vector2 (transform.position.x, pipeY);
		}
	}
}
