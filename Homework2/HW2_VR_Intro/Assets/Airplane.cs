//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//
//public class Airplane : MonoBehaviour {
//
//	// Use this for initialization
//	void Start () {
//		
//	}
//	
//	// Update is called once per frame
//	void Update () {
//		if(Input.GetKey(
//	}
//}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Airplane : MonoBehaviour {

	bool flag = false;

	Camera cockpit;
	public float maxSpeed = 10f;
	float speed = 3f;
	// Use this for initialization
	void Start () 
	{
		cockpit = GetComponentInChildren<Camera> ();	
	}

	// Update is called once per frame
	void Update () {
		if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || (Input.GetMouseButtonDown(0)))
		{
			flag = true;
			print ("heylo");
		}
		if (flag == true) 
		{
			transform.position += cockpit.transform.forward * speed * Time.deltaTime;
		
			if (speed < maxSpeed)
			{
				speed += .5f;
			}
		}
		if (transform.eulerAngles.z >= 45f && transform.eulerAngles.z <= 315f)
		{
			SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
		}
	}
}