using System.Xml.Linq;
using UnityEngine;

//Needed for XDocument

namespace Assets.Scripts {
    public class BoardManager : MonoBehaviour {






        public string HasUnit(int blockID) {
            return _map[blockID].GetComponent<BlockData>().unitz;
        }

        public void MoveUnit(int fromBlockID, int toBlockID) {
            _map[fromBlockID].GetComponent<BlockData>().unitz = "none";
            _map[toBlockID].GetComponent<BlockData>().unitz = "ally";
        }

        public void isSelected(int BlockID) {
            Debug.Log(_map[BlockID].GetComponent<BlockData>().isSelected);
        }



















        /// <summary>
        /// //////////////////////////////////////////////////////////////////////
        /// 
        ///  MAP CREATION
        /// 
        /// //////////////////////////////////////////////////////////////////////
        /// </summary>



        public GameObject Block;
        private readonly GameObject[] _map = new GameObject[400];
        private int _mapWidth;
        private int _mapHeight;
        private XDocument _xmlDoc; //create Xdocument. Will be used later to read XML file IEnumerable<XElement> items; //Create an Ienumerable list. Will be used to store XML Items. 
        private readonly string[,] _xmlMapData = new string[500, 50];




        void Start() {
            //
            LoadXML();
            CreateMap();
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
                    _map[blockID] = Instantiate(Block, new Vector3(col, float.Parse(_xmlMapData[blockID, 2]), row), Quaternion.identity, transform);

                    _map[blockID].GetComponent<BlockData>().Initialize(
                        int.Parse(_xmlMapData[blockID, 0]),//ID
                        _xmlMapData[blockID, 1],//Geography
                        float.Parse(_xmlMapData[blockID, 2]),//Height
                        _xmlMapData[blockID, 3],//Desc
                        _xmlMapData[blockID, 25]//unit
                    );

                    _map[blockID].name = "blk_" + col + "_" + row;
                    _map[blockID].transform.SetParent(transform);
                    _map[blockID].isStatic = true;
      

                }
            }
        
            StaticBatchingUtility.Combine(this.gameObject);
            Debug.Log("Create _map End");
        }
    }
}
