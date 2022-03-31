using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameQuitListener : MonoBehaviour {

	public bool hitEscape = false;

	// void Awake() {
	// 	DontDestroyOnLoad(this.gameObject);
	// }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update() {
		// if (Input.GetKey ("escape") && hitEscape) {
		// 	Application.Quit ();
		// }
	}

	public void QuitGame() {
		Debug.Log("BEING CALLED");
		Application.Quit ();
	}
}
