using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Assets.Scripts;

public class UnitLoader : NetworkBehaviour {
    public GameObject Unit;

    [Command]
    public void CmdCreateUnits(GameObject node, int _mapHeight, int _mapWidth) {
        Debug.Log("Creating Unit");
        if (node.GetComponent<NodeData>().unitOnNode == "ally") {
            Vector3 pos = node.transform.position;
            node.GetComponent<NodeData>().thisUnit = Instantiate(Unit, new Vector3(pos.x, pos.y + 2.5f, pos.z), Quaternion.identity, transform);
            NetworkServer.Spawn(node.GetComponent<NodeData>().thisUnit);
            //NetworkServer.SpawnWithClientAuthority(node.GetComponent<NodeData>().thisUnit, connectionToClient);
        }

    }
    /*
    [Command]
    public void CmdCreateUnits(GameObject[,] _map, int _mapHeight, int _mapWidth) {
        Debug.Log("Creating Unit");
        for (int col = 0; col < _mapWidth; col++) {
            for (int row = 0; row < _mapHeight; row++) {
                if (_map[col, row].GetComponent<NodeData>().unitOnNode == "ally") {
                    Vector3 pos = _map[col, row].transform.position;
                    _map[col, row].GetComponent<NodeData>().thisUnit = Instantiate(Unit, new Vector3(pos.x, pos.y + 2.5f, pos.z), Quaternion.identity, transform);
                    NetworkServer.Spawn(_map[col, row].GetComponent<NodeData>().thisUnit);
                }




            }
        }
        //node.GetComponent<NodeData>()

    }
    */

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
}
