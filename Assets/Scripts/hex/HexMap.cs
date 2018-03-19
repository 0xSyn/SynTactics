using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

    public GameObject hexPrefab;

    int width=20;
    int height=20;

    public Sprite[] hexState;

    //float xOffset=1.0f;
    //float zOffset=.764f;

	// Use this for initialization
	void Start () {
        CreateMap();
	}
	
	
    void CreateMap() {
        for (int col = 0; col < width; col++){
            for (int row = 0; row < height; row++){

                HexLoad h=new HexLoad(col,row);
                GameObject hex_go=(GameObject)Instantiate(hexPrefab,h.Position(),Quaternion.identity,this.transform);
                hex_go.name="hex"+col+"_"+row;
                hex_go.transform.SetParent(this.transform);
                hex_go.isStatic=true;
                
                hex_go.GetComponentInChildren<SpriteRenderer>().sprite=hexState[1];
               
                
            }
        }
        StaticBatchingUtility.Combine(this.gameObject);

    }
    // Update is called once per frame
	void Update () {
		
	}
}
