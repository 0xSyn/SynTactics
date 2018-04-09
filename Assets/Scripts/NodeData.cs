using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using Assets.Scripts;

namespace Assets.Scripts {


    public class NodeData : MonoBehaviour {

        //[XmlAttribute("name")]
        public GameObject Map;
        public GameObject Unit;
        public GameObject thisUnit;
        private BoardManager _boardManager;
        private BoardPathing _path;
        public int col;
        public int row;
        public float height = 20;
        public string geography;
        public string desc;
        private int mat_tile;
        private int mat_base;
        public int blockID;
        public Material[] mats;
        public Material[] mats_selector;
        private bool isHovered;
        private bool _isSelectedForMove;
        public bool isSelected;
        //private GameObject
        public string unitOnNode;
        public bool movableArea;
        private Renderer _nodeHighlight;

        void Start() {
            transform.GetChild(2).GetComponent<Renderer>().enabled = false;
            Map = GameObject.Find("Map");
            _boardManager = GameObject.Find("Map").GetComponent<BoardManager>();
            _path = GetComponent<BoardPathing>();
            _nodeHighlight = transform.GetChild(2).GetComponent<Renderer>();
        }


        void OnMouseDown() {
            //Debug.Log("Clicked: " + name);

            switch (unitOnNode) {

                case "none":
                    if (_isSelectedForMove) {
                        Debug.Log("makinMoviez");
                        _boardManager.MoveUnit();
                    }
                    else if (_boardManager.isMovementModeActive()) {
                        _boardManager.AddToPath(blockID);
                    } 
                    else {
                        Debug.Log("Node Empty");
                    }
                    break;

                case "ally":
                    Debug.Log("ally");
                    isSelected = true;
                    _boardManager.Selected(col, row);
                    break;

                case "enemy":
                    Debug.Log("Enemy Unit");
                    break;

                default:
                    break;

            }

        }
        void OnMouseOver() {
            if (!isHovered) {
                isHovered = true;

                _nodeHighlight.enabled = true;
                switch (unitOnNode) {
                    case "none":
                        if (_isSelectedForMove) {
                            _nodeHighlight.material = mats_selector[5];
                        } else if (movableArea) {

                            _nodeHighlight.material = mats_selector[3];
                        } else {
                            _nodeHighlight.material = mats_selector[0];
                        }

                        break;
                    case "ally":
                        _nodeHighlight.material = mats_selector[1];
                        break;
                    case "enemy":
                        _nodeHighlight.material = mats_selector[2];
                        break;
                    default:
                        break;

                }
            }
        }


        void OnMouseExit() {
            isHovered = false;
            if (_isSelectedForMove) {
                _nodeHighlight.material = mats_selector[5];
            } else if (movableArea) {
                _nodeHighlight.material = mats_selector[2];

            } else {

                _nodeHighlight.enabled = false;
            }
        }


        public void HighlightOff() {
            _nodeHighlight.enabled = false;
        }




        public void SetNode_Last(bool b) {
            //Debug.Log("SDADKLSAJD");
            if (b) {
                _isSelectedForMove = true;
                _nodeHighlight.material = mats_selector[5];
                _nodeHighlight.enabled = true;
            }
            else {
                _isSelectedForMove = false;
                //_nodeHighlight.material = mats_selector[5];
                _nodeHighlight.enabled = true;
            }

        }

        void SetNode_Walkable() {
            _nodeHighlight.material = mats_selector[2];
            _nodeHighlight.enabled = true;
            movableArea = true;
        }


        public void SetMovable(bool moveable) {
            if (moveable) {
                SetNode_Walkable();
            } else {
                _nodeHighlight.material = mats_selector[1];
                _nodeHighlight.enabled = false;
                movableArea = false;
            }
        }























        public void Initialize(int id, string geo, float h, string desc, string unit) {
            blockID = id;
            height = h;
            geography = geo;
            this.desc = desc;
            unitOnNode = unit;
            SetTexture();
            if (unitOnNode == "ally") {
                thisUnit = Instantiate(Unit, new Vector3(transform.position.x, transform.position.y + 2.5f, transform.position.z), Quaternion.identity, transform);
                //Debug.Log(" range " + thisUnit.GetComponent<UnitData>().GetMoveRange());
            }

        }



















        public void SetTexture() {
            switch (geography) {
                case "grass":
                    mat_tile = 0;
                    mat_base = 1;
                    break;
                case "water":
                    mat_tile = 5;
                    mat_base = 5;
                    break;
                case "sand":
                    mat_tile = 2;
                    mat_base = 2;
                    break;
                case "rock":
                    mat_tile = 3;
                    mat_base = 3;
                    break;
                case "red":
                    mat_tile = 3;
                    mat_base = 4;
                    break;
                case "crate":
                    mat_tile = 6;
                    mat_base = 6;
                    break;
                default:
                    break;


            }


            transform.GetChild(0).GetComponent<Renderer>().material = mats[mat_base];
            transform.GetChild(1).GetComponent<Renderer>().material = mats[mat_tile];
        }
    }
}