using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class UnitPath : MonoBehaviour {

    List<int> path = new List<int>();
    private BoardManager _boardManager;

    private void Start() {
        _boardManager = GetComponent<BoardManager>();
    }

    public void BuildPath(int nodeIndex) {
        path.Add(nodeIndex);
        Debug.Log("Index0" + path[0]);//Item[0]);
        if (path.Count > 1) { DrawPath(nodeIndex ); }
    }

    public void DrawPath(int nodeIndex) {
        //Debug.Log("dsa"+ path.IndexOf(0));
        DrawLine(
            _boardManager.nodePos(path[0]),
            _boardManager.nodePos(path[1]),
            Color.blue
        );
    }


    void DrawLine(Vector3 start, Vector3 end, Color color, float duration = 0.2f) {
        //var otherScript: BoardManager = GetComponent(BoardManager);
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
