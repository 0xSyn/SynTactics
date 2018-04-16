using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

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
    }


    public void AdjustNodeHeight(float heightAdjust) {
        Vector3 nodePos=_selectedNode.transform.position;
        _selectedNode.transform.position = new Vector3(nodePos.x,nodePos.y+heightAdjust,nodePos.z);//_selectedNode.transform.position.y;
    }





}
