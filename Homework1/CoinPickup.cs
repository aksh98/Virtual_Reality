using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag ("Pick Up")) {
			gameObject.SetActive (false);
		}
		if (other.gameObject.CompareTag ("Damage")) {
			gameObject.SetActive (false);
		}
		if (other.gameObject.CompareTag ("")) {
			gameObject.SetActive (false);
		}
	}
}
