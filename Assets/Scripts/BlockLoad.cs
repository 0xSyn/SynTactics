using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockLoad {
    public readonly int COL;
    public readonly int ROW;


    public BlockLoad(int c, int r) {
        this.COL = c;
        this.ROW = r;
        

    }

    public Vector3 Position(){
        return new Vector3(COL, 0, ROW);
    }
    
}
