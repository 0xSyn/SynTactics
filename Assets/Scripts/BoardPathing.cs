using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


namespace Assets.Scripts {
    public class BoardPathing : MonoBehaviour {

        private readonly List<GameObject> _path = new List<GameObject>();
        private readonly List<GameObject> _pathLines = new List<GameObject>();
        private readonly GameObject[] _nodeSubset = new GameObject[100];
        private int _subsetPointer;
        private BoardManager _boardManager;

        private void Start() {
            _boardManager = GetComponent<BoardManager>();
        }

        public GameObject IsNodeInSubset(int id) {
            for (int i = 0; i < _subsetPointer; i++) {
                if (_nodeSubset[i].GetComponent<NodeData>().blockID == id) {
                    return _nodeSubset[i];          
                }
            }
            return null;
        }

        //Removes nodes from path from node n-1
        public void RemovePartialPath(int num) {
            for (int i = 0; i < num; i++) {
                if (_path.Count > 0) {
                    _path[_path.Count - 1].GetComponent<NodeData>().SetNode_Last(false);
                    _path.RemoveAt(_path.Count - 1);
                    if (_pathLines.Count > 0) {
                        Destroy(_pathLines[_pathLines.Count - 1]);
                        _pathLines.RemoveAt(_pathLines.Count - 1);
                    }

                    if (_path.Count > 0) {
                        _path[_path.Count - 1].GetComponent<NodeData>().SetNode_Last(true);
                    }

                }
            }

            
            if (_path.Count > 0) {
                _path[0].GetComponent<NodeData>().thisUnit.GetComponent<UnitData>().MoveRangeInc(num);
                DrawPathLines();
                findCircRange(_path[_path.Count - 1]);
            }

        }

        public void RemoveAllPathNodes() {
            while (_path.Count > 0) {
                if (_path.Count > 0) {
                    _path[_path.Count - 1].GetComponent<NodeData>().SetNode_Last(false);
                    _path[_path.Count - 1].GetComponent<NodeData>().HighlightOff();
                    _path.RemoveAt(_path.Count - 1);
                }
            }
        }

        public void RemoveAllPathLines() {
            while (_pathLines.Count > 0) {
                GameObject line = _pathLines[_pathLines.Count - 1];
                _pathLines.RemoveAt(_pathLines.Count - 1);
                Destroy(line);                   
            }
        }

        public int PathSize() {
            return _path.Count;
        }

        //Adds nodes to path in straight line from Node A to node B
        public void AddNodes(GameObject nodeA, GameObject nodeB) {
            _path[_path.Count - 1].GetComponent<NodeData>().SetNode_Last(false);
            int dist = 1;
            if (nodeA.GetComponent<NodeData>().col > nodeB.GetComponent<NodeData>().col) { //move left
                dist += Mathf.Abs(nodeA.GetComponent<NodeData>().col - nodeB.GetComponent<NodeData>().col);
                for (int i = 1; i != dist; i++) {
                    _path.Add(_boardManager._map[nodeA.GetComponent<NodeData>().col - i,
                        nodeA.GetComponent<NodeData>().row]);
                }
            }
            else if (nodeA.GetComponent<NodeData>().col < nodeB.GetComponent<NodeData>().col) { //move right
                dist += Mathf.Abs(nodeB.GetComponent<NodeData>().col - nodeA.GetComponent<NodeData>().col);
                for (int i = 1; i != dist; i++) {
                    _path.Add(_boardManager._map[nodeA.GetComponent<NodeData>().col + i,
                        nodeA.GetComponent<NodeData>().row]);
                }
            }
            else if (nodeA.GetComponent<NodeData>().row > nodeB.GetComponent<NodeData>().row) { //move down
                dist += Mathf.Abs(nodeA.GetComponent<NodeData>().row - nodeB.GetComponent<NodeData>().row);
                for (int i = 1; i != dist; i++) {
                    _path.Add(_boardManager._map[nodeA.GetComponent<NodeData>().col,
                        nodeA.GetComponent<NodeData>().row - i]);
                }
            }
            else { //move up
                dist += Mathf.Abs(nodeB.GetComponent<NodeData>().row - nodeA.GetComponent<NodeData>().row);
                for (int i = 1; i != dist; i++) {
                    _path.Add(_boardManager._map[nodeA.GetComponent<NodeData>().col,
                        nodeA.GetComponent<NodeData>().row + i]);
                }
            }
            _path[_path.Count - 1].GetComponent<NodeData>().SetNode_Last(true);
        }



