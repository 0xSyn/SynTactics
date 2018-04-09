using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts {
    public class UnitData : MonoBehaviour {
        private int HP;
        private int MP;
        private int lev;
        string job;
        private int MOVERANGE = 5;
        private int moveRange = 5;
        private int team = 1;

        void Start() {
            TurnReset();
        }

        public bool IsFriendlyUnit(int teamNum) {
            return team == teamNum;
        }

        public int GetTeam() {
            return team;

        }

        public void TurnReset() {
            Debug.Log("UNIT TURN RESET");
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
            Debug.Log("Moves Available: " + moveRange);
        }

        public void MoveRangeDec(int dist) {
            moveRange = moveRange - dist;
            Debug.Log("Moves Available: " + moveRange);
        }

        public void MoveRangeInc(int dist) {
            moveRange = moveRange + dist;
            Debug.Log("Moves Available: " + moveRange);
        }









            
        


        



    }
}