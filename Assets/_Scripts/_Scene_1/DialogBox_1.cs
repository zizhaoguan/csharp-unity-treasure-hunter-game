using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogBox_1 : MonoBehaviour
{
    public Text gameText;
    GameObject textObject;
    private List<string> textList;
    public GameObject game;
    public GameObject hunter;


    // Start is called before the first frame update
    void Start()
    {
        
        if (hunter == null || game == null)
        {
            hunter = GameObject.Find("_Hunter");
            game = GameObject.Find("Main Camera");

        }
        //can use this to instead of placing the Main Camera GameObject into the inspector.

        //if (game == null)
        //{
        //    game = GameObject.Find("Main Camera");
        //}


        textObject = GameObject.Find("Dialog");
        gameText = textObject.GetComponent<Text>();
        gameText.text = "Click the arrow to continue to see context";

        textList = new List<string>();
        textList.Add(" Now you can click the right top button to check the map.");

        textList.Add("For this level, you can find the treasure using the map.");
        textList.Add("There are 3 buttons over there, be careful");
        textList.Add("A: left\nD: right\nW: jump\n jump to catch rope then W + D to climb");
        PauseGame();
    }

    // Update is called once per frame
    void Update()
    {

        if (hunter.GetComponent<Hunter>().meetItem == true)
        {
            gameText.text = "You found an item, do you want to check?";
        }
        else if (hunter.GetComponent<Hunter>().lostChance)
        {
            if (!Lost())
            {
                if (hunter.GetComponent<Hunter>().liar)
                {
                    gameText.text = "You are liar...\n" + hunter.GetComponent<Hunter>().GetGameChances() + " game chances left...";
                    hunter.GetComponent<Hunter>().liar = false;
                }
                else
                {
                    gameText.text = "You have been killed...\n" + hunter.GetComponent<Hunter>().GetGameChances() + " game chances left...";
                }

                hunter.GetComponent<Hunter>().setLostChance(false);
            }
            else
            {
                if (hunter.GetComponent<Hunter>().liar)
                {
                    gameText.text = "You are liar...Unfortunately...You failed...Do you want to restart the game?";
                    hunter.GetComponent<Hunter>().liar = false;
                }
                else
                {
                    gameText.text = "Unfortunately...You failed...Do you want to restart the game?";
                }

                hunter.GetComponent<Hunter>().setLostChance(false);
            }
        }
        
    }
    public void ClickContinue()
    {
        if (textList.Count > 0)
        {
            gameText.text = textList[0];
            textList.RemoveAt(0);
        }
        else
        {
            game.GetComponent<TreasureHunter_1>().continueBotton.SetActive(false);
            game.GetComponent<TreasureHunter_1>().dBox.SetActive(false);
            StartGame();
        }
    }
    public void MakeChoice()
    {
        if (Lost())
        {
            SceneManager.LoadScene("_Scene_1");
        }  
    }
    public void CloseDialogbox()
    {
        if (Lost() || Winned())
        {
            Application.Quit(); 
        }
        else
        {
            hunter.GetComponent<Hunter>().setMeetItem(false);
            game.GetComponent<TreasureHunter_1>().continueBotton.SetActive(false);
            game.GetComponent<TreasureHunter_1>().okButton.SetActive(false);
            game.GetComponent<TreasureHunter_1>().cancelButton.SetActive(false);
            game.GetComponent<TreasureHunter_1>().dBox.SetActive(false);
            StartGame();
        }

    }
    public bool Lost()
    {
        return game.GetComponent<TreasureHunter_1>().lost;
    }
    public bool Winned()
    {

        return game.GetComponent<TreasureHunter_1>().winned;
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
    }
    private void StartGame()
    {
        Time.timeScale = 1;
        hunter.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f);
    }

}
