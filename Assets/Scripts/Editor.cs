using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using System.IO;

public class Editor : MonoBehaviour {
    public GameObject Block;
    private GameObject[,] _board;
    private GameObject _selectedNode;
    private Editor_UI _UI;

    void Start() {
        _UI = GetComponent<Editor_UI>();
    }

    public void LoadBlocks(int x,int y) {
        _board=new GameObject[x,y];
        for (int i = 0; i < x; i++) {
            for (int j = 0; j < y; j++) {
                _board[i,j] = Instantiate(Block, new Vector3(i, 0, j), Quaternion.identity, transform);
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
        Vector3 nodePos=_selectedNode.transform.position;
        _selectedNode.transform.position = new Vector3(nodePos.x,nodePos.y+heightAdjust,nodePos.z);//_selectedNode.transform.position.y;
    }




    public void ExportXML(string title) {




        string output = "<Map0> \n   <col>"+ _board.Length+"</col> \n   <row>"+_board.Length+"</row>\n";




        for (int x = 0; x < _board.GetLength(0)-1;x++){
            for (int y = 0; y < _board.GetLength(1)-1; y++) {
                output += "   <Block name = \"" + x + "_" + y+"\"" + ">\n";
                output += "      <GeoType>"+"grass"+"</GeoType>\n";
                output += "      <Height>"+ _board[x, y].transform.position.y + "</Height>\n";//
                output += "      <Desc>"+"desc"+"</Desc>\n";
                output += "   </Block>\n";
            }
        }

        output += "</Map>";
        File.WriteAllText(title+".xml", output);
        Debug.Log("FILE SAVED");
        //File.WriteAllText("hi.txt", "ds");


    }



}
