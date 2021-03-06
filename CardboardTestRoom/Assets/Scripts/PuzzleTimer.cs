﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System;

public class PuzzleTimer : MonoBehaviour {

    public Text timerText;

    private float elapsed;

   

    private bool active = false;

    void Awake()
    {
        
        elapsed = 0.0f;
        print(StaticClass.Timer.ToString("F"));
    }

    // Update is called once per frame
    void Update ()
    {
        if(active)
        {
            elapsed += Time.deltaTime;
        }
        
        timerText.text = elapsed.ToString("F");
	}
    //Write angle information to a file. Used to determine jerk
    /*void WriteToFile()
    {
        string contents = Camera.main.transform.localEulerAngles.y.ToString() + "," + elapsed.ToString("F");
        string FILE_PATH2 = Application.persistentDataPath + "/AngleFile.csv";
        File.AppendAllText(FILE_PATH2, Environment.NewLine + contents);
        print("Wrote to file");
        print(contents);
    }*/

    public void ToggleActive(bool toggle)
    {
        active = toggle;
    }

    public float GetElapsed()
    {
        return elapsed;
    }
}
