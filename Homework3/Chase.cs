﻿//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//
//public class Chase : MonoBehaviour {
//
//	// Use this for initialization
//	void Start () {
//		
//	}
//	
//	// Update is called once per frame
//	void Update () {
//		
//	}
//}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : MonoBehaviour {

	float rotationSpeed = 1;
	Animator anim;

	void Start()
	{
		anim = this.GetComponent<Animator>();
	}

	// Use this for initialization
	void OnTriggerEnter(Collider collider) 
	{
		if(collider.gameObject.tag == "bullet")
		{
			anim.SetTrigger("hit");
		
			Destroy(collider.gameObject);

			this.GetComponent<Collider>().enabled = false;
			Destroy(this.gameObject,5);
		}
	}

	// Update is called once per frame
	void Update () 
	{
	
		Vector3 direction = Camera.main.transform.position - this.transform.position;

		direction.y = 0;

		this.transform.rotation = Quaternion.Slerp (this.transform.rotation, Quaternion.LookRotation(direction),
			rotationSpeed * Time.deltaTime);
	}
}
