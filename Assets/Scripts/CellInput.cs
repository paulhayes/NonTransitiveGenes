using UnityEngine;
using System.Collections;

public class CellInput : MonoBehaviour {

	static bool enableOnOver;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void OnMouseDown(){
		Debug.Log ("uninhabitable");
		enableOnOver = renderer.enabled = !renderer.enabled;
	}
	
	public void OnMouseOver(){
		if( Input.GetMouseButton(0) ){
			renderer.enabled = enableOnOver;
		}
	}
}
