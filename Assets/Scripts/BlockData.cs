using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using Assets.Scripts;

public class BlockData : MonoBehaviour {

    //[XmlAttribute("name")]
    public GameObject Map;
    public GameObject Unit;
    private BoardManager _boardManager;
    public int column;
    public int row;
    public float height = 20;
    public string geography;
    public string desc;
    private int mat_tile;
    private int mat_base;
    private int blockID;
    public Material[] mats;
    public Material[] mats_selector;
    private bool isHovered = false;
    public bool isSelected = false;
    public string unitz;
    public bool movableArea = false;

    void Start() {
        transform.GetChild(2).GetComponent<Renderer>().enabled = false;
        Map = GameObject.Find("Map");
        _boardManager = GameObject.Find("Map").GetComponent<BoardManager>();
    }


    void OnMouseDown() {
        Debug.Log("Clicked: " + name);

        switch (_boardManager.HasUnit(column,row)) {
            case "none":
                Debug.Log("Nothing here");
                _boardManager.MoveUnit(2,0);
                break;
            case "ally":
                Debug.Log("Where to Move unit?!?!?!");
                isSelected = true;
                _boardManager.MovementRange(column,row,5);
                break;
            case "enemy":
                Debug.Log("Taliban!");
                break;
            default:
                break;

        }

    }
    void OnMouseOver() {
        if (!isHovered) {
            isHovered = true;
            transform.GetChild(2).GetComponent<Renderer>().enabled = true;
            switch (_boardManager.HasUnit(column,row)) {
                case "none":
                    if (!movableArea) {
                        //Debug.Log("FUCK EVERYTHING");
                        transform.GetChild(2).GetComponent<Renderer>().material = mats_selector[0];
                    }

                    break;
                case "ally":
                    transform.GetChild(2).GetComponent<Renderer>().material = mats_selector[1];
                    break;
                case "enemy":
                    transform.GetChild(2).GetComponent<Renderer>().material = mats_selector[2];
                    break;
                default:
                    break;

            }
        }
    }

    public void setMovable() {
        transform.GetChild(2).GetComponent<Renderer>().material = mats_selector[2];
        transform.GetChild(2).GetComponent<Renderer>().enabled = true;
        movableArea = true;
    }

    void OnMouseExit() {
        if (!movableArea) {
            isHovered = false;
            transform.GetChild(2).GetComponent<Renderer>().enabled = false;
        }
    }





















    public void Initialize(int id,string geo,float h,string desc, string unit) {
        //renderer = GetComponentsInChildren<Renderer>();
        blockID = id;
        height = h;
        geography = geo;
        this.desc = desc;
        unitz = unit;
        SetTexture();
        //transform.position.x = h;
        if (unitz=="ally") {
            GameObject thisUnit = Instantiate(Unit, new Vector3(transform.position.x, transform.position.y+2.5f, transform.position.z), Quaternion.identity, transform);
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

