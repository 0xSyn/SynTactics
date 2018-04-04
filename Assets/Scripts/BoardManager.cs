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
        private byte[] buildPath = new byte[100];
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

        void Update() {
            if (_blockHovered != null && _blockSelected!=null && false) {

                DrawLine(new Vector3(
                    _blockSelected.transform.position.x,
                    _blockSelected.transform.position.y + 3,
                    _blockSelected.transform.position.z),
                    new Vector3(
                        _blockHovered.transform.position.x,
                        _blockHovered.transform.position.y + 3,
                        _blockHovered.transform.position.z),
                    Color.blue);
            }
        }


        public int GetCol(int BlockID) { return BlockID /_mapWidth; }
        public int GetRow(int BlockID) { return BlockID % _mapWidth; }

        //public Vector3 nodePos(GameObject node) {
            //int col = GetCol(nodeIndex);
            //int row = GetRow(nodeIndex);
            //Debug.Log("COL: "+col+" ROW: "+row);
            //return new Vector3(
                    //_map[node.col col, row].transform.position.x,
                    //_map[col, row].transform.position.y + 3,
                    //_map[col, row].transform.position.z);
        //}


        void DrawLine(Vector3 start, Vector3 end, Color color, float duration = 0.2f) {
            GameObject myLine = new GameObject();
            myLine.transform.position = start;
            myLine.AddComponent<LineRenderer>();
            LineRenderer lr = myLine.GetComponent<LineRenderer>();
            //lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
            lr.SetColors(color, color);
            lr.SetWidth(0.1f, 0.1f);
            lr.SetPosition(0, start);
            lr.SetPosition(1, end);
            GameObject.Destroy(myLine, duration);
        }







        

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
            _path.BuildPath(_blockSelected);
        }

        public bool isMovementModeActive() {
            return _movementMode;
        }

        public void Mode_Movement() {
            if (_blockSelected != null) {
               
                _movementMode = true;
                //findCircRange();
            }
        }


/*
        private void ClearBlockSubset() {
            for (int i = 0; i < blockSubset_pointer; i++) {
                blockSubset[i].GetComponent<BlockData>().setMovable(false);
                blockSubset[i] = null;
                _blockSelected = null;
                _blockSelected2 = null;
                UI_ally.SetActive(false);
            }
            blockSubset_pointer = 0;
        }

        public void FindPath() { }

        public void findCircRange() {
            blockSubset_pointer = 0;
            int col=_blockSelected.GetComponent<BlockData>().col;
            int row = _blockSelected.GetComponent<BlockData>().row;
            int range = 5;
            int rangeDec = range+1;

            for (int x = 1; x < range+1; x++) {
                rangeDec--;
                if (col - x > -1) {//left
                    blockSubset[blockSubset_pointer++] = _map[col - x, row];
                    _map[col - x, row].GetComponent<BlockData>().setMovable(true);
                }

                if (col + x < _mapWidth) {//right
                    blockSubset[blockSubset_pointer++] = _map[col + x, row];
                    _map[col + x, row].GetComponent<BlockData>().setMovable(true);
                }
                if (row - x > -1) {//down
                    blockSubset[blockSubset_pointer++] = _map[col, row - x];
                    _map[col, row - x].GetComponent<BlockData>().setMovable(true);
                }
                if (row + x < _mapWidth) {//up
                    blockSubset[blockSubset_pointer++] = _map[col, row + x];
                    _map[col, row + x].GetComponent<BlockData>().setMovable(true);
                }

                for (int y = 1; y < rangeDec; y++) {
                    if (col + x < _mapWidth && row + y < _mapWidth) { //up right
                        blockSubset[blockSubset_pointer++] = _map[col + x, row + y];
                        _map[col + x, row + y].GetComponent<BlockData>().setMovable(true);
                    }

                    if (col + x < _mapWidth && row - y > -1) {//down right
                        blockSubset[blockSubset_pointer++] = _map[col + x, row - y];
                        _map[col + x, row - y].GetComponent<BlockData>().setMovable(true);
                    }
                    if (col - x > -1 && row + y < _mapWidth) { //up left
                        blockSubset[blockSubset_pointer++] = _map[col - x, row + y];
                        _map[col - x, row + y].GetComponent<BlockData>().setMovable(true);
                    }

                    if (col - x > -1 && row - y > -1) {//down left
                        blockSubset[blockSubset_pointer++] = _map[col - x, row - y];
                        _map[col - x, row - y].GetComponent<BlockData>().setMovable(true);
                    }





                }
            }
            Debug.Log(blockSubset_pointer);
        }


        public void CreateNode() { }



    */




    }
}
