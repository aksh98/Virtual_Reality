using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour {

	public GameObject coin;
	float randX;
	float randZ;
	public int count = 1;
	float yaxis = 2.5f;
	//Vector3 location;
	public float spawnRate = 2f;
	float nextSpawn = 0.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		ScoreManager scoremanager = GetComponent<ScoreManager> ();

		if (/*Time.time > nextSpawn && */ ScoreManager.score > 50*count ) {
			//nextSpawn = Time.time + spawnRate;
			count = count + 1;
			coin.SetActive (true);
//			randX = Random.Range (-15, 20);
//			randZ = Random.Range (-15, 10);
			Vector3 location = new Vector3 (Random.Range (-15, 10), -2, Random.Range (-15, 20));
			Instantiate (coin, location + transform.TransformPoint(0,0,0), gameObject.transform.rotation);
		}
			
	}
}
