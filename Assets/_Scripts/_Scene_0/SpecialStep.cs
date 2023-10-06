using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialStep : MonoBehaviour
{
    public Text gameText;
    public GameObject textObject;
    public GameObject game;

    bool noticeHunter = false;

    //public GameObject dBox;
    //public GameObject continueButton;
    //public GameObject okButton;
    //public GameObject cancelButton;

    private void Start()
    {
        //can place the Main Cammera GameObject in the inspector field directly, instead of this
        if (game == null) { game = GameObject.Find("Main Camera"); }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!noticeHunter && collision.gameObject.tag == "Hunter")
        {
            game.GetComponent<TreasureHunter>().dBox.SetActive(true);
            game.GetComponent<TreasureHunter>().cancelButton.SetActive(true);
            textObject = GameObject.Find("Dialog");
            gameText = textObject.GetComponent<Text>();
            gameText.text = "It looks like that there are 2 buttoms over there...One is danger...Be careful...";
            noticeHunter = true;
            game.GetComponent<TreasureHunter>().PauseGame();

        }
    }
}
