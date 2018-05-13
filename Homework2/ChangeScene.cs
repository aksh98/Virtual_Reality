using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ChangeScene : MonoBehaviour {

//	// Use this for initialization
//	void Start () {
//		
//	}
	
	// Update is called once per frame
//	void Update () {
//		
//	}

	public void ChangetoScene(int newscene) 
	{
		SceneManager.LoadScene (newscene);

	}

//	public void Hover(){
//		GetComponent<TextMesh> ().color = startColor;
//	
//	}
}
