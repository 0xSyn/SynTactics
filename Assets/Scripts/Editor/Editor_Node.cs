using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
//using UnityEditor.IMGUI.Controls;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;


//using Assets.Scripts;
namespace Assets.Scripts {
    public class Editor_Node : MonoBehaviour {

        public MeshFilter[] meshAry;
        public GameObject Map;
        public GameObject Unit;
        public GameObject thisUnit;
        private Editor _levelEditor;
        private BoardPathing _path;
        public int x;
        public int y;
        private int mat_tile;
        private int mat_base;
        public int blockID;
        public Material[] mats;
        public Material[] mats_selector;
        private bool isHovered;

        //private GameObject
        public string unitOnNode;
        public bool movableArea;
        private Renderer _nodeHighlight;

        void Start() {
            transform.GetChild(2).GetComponent<Renderer>().enabled = false;
            //Map = GameObject.Find("Mana");
            _levelEditor = GameObject.Find("Manager").GetComponent<Editor>();
            _path = GetComponent<BoardPathing>();
            _nodeHighlight = transform.GetChild(2).GetComponent<Renderer>();
            SetMesh();
        }


        void OnMouseDown() {
            _levelEditor.SelectNode(x,y);


        }
        void OnMouseOver() {
            if (!isHovered) {
                isHovered = true;
                _nodeHighlight.enabled = true;
            }
        }


        void OnMouseExit() {
            isHovered = false;
            _nodeHighlight.enabled = false;
        }























        private void SetMesh() {


            MeshFilter meshF = transform.GetChild(0).GetComponent<MeshFilter>();
            meshF.mesh = meshAry[0].sharedMesh;
            transform.GetChild(1).GetComponent<Renderer>().enabled = false;

        }







        public void SetTexture() {

            Material[] _m = transform.GetChild(0).GetComponent<MeshRenderer>().materials;
            _m[0] = mats[mat_base];//bottom
            _m[1] = mats[mat_tile];//top
            _m[2] = mats[mat_base];//back
            _m[3] = mats[mat_base];//right
            _m[4] = mats[mat_base];//front
            _m[5] = mats[mat_base];//left


            transform.GetChild(0).GetComponent<Renderer>().materials = _m;
        }
    }
}
