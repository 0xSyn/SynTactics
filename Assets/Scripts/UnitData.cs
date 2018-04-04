using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitData : MonoBehaviour {
    private int HP;
    private int MP;
    private int lev;
    string job;
    private int MOVERANGE = 5;
    public int moveRange = 5;

    public void TurnReset() {
        moveRange = MOVERANGE;
    }

    public void MoveRangeDec(int dist) {
        moveRange = moveRange - dist;
    }
    public void MoveRangeInc(int dist) {
        moveRange = moveRange + dist;
    }
}
