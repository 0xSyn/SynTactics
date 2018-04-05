using System.Xml.Linq;
using UnityEngine;

//Needed for XDocument

namespace Assets.Scripts {
    public class BoardManager : MonoBehaviour {

        public GameObject[,] _map = new GameObject[20, 20];
        public int _mapWidth;
        public int _mapHeight;

        private UnitPath _path;
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
            _path = GetComponent<UnitPath>();
        }


        public int SetMapHeight(int height) {return _mapHeight = height;}
        public int SetMapWidth(int width) {return _mapWidth = width;}
        public int GetCol(int BlockID) { return BlockID /_mapWidth; }
        public int GetRow(int BlockID) { return BlockID % _mapWidth; }







        

        public string HasUnit(int col,int row) {
            return _map[col,row].GetComponent<BlockData>().unitz;
        }

        public void MoveUnit(int id) {
            if (_movementMode) {
                if (_blockSelected2=_path.scanSubset(id)) {
                    Debug.Log("ID: " + _blockSelected2.GetComponent<BlockData>().blockID);
                    _path.BuildPath(_blockSelected2);
                }
                else {
                    Debug.Log("Out Of Movement Range");
                }
            }
        }

        public void SetHovered(int col, int row) {
            _blockHovered = _map[col, row];
        }




        public void Selected(int x, int y) {
            _blockSelected=_map[x, y];
            UI_ally.SetActive(true);
            
        }

        public bool isMovementModeActive() {
            return _movementMode;
        }

        public void Mode_Movement() {
            if (_blockSelected != null) {
                
                _movementMode = true;
                _path.BuildPath(_blockSelected);

            }
        }
    }
}
