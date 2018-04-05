using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class UnitPath : MonoBehaviour {

    List<GameObject> path = new List<GameObject>();
    private BoardManager _boardManager;
    private GameObject[] _nodeSubset = new GameObject[100];
    private int blockSubset_pointer;
    public Color[] colorz = { Color.black, Color.blue, Color.cyan, Color.gray, Color.green };

    private void Start() {
        _boardManager = GetComponent<BoardManager>();
    }

    public GameObject scanSubset(int id) {
        for (int i = 0; i < blockSubset_pointer; i++) {
            if (_nodeSubset[i].GetComponent<BlockData>().blockID == id) {
                return _nodeSubset[i];
                //_blockSelected2 = blockSubset[i];            
            }
        }
        return null;
    }



    public void AddNodes(GameObject nodeA, GameObject nodeB) {
        int dist=1;
        if (nodeA.GetComponent<BlockData>().col > nodeB.GetComponent<BlockData>().col) {//move left
            dist += Mathf.Abs(nodeA.GetComponent<BlockData>().col - nodeB.GetComponent<BlockData>().col);
            for (int i = 1; i != dist; i++) {
                path.Add(_boardManager._map[nodeA.GetComponent<BlockData>().col - i, nodeA.GetComponent<BlockData>().row]);
            }
        }
        else if (nodeA.GetComponent<BlockData>().col < nodeB.GetComponent<BlockData>().col) {//move right
            dist += Mathf.Abs(nodeB.GetComponent<BlockData>().col - nodeA.GetComponent<BlockData>().col);
            for (int i = 1; i != dist; i++) {
                path.Add(_boardManager._map[nodeA.GetComponent<BlockData>().col + i, nodeA.GetComponent<BlockData>().row]);
            }
        }
        else if (nodeA.GetComponent<BlockData>().row > nodeB.GetComponent<BlockData>().row) {//move down
            dist += Mathf.Abs(nodeA.GetComponent<BlockData>().row - nodeB.GetComponent<BlockData>().row);
            for (int i = 1; i != dist; i++) {
                path.Add(_boardManager._map[nodeA.GetComponent<BlockData>().col, nodeA.GetComponent<BlockData>().row - i]);
            }
        }
        else {//move up
            dist += Mathf.Abs(nodeB.GetComponent<BlockData>().row - nodeA.GetComponent<BlockData>().row);
            for (int i = 1; i != dist; i++) {
                path.Add(_boardManager._map[nodeA.GetComponent<BlockData>().col, nodeA.GetComponent<BlockData>().row + i]);
            }
        }
    }

    void printnodes() {
        for (int i = 0; i < path.Count - 1; i++) {
            Debug.Log("path[0,n-1] "+path[i].GetComponent<BlockData>().col);
        }
    }

    public void BuildPath(GameObject node) {
        Debug.Log(node.GetComponent<BlockData>().Unit.GetComponent<UnitData>().GetMoveRange());

        if (path.Count < 1) {
            path.Add(node);
        }
        else {
            AddNodes(path[path.Count-1], node);
            path[0].GetComponent<BlockData>().thisUnit.GetComponent<UnitData>().SetMoveRange(path[0].GetComponent<BlockData>().thisUnit.GetComponent<UnitData>().GetMoveRangeTotal() - (path.Count-1));
            DrawPath();
        }
        findCircRange(path[path.Count - 1]);
    }

    public void DrawPath() {
        for (int i = 0; i < path.Count-1; i++) {
            
            DrawLine(
                new Vector3(
                    path[i].transform.position.x,
                    path[i].transform.position.y + 3,
                    path[i].transform.position.z),
                new Vector3(
                    path[i + 1].transform.position.x,
                    path[i + 1].transform.position.y + 3,
                    path[i + 1].transform.position.z),
                colorz[i]
            );
        }
        
    }
    public void findCircRange(GameObject node) {
        
        ClearBlockSubset();
        blockSubset_pointer = 0;
        int col = node.GetComponent<BlockData>().col;
        int row = node.GetComponent<BlockData>().row;
        int range = path[0].GetComponent<BlockData>().thisUnit.GetComponent<UnitData>().GetMoveRange();
        int rangeDec = range + 1;
        //Debug.Log(path[0].GetComponent<BlockData>().thisUnit.GetComponent<UnitData>().GetMoveRange());
        //Debug.Log("RANGE_" + range + " col_" + path[0].GetComponent<BlockData>().col + " row_" + path[0].GetComponent<BlockData>().row);
        for (int x = 1; x < range + 1; x++) {
            rangeDec--;
            if (col - x > -1) {//left
                _nodeSubset[blockSubset_pointer++] = _boardManager._map[col - x, row];
                _boardManager._map[col - x, row].GetComponent<BlockData>().setMovable(true);
            }

            if (col + x < _boardManager._mapWidth) {//right
                _nodeSubset[blockSubset_pointer++] = _boardManager._map[col + x, row];
                _boardManager._map[col + x, row].GetComponent<BlockData>().setMovable(true);
            }
            if (row - x > -1) {//down
                _nodeSubset[blockSubset_pointer++] = _boardManager._map[col, row - x];
                _boardManager._map[col, row - x].GetComponent<BlockData>().setMovable(true);
            }
            if (row + x < _boardManager._mapWidth) {//up
                _nodeSubset[blockSubset_pointer++] = _boardManager._map[col, row + x];
                _boardManager._map[col, row + x].GetComponent<BlockData>().setMovable(true);
            }

            for (int y = 1; y < rangeDec; y++) {
                if (col + x < _boardManager._mapWidth && row + y < _boardManager._mapWidth) { //up right
                    _nodeSubset[blockSubset_pointer++] = _boardManager._map[col + x, row + y];
                    _boardManager._map[col + x, row + y].GetComponent<BlockData>().setMovable(true);
                }

                if (col + x < _boardManager._mapWidth && row - y > -1) {//down right
                    _nodeSubset[blockSubset_pointer++] = _boardManager._map[col + x, row - y];
                    _boardManager._map[col + x, row - y].GetComponent<BlockData>().setMovable(true);
                }
                if (col - x > -1 && row + y < _boardManager._mapWidth) { //up left
                    _nodeSubset[blockSubset_pointer++] = _boardManager._map[col - x, row + y];
                    _boardManager._map[col - x, row + y].GetComponent<BlockData>().setMovable(true);
                }

                if (col - x > -1 && row - y > -1) {//down left
                    _nodeSubset[blockSubset_pointer++] = _boardManager._map[col - x, row - y];
                    _boardManager._map[col - x, row - y].GetComponent<BlockData>().setMovable(true);
                }





            }
        }
        //Debug.Log(blockSubset_pointer);
    }


    private void ClearBlockSubset() {
        for (int i = 0; i < blockSubset_pointer; i++) {
            _nodeSubset[i].GetComponent<BlockData>().setMovable(false);
            _nodeSubset[i] = null;
            //_blockSelected = null;
            //_blockSelected2 = null;
            //UI_ally.SetActive(false);
        }
        blockSubset_pointer = 0;
    }

    void DrawLine(Vector3 start, Vector3 end, Color color, float duration = 1.2f) {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
        lr.SetColors(color, color);
        lr.SetWidth(0.1f, 0.1f);
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        GameObject.Destroy(myLine, duration);
    }
}
