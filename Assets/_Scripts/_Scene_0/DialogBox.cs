using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogBox: MonoBehaviour
{
    public Text gameText;
    GameObject textObject;
    private List<string> textList;
    public GameObject game;
    public GameObject hunter;
    public GameObject map1;
    public GameObject map2;
    public GameObject map3;


    // Start is called before the first frame update
    void Start()
    {

        if (hunter == null || game == null)
        {
            hunter = GameObject.Find("_Hunter");
          
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
        textList.Add(" Once upon a time, there was a famous treasure hunter." +
        " He devoted his life to treasure hunt. However, for the last adventure, he sacrificed...(Left click mouse to continue...)");

        textList.Add("Hundreds of years later, you became a brave treasure hunter but unfortunately lost all your money in Las Vegas..." +
                     "Then, you want to find the treasure to pay your bills." +
                    " No one believes you will succeed. It's time to prove you are a great treasure hunter!");
            textList.Add("For the first stage, you need to find 2 maps");
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
        else if (Winned())
        {
            
            gameText.text = "Hooray! You have found 2 maps (There are 3 maps in this game)~ "
                       + "Do you want to move to the 2nd stage?";
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
            game.GetComponent<TreasureHunter>().continueBotton.SetActive(false);
            game.GetComponent<TreasureHunter>().dBox.SetActive(false);
            StartGame();
        }
    }
    public void MakeChoice()
    {
        if (Lost())
        {
            SceneManager.LoadScene("_Scene_0");
        }
        else if (Winned())
        {
            //SceneManager.LoadScene("_Scene_1");
            Global.instance.storeMap();

            SceneManager.LoadSceneAsync("_Scene_1", LoadSceneMode.Single);
            //Scene _scene_1 = SceneManager.GetSceneByName("_Scene_1");
            //SceneManager.SetActiveScene(_scene_1);
            
        }
        else
        {
            game.GetComponent<TreasureHunter>().okButton.SetActive(false);

            if (hunter.GetComponent<Hunter>().CheckMap())
            {
                gameText.text = "You found one treasure map!";

            }
            else { gameText.text = "Yeah!!! You found a real fried chicken recipe!!! But you are still poor..."; }
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
            game.GetComponent<TreasureHunter>().continueBotton.SetActive(false);
            game.GetComponent<TreasureHunter>().okButton.SetActive(false);
            game.GetComponent<TreasureHunter>().cancelButton.SetActive(false);
            game.GetComponent<TreasureHunter>().dBox.SetActive(false);
            StartGame();
        }

    }
    public bool Lost()
    {
        return game.GetComponent<TreasureHunter>().lost;
    }
    public bool Winned()
    {
        return game.GetComponent<TreasureHunter>().winned;
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
