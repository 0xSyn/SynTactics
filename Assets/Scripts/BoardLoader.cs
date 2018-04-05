using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Linq;
namespace Assets.Scripts {
    public class BoardLoader : MonoBehaviour {
        public GameObject Block;
        private BoardManager _boardManager;
        private int _mapWidth;
        private int _mapHeight;
        private XDocument _xmlDoc; //create Xdocument. Will be used later to read XML file IEnumerable<XElement> items; //Create an Ienumerable list. Will be used to store XML Items. 
        private readonly string[,] _xmlMapData = new string[500, 50];
        public GameObject UI_ally;
        public GameObject UI_selected;

        void Start() {
            _boardManager = GetComponent<BoardManager>();
            //SetUI();
            LoadXML();
            CreateMap();
            
            //_path = GetComponent<UnitPath>();
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

                        _mapWidth=_boardManager.SetMapWidth(int.Parse(item.Value.Trim()));
                    }
                    else if (blockID == -1) {
                        _mapHeight=_boardManager.SetMapHeight(int.Parse(item.Value.Trim()));
                    }
                }
                else {

                    _xmlMapData[blockID, 0] = blockID.ToString();
                    _xmlMapData[blockID, 1] = item.Element("GeoType").Value.Trim();
                    _xmlMapData[blockID, 2] = item.Element("Height").Value;
                    _xmlMapData[blockID, 3] = item.Element("Desc").Value;
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
                    _boardManager._map[col, row] = Instantiate(Block, new Vector3(col, float.Parse(_xmlMapData[blockID, 2]), row), Quaternion.identity, transform);

                    _boardManager._map[col, row].GetComponent<BlockData>().Initialize(
                        int.Parse(_xmlMapData[blockID, 0]),//ID
                        _xmlMapData[blockID, 1],//Geography
                        float.Parse(_xmlMapData[blockID, 2]),//Height
                        _xmlMapData[blockID, 3],//Desc
                        _xmlMapData[blockID, 25]//unit
                    );

                    _boardManager._map[col, row].name = "blk_" + col + "_" + row;
                    _boardManager._map[col, row].transform.SetParent(transform);
                    _boardManager._map[col, row].isStatic = true;
                    _boardManager._map[col, row].GetComponent<BlockData>().col = col;
                    _boardManager._map[col, row].GetComponent<BlockData>().row = row;


                }
            }

            StaticBatchingUtility.Combine(this.gameObject);
            Debug.Log("Create _map End");
        }
    }
}
