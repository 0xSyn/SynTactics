using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour {
    private string hexId="hex0_0";
    public static Ray ray;
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
        ray =Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit)){
            
            //Debug.Log("3d raycast at: "+hit.collider.gameObject.name);
            //GameObject.Find(hit.collider.gameObject.name).GetComponentInChildren<SpriteRenderer>().sprite=Resources.Load("Sprites/hex_empty64", typeof(Sprite)) as Sprite;

            //Hover
            if (hexId!=hit.collider.gameObject.name) {
                GameObject.Find(hexId).SendMessage("HexState",2, SendMessageOptions.DontRequireReceiver);
                hit.transform.SendMessage("HexState",1, SendMessageOptions.DontRequireReceiver);
                hexId=hit.collider.gameObject.name;
            }
        }
    }
}
