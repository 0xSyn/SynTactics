using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class UnitPath : MonoBehaviour {

    List<GameObject> path = new List<GameObject>();
    private BoardManager _boardManager;
    private GameObject[] blockSubset = new GameObject[100];
    private int blockSubset_pointer;

    private void Start() {
        _boardManager = GetComponent<BoardManager>();
    }
    //public int GetCol(int BlockID) { return _mapWidth / BlockID; }
    //public int GetRow(int BlockID) { return BlockID - (_mapWidth / BlockID); }

    public GameObject scanSubset(int id) {
        for (int i = 0; i < blockSubset_pointer; i++) {
            if (blockSubset[i].GetComponent<BlockData>().blockID == id) {
                return blockSubset[i];
                //_blockSelected2 = blockSubset[i];

             
            }
        }
        return null;
    }

    public void BuildPath(GameObject node) {
        path.Add(node);
        if (path.Count > 1) { DrawPath(); }
    }

    public void DrawPath() {
        for (int i = 1; i < path.Count; i++) {
            DrawLine(
                new Vector3(
                    path[i].transform.position.x,
                    path[i].transform.position.y + 3,
                    path[i].transform.position.z),
                new Vector3(
                    path[i - 1].transform.position.x,
                    path[i - 1].transform.position.y + 3,
                    path[i - 1].transform.position.z),
                Color.blue
            );
        }
        findCircRange(path[path.Count-1]);
    }
    public void findCircRange(GameObject node) {
        ClearBlockSubset();
        blockSubset_pointer = 0;
        int col = node.GetComponent<BlockData>().col;
        int row = node.GetComponent<BlockData>().row;
        int range = path[0].GetComponent<BlockData>().Unit.GetComponent<UnitData>().moveRange;
        int rangeDec = range + 1;

        for (int x = 1; x < range + 1; x++) {
            rangeDec--;
            if (col - x > -1) {//left
                blockSubset[blockSubset_pointer++] = _boardManager._map[col - x, row];
                _boardManager._map[col - x, row].GetComponent<BlockData>().setMovable(true);
            }

            if (col + x < _boardManager._mapWidth) {//right
                blockSubset[blockSubset_pointer++] = _boardManager._map[col + x, row];
                _boardManager._map[col + x, row].GetComponent<BlockData>().setMovable(true);
            }
            if (row - x > -1) {//down
                blockSubset[blockSubset_pointer++] = _boardManager._map[col, row - x];
                _boardManager._map[col, row - x].GetComponent<BlockData>().setMovable(true);
            }
            if (row + x < _boardManager._mapWidth) {//up
                blockSubset[blockSubset_pointer++] = _boardManager._map[col, row + x];
                _boardManager._map[col, row + x].GetComponent<BlockData>().setMovable(true);
            }

            for (int y = 1; y < rangeDec; y++) {
                if (col + x < _boardManager._mapWidth && row + y < _boardManager._mapWidth) { //up right
                    blockSubset[blockSubset_pointer++] = _boardManager._map[col + x, row + y];
                    _boardManager._map[col + x, row + y].GetComponent<BlockData>().setMovable(true);
                }

                if (col + x < _boardManager._mapWidth && row - y > -1) {//down right
                    blockSubset[blockSubset_pointer++] = _boardManager._map[col + x, row - y];
                    _boardManager._map[col + x, row - y].GetComponent<BlockData>().setMovable(true);
                }
                if (col - x > -1 && row + y < _boardManager._mapWidth) { //up left
                    blockSubset[blockSubset_pointer++] = _boardManager._map[col - x, row + y];
                    _boardManager._map[col - x, row + y].GetComponent<BlockData>().setMovable(true);
                }

                if (col - x > -1 && row - y > -1) {//down left
                    blockSubset[blockSubset_pointer++] = _boardManager._map[col - x, row - y];
                    _boardManager._map[col - x, row - y].GetComponent<BlockData>().setMovable(true);
                }





            }
        }
        Debug.Log(blockSubset_pointer);
    }


    private void ClearBlockSubset() {
        for (int i = 0; i < blockSubset_pointer; i++) {
            blockSubset[i].GetComponent<BlockData>().setMovable(false);
            blockSubset[i] = null;
            //_blockSelected = null;
            //_blockSelected2 = null;
            //UI_ally.SetActive(false);
        }
        blockSubset_pointer = 0;
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
}
