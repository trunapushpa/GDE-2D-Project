using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingsMeter : MonoBehaviour
{
    public static float progress = 0;
    private Vector2 position = new Vector2(Screen.width*0.65f, Screen.height*0.05f);
    private Vector2 size = new Vector2(Screen.width*0.25f, Screen.height*0.05f);
    private Vector2 _skull_size = new Vector2(Screen.height*0.05f, Screen.height*0.05f);
    private Vector2 _skull_position = new Vector2(Screen.width*0.65f - 1.5f*Screen.height*0.05f, Screen.height*0.05f);
    public Texture2D emptyImage;
    public Texture2D fullImage;
    public Texture2D skullImage;
    public float speed = 0;
    bool check = false;
    
    // Start is called before the first frame update
    void OnGUI()
    {
        // Vector2 pivot = new Vector2(Screen.width/2, Screen.height/2);
        // GUIUtility.RotateAroundPivot (-90.0f, pivot);
        GUI.DrawTexture(new Rect(position.x, position.y, size.x, size.y), emptyImage);
        GUI.DrawTexture(new Rect(_skull_position.x, _skull_position.y, _skull_size.x, _skull_size.y), skullImage);
        GUI.DrawTexture(new Rect(position.x, position.y, size.x * Mathf.Clamp01(progress), size.y), fullImage);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public static void ChangeBuildingsLevel(int current_buildings, int total_buildings) {
        // TODO : Set values
        progress = ((float) current_buildings) / ((float) total_buildings);
        progress = Mathf.Max(progress, 0.0f);
    }

    public static bool isFull() {
        return progress >= 1.0f;
    }
}
