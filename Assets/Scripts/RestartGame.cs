using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    public TMP_Text restartGame;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        restartGame.text = "Rope broke, keep an eye on your remaining newtons \n Press R to restart the game";
    }

    // Update is called once per frame
    void Update()
    {
         if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
