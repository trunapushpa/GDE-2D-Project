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
        messagePanel.transform.localScale.Scale(new Vector3(Screen.width, Screen.height, 0));
        textMessage.fontSize = Screen.width/10;
    }

    // Update is called once per frame
    void Update() {
        if(CarbonMeter.isWin()) {
            messagePanel.gameObject.SetActive(true);
            textMessage.text = "Level Cleared!!";
        } else if(CarbonMeter.isFull()) {
            messagePanel.gameObject.SetActive(true);
            textMessage.text = "Game Over!!";
        }
        if(Input.GetKey(KeyCode.Escape) && isGameOver()) {
            CarbonMeter.changeCarbonLevel(0, 0);
            BuildingsMeter.ChangeBuildingsLevel(0, 0);
            SceneManager.LoadScene("start");
            CarbonMeter.setWin(false);
        }
    }

    public static bool isGameOver() {
        return CarbonMeter.isFull() || CarbonMeter.isWin();
    }
}
