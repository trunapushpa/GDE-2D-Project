﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarbonMeter : MonoBehaviour
{
    public static float progress = 0;
    public Vector2 position = new Vector2(Screen.width*0.02f, Screen.height*0.98f);
    public Vector2 size = new Vector2(400, 50);
    public Texture2D emptyImage;
    public Texture2D fullImage;
    public float speed = 0;
    bool check = false;
    
    // Start is called before the first frame update
    void OnGUI()
    {
        // Vector2 pivot = new Vector2(Screen.width/2, Screen.height/2);
        // GUIUtility.RotateAroundPivot (-90.0f, pivot);
        GUI.DrawTexture(new Rect(position.x, position.y, size.x, size.y), emptyImage);
        GUI.DrawTexture(new Rect(position.x, position.y, size.x * Mathf.Clamp01(progress), size.y), fullImage);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public static void changeCarbonLevel(int num_pipes, int num_farms) {
        float carbon_pipe = 0.01f;
        float carbon_farm = -0.01f;
        progress = num_pipes * carbon_pipe + num_farms * carbon_farm;
        progress = Mathf.Max(progress, 0.0f);
    }

    public static bool isFull() {
        return progress >= 1.0f;
    }
}
