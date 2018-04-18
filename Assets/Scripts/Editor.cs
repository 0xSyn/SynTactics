using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using System.IO;
using System.Xml.Linq;

public class Editor : MonoBehaviour {
    public GameObject Block;
    private GameObject[,] _board;
    private GameObject _selectedNode;
    private Editor_UI _UI;

    void Start() {
        _UI = GetComponent<Editor_UI>();
    }

    public void LoadBlocks(int x, int y) {
        _board = new GameObject[x, y];
        for (int i = 0; i < x; i++) {
            for (int j = 0; j < y; j++) {
                _board[i, j] = Instantiate(Block, new Vector3(i, 0, j), Quaternion.identity, transform);
                _board[i, j].GetComponent<Editor_Node>().x = i;
                _board[i, j].GetComponent<Editor_Node>().y = j;
            }
        }
    }

    public void SelectNode(int x, int y) {
        _selectedNode = _board[x, y];
        _UI.SetPanel_Node();
        _UI.SetNodeID(x, y);
    }


    public void AdjustNodeHeight(float heightAdjust) {
        Vector3 nodePos = _selectedNode.transform.position;
        _selectedNode.transform.position = new Vector3(nodePos.x, nodePos.y + heightAdjust, nodePos.z);//_selectedNode.transform.position.y;
    }



    //public GameObject Block;
    //private BoardManager _boardManager;
    private int _mapWidth;
    private int _mapHeight;
    private XDocument _xmlDoc; //create Xdocument. Will be used later to read XML file IEnumerable<XElement> items; //Create an Ienumerable list. Will be used to store XML Items. 
    private readonly string[,] _xmlMapData = new string[500, 50];


    public void ImportXML(string title) {

        _xmlDoc = XDocument.Load("Assets/Maps/"+title+".xml");
        var items = _xmlDoc.Descendants("Map").Elements();
        int blockID = -2;

        foreach (var item in items) {
            if (blockID < 0) {
                if (blockID == -2) {

                    _mapWidth =(int.Parse(item.Value.Trim()));
                } else if (blockID == -1) {
                    _mapHeight = (int.Parse(item.Value.Trim()));
                }
            } else {

                _xmlMapData[blockID, 0] = blockID.ToString();
                _xmlMapData[blockID, 1] = item.Element("GeoType").Value.Trim();
                _xmlMapData[blockID, 2] = item.Element("Height").Value;
                _xmlMapData[blockID, 3] = item.Element("Desc").Value;
                if (item.Element("Unit") != null) {
                    //Debug.Log(item.Element("Unit").Value);
                    _xmlMapData[blockID, 25] = item.Element("Unit").Value;
                } else { _xmlMapData[blockID, 25] = "none"; }
            }
            blockID++;
        }
        CreateMap();

    }



    private void DestroyBoard() {
        if (_board != null) {
            for (int col = 0; col < _mapWidth; col++) {
                for (int row = 0; row < _mapHeight; row++) {
                    Destroy(_board[col, row]);
                }
            }
        }
    }



    private void CreateMap() {
        DestroyBoard();
        _board=new GameObject[_mapWidth, _mapHeight];
        Debug.Log("Create map Start");
        for (int col = 0; col < _mapWidth; col++) {
            for (int row = 0; row < _mapHeight; row++) {

                int blockId = (col * _mapHeight) + row;
                _board[col, row] = Instantiate(Block, new Vector3(col, float.Parse(_xmlMapData[blockId, 2]), row), Quaternion.identity, transform);
                /*
                _board[col, row].GetComponent<Editor_Node>().Initialize(
                    int.Parse(_xmlMapData[blockID, 0]),//ID
                    _xmlMapData[blockID, 1],//Geography
                    float.Parse(_xmlMapData[blockID, 2]),//Height
                    _xmlMapData[blockID, 3],//Desc
                    _xmlMapData[blockID, 25]//unit
                );
                */
                _board[col, row].name = "blk_" + col + "_" + row;
                _board[col, row].transform.SetParent(transform);
                _board[col, row].isStatic = true;
                _board[col, row].GetComponent<Editor_Node>().x = col;
                _board[col, row].GetComponent<Editor_Node>().y = row;
            }
        }

        StaticBatchingUtility.Combine(this.gameObject);
        Debug.Log("Create _map End");
    }


    public void ExportXML(string title) {
        string output = "<Map> \n   <col>" + _board.GetLength(0) + "</col> \n   <row>" + _board.GetLength(1) + "</row>\n";
        for (int x = 0; x < _board.GetLength(0); x++) {
            for (int y = 0; y < _board.GetLength(1); y++) {
                output += "   <Block name = \"" + x + "_" + y + "\"" + ">\n";
                output += "      <GeoType>" + "grass" + "</GeoType>\n";
                output += "      <Height>" + _board[x, y].transform.position.y + "</Height>\n";//
                output += "      <Desc>" + "desc" + "</Desc>\n";
                output += "   </Block>\n";
            }
        }
        output += "</Map>";
        File.WriteAllText("Assets/Maps/" + title + ".xml", output);
        Debug.Log("FILE SAVED");
    }
}
