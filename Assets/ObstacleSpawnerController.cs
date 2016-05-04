using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObstacleSpawnerController : MonoBehaviour {

	public Object obstacleObjPrefab;
	public Object warningPrefab;	
	float gap = 15f;
	float startTime;
	// Use this for initialization
	void Start () {
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		Spawner ();
	}

	GameObject RandomePipeForRocket(){
		GameObject[] pipeList = GameObject.FindGameObjectsWithTag ("Pipe");
		GameObject pipe;
		List<GameObject> availableList = new List<GameObject> ();
		foreach (GameObject go in pipeList) {
			PipeController controller = go.GetComponent<PipeController> ();
			if (controller.rocket == null && go.transform.position.x <= 7) {
				availableList.Add (go);
			}
		}
		int index = Random.Range (0, availableList.Count);
		pipe = availableList [index];

		return pipe;
	}

	void Spawner(){
		if (Time.time - startTime >= gap) {
			startTime = Time.time;
			gap = Random.Range (1,4);

			GameObject rocket = GameObject.Instantiate (obstacleObjPrefab) as GameObject;
			GameObject targetPipe = RandomePipeForRocket ();

			//get size of pipe 

			float offset = targetPipe.GetComponent<SpriteRenderer>().bounds.size.x/2;

			int index = Random.Range (0, 2);

			if (index > 0) {
				rocket.transform.position = new Vector2 (targetPipe.transform.position.x - offset, 14);
			} else {
				rocket.transform.position = new Vector2 (targetPipe.transform.position.x + offset, 14);
			}

			rocket.transform.parent = targetPipe.transform;
			targetPipe.GetComponent<PipeController> ().rocket = rocket;
			GameObject warningObj = GameObject.Instantiate (warningPrefab) as GameObject;
			warningObj.transform.position = new Vector2 (rocket.transform.position.x, 4);
			warningObj.transform.parent = targetPipe.transform;
			Destroy (warningObj, 1.5f);
		}
	}
}
