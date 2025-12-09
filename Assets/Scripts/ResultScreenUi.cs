using UnityEngine;
using TMPro;

public class ResultScreenUi : MonoBehaviour
{
    public TMP_Text timeText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeText.text = "Time: " + Magnet.timeElapsed.ToString("F2") + " seconds \n Good Job!";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
