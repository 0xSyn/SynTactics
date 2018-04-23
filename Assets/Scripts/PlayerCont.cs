using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class PlayerCont : NetworkBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    if (!isLocalPlayer) {
	        return;           
        }

	    if (Input.GetKey(KeyCode.UpArrow)) {
	        transform.position += transform.up*Time.deltaTime;
	    }
	    if (Input.GetKey(KeyCode.DownArrow)) {
	        transform.position -= transform.up * Time.deltaTime;
	    }
	    if (Input.GetKey(KeyCode.RightArrow)) {
	        transform.position += transform.right * Time.deltaTime;
	    }
	    if (Input.GetKey(KeyCode.LeftArrow)) {
	        transform.position -= transform.right * Time.deltaTime;
	    }
    }
}
