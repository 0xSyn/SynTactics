using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditor : MonoBehaviour {
    public GameObject Block;
    private GameObject[,] _board;
    public void LoadBlocks(int x,int y) {
        _board=new GameObject[x,y];
        for (int i = 0; i < x; i++) {
            for (int j = 0; j < y; j++) {
                _board[i,j] = Instantiate(Block, new Vector3(i, 0, j), Quaternion.identity, transform);
            }
        }
    }

}
