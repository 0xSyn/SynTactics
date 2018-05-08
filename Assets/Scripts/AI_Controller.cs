using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts {
    public class AI_Controller : MonoBehaviour {
        private BoardManager _boardManager;


        void Start() {
            _boardManager = GameObject.Find("Map").GetComponent<BoardManager>();
        }

        // Update is called once per frame
        void Update() {
            if (Input.GetKeyDown(KeyCode.C)) {
                AI_turn();
                Debug.Log("Gotta find units");
            }
        }

        public void AI_turn() {
            AI_findUnits();
        }

        void AI_findUnits() {
            GameObject[] alliedUnits = _boardManager.GetComponent<BoardManager>().GetUnitsByTeam(0);
            GameObject[] enemyUnits = _boardManager.GetComponent<BoardManager>().GetUnitsByTeam(1);
            Debug.Log("N");
            //foreach (var unit in alliedUnits) {
                //if (unit.GetComponent<NodeData>().thisUnit != null) {
                    //Debug.Log("NO");
                    _boardManager.GetComponent<BoardPathing>().AI_path_simple(enemyUnits[0],alliedUnits[0]);
            AI_attack(alliedUnits[0]);
                    //AI_attack(unit);
                //}

            //}

        }

        void AI_move() {

        }


        void AI_attack(GameObject unit) {
            unit.GetComponentInChildren<UnitData>().TakeDamage(3);
        }



    }
}