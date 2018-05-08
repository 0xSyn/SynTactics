using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Editor_UI : MonoBehaviour {
    private Editor _editor;
    public InputField in_x;
    public InputField in_y;
    public InputField in_title;
    public GameObject panelBoard;
    public GameObject panelNode;
    public Text txt_nodeId;
    public GameObject panelUnit;

    void Start() {
        _editor = GetComponent<Editor>();
        SetPanel_Board();
    }

    public void BTN_LoadBlocks() {
        _editor.LoadBlocks(Int32.Parse(in_x.text), Int32.Parse(in_y.text));
    }


    public void SetPanel_Node() {
        panelBoard.SetActive(false);
        panelNode.SetActive(true);
    }

    public void SetPanel_Board() {
        panelBoard.SetActive(true);
        panelNode.SetActive(false);
    }

    public void BTN_RaiseNode() {
        _editor.AdjustNodeHeight(.25f);
    }
    public void BTN_LowerNode() {
        _editor.AdjustNodeHeight(-.25f);
    }

    public void SetNodeID(int x,int y) {
        txt_nodeId.text="Node ID: "+x+"_"+y;
    }



    public void BTN_Save() {
        _editor.ExportXML(in_title.text);
    }
    public void BTN_Load() {
        _editor.ImportXML(in_title.text);
    }

}
