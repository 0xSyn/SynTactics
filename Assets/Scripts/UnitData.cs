using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitData : MonoBehaviour {
    private int HP;
    private int MP;
    private int lev;
    string job;
    private int MOVERANGE = 5;
    private int moveRange = 5;

    void Start (){
        TurnReset();
    }


    public void TurnReset() {
        Debug.Log("FUK "+moveRange);
        moveRange = MOVERANGE;
    }
    public int GetMoveRange() {
        return moveRange;
    }
    public int GetMoveRangeTotal() {
        return MOVERANGE;
    }

    public void SetMoveRange(int newRange) {
        moveRange = newRange;
    }

    public void MoveRangeDec(int dist) {
        moveRange = moveRange - dist;
    }
    public void MoveRangeInc(int dist) {
        moveRange = moveRange + dist;
    }
}
