using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts {
    public class UI_manager : MonoBehaviour {
        BoardManager _boardManager;
        public GameObject UI_ActionMenu;
        public GameObject UI_UnitData;
        private AI_Controller ai;
        public Text HPcurrent_allied;
        public Text HPtotal_allied;
        public Text APcurrent_allied;
        public Text APtotal_allied;
        public Text name_allied;
        public Slider HealthBar_allied;
        // Use this for initialization
        void Start() {
            _boardManager = GetComponent<BoardManager>();
            UI_ActionMenu.SetActive(false);
            UI_UnitData.SetActive(false);
            ai= GameObject.Find("Game").GetComponent<AI_Controller>();
        }

        public void SetActionMenuVisible(bool visible) {
            UI_ActionMenu.SetActive(visible);
        }
        public void SetUnitDataVisible(bool visible) {
            UI_UnitData.SetActive(visible);
        }
        public void SetUnitDataVisible(UnitData unit) {
            UI_UnitData.SetActive(true);
            HPcurrent_allied.text = ""+unit.GetHP_current();
            HPtotal_allied.text = "/" + unit.GetHP_total();
            APcurrent_allied.text = "" + unit.GetMoveRange();
            APtotal_allied.text = "/" + unit.GetMoveRangeTotal();
            HealthBar_allied.value = unit.GetHP_current();
            HealthBar_allied.maxValue= unit.GetHP_total();
            name_allied.text = "Dark Mage";
            Debug.Log(name_allied.text);
        }


        public void BTN_Move() {
            _boardManager.SetMovementMode(true);
        }

        public void BTN_attack() {
            _boardManager.SetAttackMode(true);
        }

        public void BTN_endTurn() {
            Debug.Log("END TURN");
            ai.AI_turn();
            GameObject[] nodes=_boardManager.GetUnitsByTeam(0);
            nodes[0].GetComponent<NodeData>().thisUnit.GetComponent<UnitData>().TurnReset();
        }
    }
}