        public void Move() {
            _path[0].GetComponent<NodeData>().SetNode_Last(false);
            _path[0].GetComponent<NodeData>().unitOnNode = "none";
            _path[_path.Count - 1].GetComponent<NodeData>().unitOnNode = "ally";
            _path[_path.Count - 1].GetComponent<NodeData>().thisUnit = _path[0].GetComponent<NodeData>().thisUnit;
            //_path[_path.Count - 1].GetComponent<NodeData>().thisUnit.GetComponent<UnitData>().TurnReset();
            Vector3[] paths=new Vector3[_path.Count];
            for (int i = 0; i < _path.Count; i++) {
                paths[i] = _path[i].transform.position;
            }
            _path[0].GetComponent<NodeData>().thisUnit.GetComponent<UnitMovement>().Move(paths);
            _path[0].GetComponent<NodeData>().thisUnit = null;
            RemoveAllPathNodes();
            RemoveAllPathLines();
            ClearBlockSubset();
        }

        public void BuildPath(GameObject node) {
            if (_path.Count < 1) {
                _path.Add(node);
            }
            else {
                AddNodes(_path[_path.Count - 1], node);
                _path[0].GetComponent<NodeData>().thisUnit.GetComponent<UnitData>().SetMoveRange(
                    _path[0].GetComponent<NodeData>().thisUnit.GetComponent<UnitData>().GetMoveRange() -
                    (_path.Count - 1));
                DrawPathLines();
            }

            findCircRange(_path[_path.Count - 1]);
            _path[_path.Count - 1].GetComponent<NodeData>().SetNode_Last(true);
        }


        public void AI_path_simple(GameObject start, GameObject finish) {
            Debug.Log("hello");
            RemoveAllPathNodes();
            _path.Add(start);
            int destCol = finish.GetComponent<NodeData>().col;
            int destRow = finish.GetComponent<NodeData>().row;
            int curCol = _path[_path.Count - 1].GetComponent<NodeData>().col;
            int curRow = _path[_path.Count - 1].GetComponent<NodeData>().row;
            //while (curCol + 1 != destCol && curCol - 1 != destCol && curRow + 1 != destRow && curRow - 1 != destRow) {
                while ((curCol + 1 != destCol || curRow != destRow) && (curCol - 1 != destCol || curRow != destRow) && (curRow + 1 != destRow || curCol != destCol) && (curRow - 1 != destRow || curCol != destCol)) {
                if (Mathf.Abs((curCol+1)-destCol)< Mathf.Abs(curCol - destCol)) {//right
                    _path.Add(_boardManager._map[curCol + 1, curRow]);
                    curCol += 1;
                }
                else if (Mathf.Abs((curCol - 1) - destCol) < Mathf.Abs(curCol - destCol)) {//left
                    _path.Add(_boardManager._map[curCol - 1, curRow]);
                    curCol -= 1;
                } 
                else if (Mathf.Abs((curRow + 1) - destRow) < Mathf.Abs(curRow - destRow)) {//up
                    _path.Add(_boardManager._map[curCol, curRow+1]);
                    curRow += 1;
                } 
                else if (Mathf.Abs((curRow - 1) - destRow) < Mathf.Abs(curRow - destRow)) {//down
                    _path.Add(_boardManager._map[curCol, curRow - 1]);
                    curRow -= 1;
                }
                _path[_path.Count - 1].GetComponent<NodeData>().SetMovable(true);
            }
            Vector3[] nodePos=new Vector3[_path.Count];
            for(int i=0;i<nodePos.Length;i++) {
                nodePos[i] = _path[i].transform.position;
                Debug.Log("NODES?");
            }
            //DrawPathLines();
            _path[0].GetComponent<NodeData>().thisUnit.GetComponent<UnitMovement>().Move(nodePos);
            _path[_path.Count - 1].GetComponent<NodeData>().thisUnit = _path[0].GetComponent<NodeData>().thisUnit;
           
            _path[_path.Count - 1].GetComponent<NodeData>().unitOnNode = "enemy";
            _path[0].GetComponent<NodeData>().unitOnNode = "none";
            _path[0].GetComponent<NodeData>().thisUnit = null;
            RemoveAllPathNodes();
        }




