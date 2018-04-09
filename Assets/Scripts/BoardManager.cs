using System.Xml.Linq;
using UnityEngine;

namespace Assets.Scripts {
    public class BoardManager : MonoBehaviour {

        public GameObject[,] _map = new GameObject[20, 20];
        public int _mapWidth;
        public int _mapHeight;

        private BoardPathing _path;
        private GameObject _blockSelected;
        private GameObject _blockSelected2;
        private GameObject _blockHovered;
        private GameObject[] blockSubset=new GameObject[100];
        private int blockSubset_pointer;
        public Canvas UI;
        public GameObject UI_ally;
        public GameObject UI_selected;
        private bool _movementMode = false;

        void Start() {
            _path = GetComponent<BoardPathing>();
            UI_ally.SetActive(false);
            UI_selected.SetActive(false);

        }

        void Update() {
            if(Input.GetMouseButtonDown(1)){
                //Debug.Log("RMB");
                if (_path.PathSize() > 1) {
                    _path.RemovePartialPath(1);
                }
                else {
                    _movementMode = false;
                    _blockSelected = null;
                    UI_ally.SetActive(false);
                    _path.ClearBlockSubset();
                }
            }
            if (Input.GetMouseButtonDown(2)) {
                //Debug.Log("MMB");
            }
            if (Input.GetKey(KeyCode.U)) {
                _path.Move();
            }

        }


        public int SetMapHeight(int height) {return _mapHeight = height;}
        public int SetMapWidth(int width) {return _mapWidth = width;}
        public int GetCol(int BlockID) { return BlockID /_mapWidth; }
        public int GetRow(int BlockID) { return BlockID % _mapWidth; }



        public void MoveUnit() {
            _blockHovered = null;
            _blockSelected = null;
            _blockSelected2 = null;
            _movementMode = false;
            _path.Move();
            UI_ally.SetActive(false);
        }



        public void AddToPath(int id) {
            if (_movementMode) {
                if (_blockSelected2=_path.IsNodeInSubset(id)) {
                    //Debug.Log("ID: " + _blockSelected2.GetComponent<NodeData>().blockID);
                    _path.BuildPath(_blockSelected2);
                }
                else {
                    //Debug.Log("Out Of Movement Range");
                }
            }
        }

        //public void SetHovered(int col, int row) {
        //    _blockHovered = _map[col, row];
        //}




        public void Selected(int x, int y) {
            _blockSelected=_map[x, y];
            UI_ally.SetActive(true);
            
        }

        public bool isMovementModeActive() {
            return _movementMode;
        }

        public void BTN_Move() {
            if (_blockSelected != null) {   
                _movementMode = true;
                _path.BuildPath(_blockSelected);
            }
        }
//_________________________________________________________________________________
    }
}
