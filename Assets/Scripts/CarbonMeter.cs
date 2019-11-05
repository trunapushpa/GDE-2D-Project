using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarbonMeter : MonoBehaviour
{
    public static float progress = 0;
    public Vector2 position = new Vector2(Screen.width/2 + 100, Screen.height/2 - Screen.width/2 - 200);
    public Vector2 size = new Vector2(400, 50);
    public Texture2D emptyImage;
    public Texture2D fullImage;
    public float diff = 0.002f;
    public float speed = 0;
    bool check = false;
    
    // Start is called before the first frame update
    void OnGUI()
    {
        Vector2 pivot = new Vector2(Screen.width/2, Screen.height/2);
        GUIUtility.RotateAroundPivot (-90.0f, pivot);
        GUI.DrawTexture(new Rect(position.x, position.y, size.x, size.y), emptyImage);
        GUI.DrawTexture(new Rect(position.x, position.y, size.x * Mathf.Clamp01(progress), size.y), fullImage);
    }

    // Update is called once per frame
    void Update()
    {
        if(!check)
        {
            StartCoroutine(delay());
        }
    }

    IEnumerator delay()
    {
        check = true;
        if(!isFull())
            progress = progress + diff;
        yield return new WaitForSeconds(speed);
        check = false;
    }

    public static bool isFull() {
        return progress > 0.10f;
    }
}
