using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelEditor_UI : MonoBehaviour {
    private LevelEditor _editor;
    public InputField in_x;
    public InputField in_y;
    public InputField in_title;

    void Start() {
        _editor = GetComponent<LevelEditor>();
    }

    public void BTN_LoadBlocks() {
        _editor.LoadBlocks(Int32.Parse(in_x.text), Int32.Parse(in_y.text));
    }
}
