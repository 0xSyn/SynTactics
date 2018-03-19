using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexLoad {
    //C+R+S==0
    public readonly int C;
    public readonly int R;
    public readonly int S;
    static readonly float WIDTH_MULTIPLIER = Mathf.Sqrt(3)/2;

    public HexLoad(int c, int r) {
        this.C=c;
        this.R=r;
        this.S=-(c+r);

    }
    public Vector3 Position() {
        float radius=.65f;
        float height=radius*1.75f;
        float width=WIDTH_MULTIPLIER*height;
        float horiz=width;
        float vert=height*(.65f);
        return new Vector3(horiz*(this.C+this.R/2f),0,vert*R);
    }
}
