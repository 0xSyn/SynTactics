using System.Xml.Linq;
using UnityEngine;

//Needed for XDocument

namespace Assets.Scripts {
    public class BoardManager : MonoBehaviour {
        private GameObject _blockSelected;
        private GameObject _blockSelected2;
        private GameObject _blockHovered;
        private GameObject[] blockSubset=new GameObject[100];
        private int blockSubset_pointer;
        public Canvas UI;
        public GameObject UI_ally;
        public GameObject UI_selected;
        private bool _movementMode = false;



        void Update() {
            if (_blockHovered != null && _blockSelected!=null) {
                Debug.Log("DRAWING");
                Debug.DrawLine(
                    new Vector3(
                        _blockSelected.transform.position.x,
                        _blockSelected.transform.position.y + 3,
                        _blockSelected.transform.position.z), 
                    new Vector3(
                        _blockHovered.transform.position.x,
                        _blockHovered.transform.position.y + 3,
                        _blockHovered.transform.position.z),
                    Color.blue
                );


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







        public void GetColRow(int BlockID) {
            int col = BlockID / _mapWidth;
            //int
        }

        public string HasUnit(int col,int row) {
            return _map[col,row].GetComponent<BlockData>().unitz;
        }

        public void MoveUnit(int id) {
            bool blockFound = false;
            if (_movementMode) {
                for(int i=0;i< blockSubset_pointer; i++){
                    if (blockSubset[i].GetComponent<BlockData>().blockID == id) {
                        blockFound = true;
                        _blockSelected2 = blockSubset[i];
                        break;
                    }
                }

                if (blockFound) {
                    Debug.Log("MOVE MOTHER FUCKER");
                    _blockSelected2.GetComponent<BlockData>().thisUnit = _blockSelected.GetComponent<BlockData>().thisUnit;
                    _blockSelected.GetComponent<BlockData>().unitz = "none";
                    _blockSelected2.GetComponent<BlockData>().unitz = "ally";
                    _blockSelected2.GetComponent<BlockData>().thisUnit.transform.position = new Vector3(
                        _blockSelected2.transform.position.x,
                        _blockSelected2.transform.position.y + 2.5f,
                        _blockSelected2.transform.position.z
                    );
                    ClearBlockSubset();
                }

                else {
                    Debug.Log("Out Of Movement Range");
                }
            }
        }

        public void SetHovered(int col, int row) {
            _blockHovered = _map[col, row];
        }

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
                findCircRange();
            }
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
















        /// <summary>
        /// //////////////////////////////////////////////////////////////////////
        /// 
        ///  MAP CREATION
        /// 
        /// //////////////////////////////////////////////////////////////////////
        /// </summary>



        public GameObject Block;
        private readonly GameObject[,] _map = new GameObject[20,20];
        private int _mapWidth;
        private int _mapHeight;
        private XDocument _xmlDoc; //create Xdocument. Will be used later to read XML file IEnumerable<XElement> items; //Create an Ienumerable list. Will be used to store XML Items. 
        private readonly string[,] _xmlMapData = new string[500, 50];




        void Start() {
            SetUI();
            LoadXML();
            CreateMap();
        }

        private void SetUI() {
            UI_ally.SetActive(false);
            UI_selected.SetActive(false);
        }

        public void LoadXML() {

            _xmlDoc = XDocument.Load("Assets/Maps/Map0.xml");
            var items = _xmlDoc.Descendants("Map0").Elements();
            int blockID = -2;
        
            foreach (var item in items) {
                if (blockID < 0) {
                    if (blockID == -2) {
                        _mapWidth = int.Parse(item.Value.Trim());
                    }
                    else if (blockID == -1) {
                        _mapHeight = int.Parse(item.Value.Trim());
                    }
                }
                else {
                
                    _xmlMapData[blockID,0] = blockID.ToString();
                    _xmlMapData[blockID,1] = item.Element("GeoType").Value.Trim();
                    _xmlMapData[blockID,2] = item.Element("Height").Value;
                    _xmlMapData[blockID,3] = item.Element("Desc").Value;
                    if (item.Element("Unit") != null) {
                        Debug.Log(item.Element("Unit").Value);
                        _xmlMapData[blockID, 25] = item.Element("Unit").Value;
                    }
                    else { _xmlMapData[blockID, 25] = "none"; }
                }
                blockID++;
            }


        }

        private void CreateMap() {
            Debug.Log("Create _map Start");
            for (int col = 0; col < _mapWidth; col++) {
                for (int row = 0; row < _mapHeight; row++) {

                    int blockID = (col * _mapHeight) + row;
                    _map[col,row] = Instantiate(Block, new Vector3(col, float.Parse(_xmlMapData[blockID, 2]), row), Quaternion.identity, transform);

                    _map[col,row].GetComponent<BlockData>().Initialize(
                        int.Parse(_xmlMapData[blockID, 0]),//ID
                        _xmlMapData[blockID, 1],//Geography
                        float.Parse(_xmlMapData[blockID, 2]),//Height
                        _xmlMapData[blockID, 3],//Desc
                        _xmlMapData[blockID, 25]//unit
                    );

                    _map[col, row].name = "blk_" + col + "_" + row;
                    _map[col, row].transform.SetParent(transform);
                    _map[col, row].isStatic = true;
                    _map[col, row].GetComponent<BlockData>().col = col;
                    _map[col, row].GetComponent<BlockData>().row = row;


                }
            }
        
            StaticBatchingUtility.Combine(this.gameObject);
            Debug.Log("Create _map End");
        }
    }
}
