using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class DialogBox2 : MonoBehaviour
{
    public GameObject game;
    public GameObject danger;
    public GameObject hunter;

    public Text gameText;
    GameObject textObject;

    // Start is called before the first frame update
    void Start()
    {
        if (hunter == null) { hunter = GameObject.Find("_Hunter"); }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Yes()
    {

        StartGame();
        game.GetComponent<TreasureHunter_1>().dBox.SetActive(false);
        game.GetComponent<TreasureHunter_1>().yesButton.SetActive(false);
        game.GetComponent<TreasureHunter_1>().noButton.SetActive(false);

        if (game.GetComponent<TreasureHunter_1>().path1)
        {
            if (game.GetComponent<TreasureHunter_1>().winned)
            {
                game.GetComponent<TreasureHunter_1>().winned = false;
                Global.instance.clearMapsList();
                SceneManager.LoadSceneAsync("_Scene_0", LoadSceneMode.Single);
            }

        }
        if (game.GetComponent<TreasureHunter_1>().path2)
        {
            if (game.GetComponent<TreasureHunter_1>().winned)
            {
                game.GetComponent<TreasureHunter_1>().winned = false;
                Global.instance.clearMapsList();
                SceneManager.LoadSceneAsync("_Scene_0", LoadSceneMode.Single);
                
            }
        }
        else if (game.GetComponent<TreasureHunter_1>().path3)
        {
            if (!game.GetComponent<TreasureHunter_1>().winned)
            {
                hunter.GetComponent<Hunter>().liar = true;
                GameObject dangerSlabstone = Instantiate<GameObject>(danger);
                dangerSlabstone.transform.position = new Vector3(33f, 50f, 0f);
                dangerSlabstone.GetComponent<Rigidbody>().useGravity = true;
            }
            else
            {
                game.GetComponent<TreasureHunter_1>().winned = false;
                Global.instance.clearMapsList();
                SceneManager.LoadSceneAsync("_Scene_0", LoadSceneMode.Single);
                
            }
           
        }
        
    }

    public void No()
    {
        StartGame();
        game.GetComponent<TreasureHunter_1>().yesButton.SetActive(false);
        game.GetComponent<TreasureHunter_1>().noButton.SetActive(false);
        textObject = GameObject.Find("Dialog");
        gameText = textObject.GetComponent<Text>();

        PauseGame();

        if (game.GetComponent<TreasureHunter_1>().path1)
        {
            if (game.GetComponent<TreasureHunter_1>().winned)
            {
                Application.Quit();
            }
        }
        else if (game.GetComponent<TreasureHunter_1>().path2)
        {
            if (game.GetComponent<TreasureHunter_1>().winned)
            {
                Application.Quit();
            }
        }
        else if (game.GetComponent<TreasureHunter_1>().path3)
        {
            if (!game.GetComponent<TreasureHunter_1>().winned)
            {
                GameObject mirror = GameObject.Find("Mirror");
                mirror.GetComponent<DiscoverBox>().discover3rdBox();
                Destroy(mirror);
                game.GetComponent<TreasureHunter_1>().cancelButton.SetActive(true);

                gameText.text = "You are very honest! You deserve this treature!";
            }
            else
            {
                Application.Quit();
            }
            
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
