using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenBox1 : MonoBehaviour
{
    public GameObject game;
    public Text gameText;
    public GameObject textObject;
    // Start is called before the first frame update
    void Start()
    {
        if (game == null)
        {
            game = GameObject.Find("Main Camera");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Hunter")
        {
            game.GetComponent<TreasureHunter_1>().winned = true;

            game.GetComponent<TreasureHunter_1>().dBox.SetActive(true);
            game.GetComponent<TreasureHunter_1>().yesButton.SetActive(true);
            game.GetComponent<TreasureHunter_1>().noButton.SetActive(true);

            textObject = GameObject.Find("Dialog");
            gameText = textObject.GetComponent<Text>();
            gameText.text = "Congratulation!!! You gained a real treasure, the gold has been deposited to your bank!\nDo you want to return to first level?";

            PauseGame();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }
    public void StartGame()
    {
        Time.timeScale = 1;
    }
}
