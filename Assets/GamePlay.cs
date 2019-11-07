using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GamePlay : MonoBehaviour
{
    public Text textMessage;
    public RectTransform messagePanel;
    // private CarbonMeter carbonMeter = gameObject.GetComponent<CarbonMeter>();
    // Start is called before the first frame update
    void Start()
    {
        messagePanel.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        if(CarbonMeter.isFull()) {
            messagePanel.gameObject.SetActive(true);
            textMessage.text = "Game Over!";
        }
    }

    public static bool isGameOver() {
        return CarbonMeter.isFull();
    }
}
