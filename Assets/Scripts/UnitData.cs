using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts {
    public class UnitData : MonoBehaviour {
        private int HP_total=20;
        private int HP_current = 20;
        private int MP;
        private int lev;
        string job;
        private int MOVERANGE = 5;
        private int moveRange = 5;
        private int attackRange = 1;
        private int team;
        private ParticleSystem blood;
        void Start() {
            TurnReset();
        }

        public bool IsFriendlyUnit(int teamNum) {
            return team == teamNum;
        }

        public int GetTeam() {
            return team;

        }
        public void SetTeam(int teamID) {
            team=teamID;

        }

        public void TurnReset() {
            Debug.Log("UNIT TURN RESET");
            moveRange = MOVERANGE;
        }

        public int GetHP_current() {
            return HP_current;

        }
        public int GetHP_total() {
            return HP_total;

        }

        public int GetMoveRange() {
            return moveRange;
        }
        public int GetAttackRange() {
            return attackRange;
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


        public void TakeDamage(int damage) {
            Debug.Log("that hurts");
            //blood = Resources.Load("ParticleSystems/ps_blood0") as ParticleSystem;
            blood=Instantiate(Resources.Load("ParticleSystems/ps_blood0", typeof (ParticleSystem))) as ParticleSystem;//, new Vector3(transform.position.x, transform.position.y + 2.5f, transform.position.z), Quaternion.identity, transform);
            blood.transform.position = transform.position;// new Vector3(transform.position.x, transform.position.y + 2.5f, transform.position.z);
            blood.Play();
            //ParticleSystem blood=Instantiate()
            if ((HP_current -= damage) <= 0) {
                //Destroy(gameObject);
                GetComponentInChildren<UnitMovement>().AnimDeath();

            }
        }








            
        


        



    }
}