        public void findCircRange(GameObject node) {

            ClearBlockSubset();
            _subsetPointer = 0;
            int col = node.GetComponent<NodeData>().col;
            int row = node.GetComponent<NodeData>().row;
            int range = _path[0].GetComponent<NodeData>().thisUnit.GetComponent<UnitData>().GetMoveRange();
            int rangeDec = range + 1;

            if (_boardManager.mode == 2) {
                range = _path[0].GetComponent<NodeData>().thisUnit.GetComponent<UnitData>().GetAttackRange();
                rangeDec = range + 1;
            }
            for (int x = 1; x < range + 1; x++) {
                rangeDec--;
                if (col - x > -1) { //left
                    _nodeSubset[_subsetPointer++] = _boardManager._map[col - x, row];
                    _boardManager._map[col - x, row].GetComponent<NodeData>().SetMovable(true);
                }

                if (col + x < _boardManager._mapWidth) { //right
                    _nodeSubset[_subsetPointer++] = _boardManager._map[col + x, row];
                    _boardManager._map[col + x, row].GetComponent<NodeData>().SetMovable(true);
                }

                if (row - x > -1) { //down
                    _nodeSubset[_subsetPointer++] = _boardManager._map[col, row - x];
                    _boardManager._map[col, row - x].GetComponent<NodeData>().SetMovable(true);
                }

                if (row + x < _boardManager._mapWidth) { //up
                    _nodeSubset[_subsetPointer++] = _boardManager._map[col, row + x];
                    _boardManager._map[col, row + x].GetComponent<NodeData>().SetMovable(true);
                }

                for (int y = 1; y < rangeDec; y++) {
                    if (col + x < _boardManager._mapWidth && row + y < _boardManager._mapWidth) { //up right
                        _nodeSubset[_subsetPointer++] = _boardManager._map[col + x, row + y];
                        _boardManager._map[col + x, row + y].GetComponent<NodeData>().SetMovable(true);
                    }

                    if (col + x < _boardManager._mapWidth && row - y > -1) { //down right
                        _nodeSubset[_subsetPointer++] = _boardManager._map[col + x, row - y];
                        _boardManager._map[col + x, row - y].GetComponent<NodeData>().SetMovable(true);
                    }

                    if (col - x > -1 && row + y < _boardManager._mapWidth) { //up left
                        _nodeSubset[_subsetPointer++] = _boardManager._map[col - x, row + y];
                        _boardManager._map[col - x, row + y].GetComponent<NodeData>().SetMovable(true);
                    }

                    if (col - x > -1 && row - y > -1) { //down left
                        _nodeSubset[_subsetPointer++] = _boardManager._map[col - x, row - y];
                        _boardManager._map[col - x, row - y].GetComponent<NodeData>().SetMovable(true);
                    }
                }
            }
        }


        public void ClearBlockSubset() {
            for (int i = 0; i < _subsetPointer; i++) {
                _nodeSubset[i].GetComponent<NodeData>().SetMovable(false);
                _nodeSubset[i].GetComponent<NodeData>().HighlightOff();
                _nodeSubset[i] = null;
            }
            _subsetPointer = 0;
        }







        public void DrawPathLines() {
            for (int i = _pathLines.Count; i < _path.Count - 1; i++) {
                DrawLine(
                    new Vector3(
                        _path[i].transform.position.x,
                        _path[i].transform.position.y + 3,
                        _path[i].transform.position.z),
                    new Vector3(
                        _path[i + 1].transform.position.x,
                        _path[i + 1].transform.position.y + 3,
                        _path[i + 1].transform.position.z),
                    Color.red//colorz[i]
                );
            }
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
            _pathLines.Add(myLine);
        }
    }
}