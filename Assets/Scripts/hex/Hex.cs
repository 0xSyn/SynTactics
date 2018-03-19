using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hex : MonoBehaviour {
    public Sprite[] hexState;
	// Use this for initialization
	void Start () {
		GetComponentInChildren<SpriteRenderer>().sprite=hexState[2];
	}
	public void HexState(int state){
        GetComponentInChildren<SpriteRenderer>().sprite=hexState[state];
    }
    void OnMouseDown(){
        Debug.Log("Clicked: ");
    }


	// Update is called once per frame
	void Update () {

    }
}

