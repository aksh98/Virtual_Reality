//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//
//public class Controller : MonoBehaviour {
//
//	public InstantTracker 
//
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
using UnityEngine.UI;
using Wikitude;

public class Controller : MonoBehaviour {

	public InstantTracker Tracker;
	private float heightAboveGround = 1.0f;
	private GridRenderer grid;
	private bool isTracking = false;
	public Button trackingControl;
	public GameObject zombiePrefab;

	public void Awake()
	{
		grid = GetComponent<GridRenderer>();     
	}

	void Update () {
		if(!isTracking) return;

		if (Input.touchCount == 2 && Input.GetTouch(0).phase == TouchPhase.Began) 
		{
			GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			sphere.transform.position = Camera.main.transform.position;
			sphere.AddComponent<Rigidbody>();
			sphere.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward*100);
			sphere.tag = "bullet";

			Destroy(sphere, 5);
		}
		else if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began) 
		{
			var cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
			Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
			float touchPos;
			if (groundPlane.Raycast(cameraRay, out touchPos)) 
			{
				Vector3 position = cameraRay.GetPoint(touchPos);
				Instantiate(zombiePrefab, position, Quaternion.identity);
			}
		}
	}

	public void StartTracker()
	{
		isTracking = !isTracking;
		if(isTracking)
			Tracker.SetState(InstantTrackingState.Tracking);
		else
			Tracker.SetState(InstantTrackingState.Initializing);
	}

	public virtual void OnErrorLoading(int errorCode, string errorMessage) {
		Debug.LogError("Error loading URL Resource!\nErrorCode: " + errorCode + "\nErrorMessage: " + errorMessage);
	}

	// Tracker events
	public virtual void OnTargetsLoaded() {
		Debug.Log("Targets loaded successfully.");
	}

	public void OnEnterFieldOfVision(string target) {
		Debug.Log("Enter Field Of Vision");
		SetScene(true);
	}

	public void OnExitFieldOfVision(string target) {
		Debug.Log("\nExit Field Of Vision");
		SetScene(false);
	}

	public void OnStateChanged(InstantTrackingState newState) {
		Debug.Log("\nState Changes to " + newState);

		Tracker.DeviceHeightAboveGround = heightAboveGround;

		if(newState == InstantTrackingState.Tracking)
		{
			trackingControl.GetComponent<Image>().color = Color.green;
			isTracking = true;
			grid.enabled = false;
		}

		else if(newState == InstantTrackingState.Initializing)
		{
			trackingControl.GetComponent<Image>().color = Color.red;
			isTracking = false;
			grid.enabled = true;
		}
	}

	private void SetScene(bool a)
	{


		GameObject[] zombies = GameObject.FindGameObjectsWithTag("zombie");

		foreach (GameObject z in zombies)
		{
		
			Renderer[] rends = z.GetComponentsInChildren<Renderer>();

			foreach(Renderer r in rends)
				r.enabled = a;
		}

		GameObject[] bullets = GameObject.FindGameObjectsWithTag("bullet");
	
		foreach (GameObject b in bullets)
		{
			b.GetComponent<Renderer>().enabled = a;
		}
	}
}
