using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Scripts {
    public class UI_manager : MonoBehaviour {
        BoardManager _boardManager;
        public GameObject UI_ActionMenu;
        public GameObject UI_UnitData;
        // Use this for initialization
        void Start() {
            _boardManager = GetComponent<BoardManager>();
            UI_ActionMenu.SetActive(false);
            UI_UnitData.SetActive(false);
        }

        public void SetActionMenuVisible(bool visible) {
            UI_ActionMenu.SetActive(visible);
        }
        public void SetUnitDataVisible(bool visible) {
            UI_UnitData.SetActive(visible);
        }


        public void BTN_Move() {
            _boardManager.SetMovementMode(true);
        }

        public void BTN_attack() {

        }
    }
}
