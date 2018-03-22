using System.Xml.Linq;
using UnityEngine;

//Needed for XDocument

namespace Assets.Scripts {
    public class BoardManager : MonoBehaviour {
        private GameObject _selected;
        public Canvas UI;
        public GameObject UI_ally;
        public GameObject UI_selected;


        public void GetColRow(int BlockID) {
            int col = BlockID / _mapWidth;
            //int
        }

        public string HasUnit(int col,int row) {
            return _map[col,row].GetComponent<BlockData>().unitz;
        }

        public void MoveUnit() {
            _map[0, 0].GetComponent<BlockData>().thisUnit = _selected.GetComponent<BlockData>().thisUnit;
            _selected.GetComponent<BlockData>().unitz = "none";
            _map[0, 0].GetComponent<BlockData>().unitz = "ally";
            _map[0, 0].GetComponent<BlockData>().thisUnit.transform.position = new Vector3(_map[0, 0].transform.position.x, _map[0, 0].transform.position.y + 2.5f, _map[0, 0].transform.position.z);
        }

        public void Selected(int x, int y) {
            _selected=_map[x, y];
            UI_ally.SetActive(true);
            MoveUnit();
            //Debug.Log(_map[BlockID].GetComponent<BlockData>().isSelected);
        }


        public void FindPath() { }

        //public

        public void findCircRange(int column, int row, int range) {
            //UI.GetComponentInChildren<AllyUnitMenu>
            int rangeDec = range+1;

            Selected(column, row);

            for (int x = 1; x < range+1; x++) {
                rangeDec--;
                if (column - x > -1) {//left
                    _map[column - x, row].GetComponent<BlockData>().setMovable();
                }

                if (column + x < _mapWidth) {//right
                    _map[column + x, row].GetComponent<BlockData>().setMovable();
                }
                if (row - x > -1) {//down
                    _map[column, row - x].GetComponent<BlockData>().setMovable();
                }
                if (row + x < _mapWidth) {//up
                    _map[column, row + x].GetComponent<BlockData>().setMovable();
                }

                for (int y = 1; y < rangeDec; y++) {
                    if (column + x < _mapWidth && row + y < _mapWidth) { //up right
                        _map[column + x, row + y].GetComponent<BlockData>().setMovable();
                    }

                    if (column + x < _mapWidth && row - y > -1) {//down right
                        _map[column + x, row - y].GetComponent<BlockData>().setMovable();
                    }
                    if (column - x > -1 && row + y < _mapWidth) { //up left
                        _map[column - x, row + y].GetComponent<BlockData>().setMovable();
                    }

                    if (column - x > -1 && row - y > -1) {//down left
                        _map[column - x, row - y].GetComponent<BlockData>().setMovable();
                    }





                }
            }
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
                    _map[col, row].GetComponent<BlockData>().column = col;
                    _map[col, row].GetComponent<BlockData>().row = row;


                }
            }
        
            StaticBatchingUtility.Combine(this.gameObject);
            Debug.Log("Create _map End");
        }
    }
}
