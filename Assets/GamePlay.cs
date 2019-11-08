using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GamePlay : MonoBehaviour
{
    public Text textMessage;
    public RectTransform messagePanel;
    // private CarbonMeter carbonMeter = gameObject.GetComponent<CarbonMeter>();
    // Start is called before the first frame update
    void Start()
    {
        messagePanel.sizeDelta = new Vector2(Screen.width, Screen.height);
        messagePanel.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        if(CarbonMeter.isWin()) {
            messagePanel.gameObject.SetActive(true);
            textMessage.text = "Level Completed!!";
            if(Input.GetKey(KeyCode.Escape)) SceneManager.LoadScene("start");
        } else if(CarbonMeter.isFull()) {
            messagePanel.gameObject.SetActive(true);
            textMessage.text = "Game Over!!";
            if(Input.GetKey(KeyCode.Escape))  SceneManager.LoadScene("start");
        }
    }

    public static bool isGameOver() {
        return CarbonMeter.isFull();
    }
}
