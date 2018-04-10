using System.Xml.Linq;
using UnityEngine;

namespace Assets.Scripts {
    public class BoardManager : MonoBehaviour {

        public GameObject[,] _map = new GameObject[20, 20];
        public int _mapWidth;
        public int _mapHeight;

        private BoardPathing _path;
        private UI_manager _UI;
        private GameObject _blockSelected;
        private GameObject _blockSelected2;
        private GameObject _blockHovered;
        private GameObject[] blockSubset=new GameObject[100];
        private int blockSubset_pointer;
        private bool _movementMode;

        void Start() {
            _path = GetComponent<BoardPathing>();
            _UI = GetComponent<UI_manager>();
        }

        void Update() {
            if(Input.GetMouseButtonDown(1)){///RMB
                if (_path.PathSize() > 1) {
                    _path.RemovePartialPath(1);
                }
                else {
                    _movementMode = false;
                    _blockSelected = null;
                    _UI.SetActionMenuVisible(false);
                    _path.ClearBlockSubset();
                }
            }
            if (Input.GetMouseButtonDown(2)) {//MMB
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

            _UI.SetActionMenuVisible(false);
        }



        public void AddToPath(int id) {
            if (_movementMode) {
                if (_blockSelected2=_path.IsNodeInSubset(id)) {
                    _path.BuildPath(_blockSelected2);
                }
                else {
                    //Debug.Log("Out Of Movement Range");
                }
            }
        }





        public void Selected(int x, int y) {
            _blockSelected=_map[x, y];
            _UI.SetActionMenuVisible(true);
            
        }

        public bool isMovementModeActive() {
            return _movementMode;
        }

        public void SetMovementMode(bool mode_on) {
            if (mode_on) {
                if (_blockSelected != null) {
                    _movementMode = true;
                    _path.BuildPath(_blockSelected);
                }
            }
            else { }
        }
//_________________________________________________________________________________
    }
}
