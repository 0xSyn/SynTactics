using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    Animator _animator;
    float spd=3.0f;
    // Use this for initialization

    private void Start () {
	    _animator = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	private void Update () {
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * spd;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * spd;
	    _animator.SetFloat("Horizontal",x);
	    _animator.SetFloat("Vertical",z);
        GetComponentInChildren<SpriteRenderer>().flipX = x>0;
        Debug.Log("Horiz:"+x+"\nVert:"+z);
        transform.Translate(x, 0, z);
        //transform.Translate(0, 0, z);
		if (Input.GetKeyDown("space")) {
            print("Move Right");
            transform.Translate(.01f, 0, 0);
		}
	}
}
