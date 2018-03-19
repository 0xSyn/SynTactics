using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
    Vector3 oldPosition;

	// Use this for initialization
	void Start () {
		oldPosition=this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 camPos=transform.position;
        Vector3 playerPos=GameObject.Find("PlayerCharacter").transform.position;
        Vector3 mPos=Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,Camera.main.nearClipPlane+3));
        
        

        //camPos.x=playerPos.x;
        camPos.z=playerPos.z;
		camPos.x=(playerPos.x+mPos.x)/2;
        camPos.z=(playerPos.z+mPos.z)/2;
        transform.position=camPos;

        //Debug.Log("pPos:"+playerPos+"\nmPos:"+mPos);
       // Debug.Log("cPos:"+camPos);
        
	}








    public void PanToHex() {

    }
    void CheckIfCamMoved() {
        if(oldPosition!=this.transform.position) {
            oldPosition=this.transform.position;
        }
    }
}
