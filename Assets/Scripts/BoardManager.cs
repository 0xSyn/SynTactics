using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
namespace Assets.Scripts {
    public class BoardManager : MonoBehaviour {

        public GameObject[,] _map = new GameObject[20, 20];
        public int _mapWidth;
        public int _mapHeight;

        private BoardPathing _path;
        private UI_manager _UI;
        private UnitLoader _unitLoader;
        private GameObject _blockSelected;
        private GameObject _blockSelected2;
        private GameObject _blockHovered;
        private GameObject[] blockSubset=new GameObject[100];
        private int blockSubset_pointer;
        private Dictionary<string, int> _mode = new Dictionary<string, int>();

        public int mode = 2;
        //public GameObject Unit;


        void Start() {
            _path = GetComponent<BoardPathing>();
            _UI = GetComponent<UI_manager>();
            _mode.Add("wait", 0);
            _mode.Add("move", 1);
            _mode.Add("attack", 2);
            _mode.Add("item", 3);

        }
        
        //[Command]
        //void CmdUpdate() {
            void Update() {
                if (Input.GetMouseButtonDown(1)){///RMB
                if (_path.PathSize() > 1) {
                    _path.RemovePartialPath(1);
                }
                else {
                    _blockSelected = null;
                    _UI.SetActionMenuVisible(false);
                    _path.ClearBlockSubset();
                }
            }
            if (Input.GetMouseButtonDown(2)) {//MMB
               SceneManager.LoadScene("Maps/Main");
            }
            if (Input.GetKeyDown(KeyCode.U)) {//MMB
                for (int col = 0; col < _mapWidth; col++) {
                    for (int row = 0; row < _mapHeight; row++) {
                        if (_map[col, row].GetComponent<NodeData>().unitOnNode == "ally") {
                            _unitLoader = GameObject.Find("Manager").GetComponent<UnitLoader>();
                            _unitLoader.CreateUnits(_map[col,row], _mapWidth, _mapHeight);
                        }
                    }
                }
            }
        }

        public GameObject[] GetUnitsByTeam(int team) {
            GameObject[] alliedUnits=new GameObject[10];
            int i=0;
            foreach (var node in _map) {
                if (node.GetComponent<NodeData>().thisUnit != null && node.GetComponent<NodeData>().thisUnit.GetComponent<UnitData>().GetTeam()==team) {
                    alliedUnits[i++] = node; //.GetComponent<NodeData>().thisUnit;
                }

            }
            return alliedUnits;
        }

        public int SetMapHeight(int height) {return _mapHeight = height;}
        public int SetMapWidth(int width) {return _mapWidth = width;}
        public int GetCol(int BlockID) { return BlockID /_mapWidth; }
        public int GetRow(int BlockID) { return BlockID % _mapWidth; }
        public int GetMode() {return mode;}


        public void MoveUnit() {
            _blockHovered = null;
            _blockSelected = null;
            _blockSelected2 = null;
            _path.Move();

            _UI.SetActionMenuVisible(false);
        }



        public void AddToPath(int id) {
            Debug.Log("Mode="+mode);
            if (mode == _mode["move"]) {
                if (_blockSelected2 = _path.IsNodeInSubset(id)) {
                    _path.BuildPath(_blockSelected2);
                }
            }
            else if (mode == _mode["attack"]) {
                if (_blockSelected2 = _path.IsNodeInSubset(id)) {
                    _path.BuildPath(_blockSelected2);
                    //node.GetComponentInChildren<UnitData>().TakeDamage(5);
                }
            }
        }


        public void Attack(GameObject node) {
            
            if (_blockSelected2 = _path.IsNodeInSubset(node.GetComponent<NodeData>().blockID)) {
                node.GetComponentInChildren<UnitData>().TakeDamage(5);
                _blockSelected.GetComponent<NodeData>().thisUnit.GetComponent<UnitMovement>().AnimAttack();
                //_blockSelected.GetComponentInChildren<UnitMovement>().AnimAttack();

            }
        }


        public void Selected(int x, int y) {
            _blockSelected=_map[x, y];
            _UI.SetActionMenuVisible(true);
            
        }

        

        public void SetMovementMode(bool mode_on) {
            if (mode_on) {
                if (_blockSelected != null) {
                    mode = _mode["move"];
                    _path.BuildPath(_blockSelected);
                }
            }
            else { }
        }

        public void SetAttackMode(bool mode_on) {
            if (mode_on) {
                if (_blockSelected != null) {

                    mode = _mode["attack"];
                    _path.BuildPath(_blockSelected);
                }
            } else { }
        }
        //_________________________________________________________________________________
    }
}
