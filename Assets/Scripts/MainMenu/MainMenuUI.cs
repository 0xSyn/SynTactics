﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void loadlevel(string level) {
        SceneManager.LoadScene(level);

    }
    public void BTN_LoadMap() {
        loadlevel("Maps/CombatBoard");
    }
    public void BTN_LevelEditor() {
        loadlevel("Maps/LevelEditor");
    }
    public void BTN_Quit() {
        Application.Quit();
    }
    public void BTN_Options() {
        
    }
}
