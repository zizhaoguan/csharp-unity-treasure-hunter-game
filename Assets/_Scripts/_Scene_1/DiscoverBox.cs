using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiscoverBox : MonoBehaviour
{
    public bool controlBox1;
    public bool controlBox2;
    public bool controlBox3;

    public bool mirrorReady;

    public GameObject box1;
    public GameObject box2;
    public GameObject box3;

    public GameObject game;

    public Text gameText;
    GameObject textObject;
    // Start is called before the first frame update
    void Start()
    {

        box1.SetActive(false);
        box2.SetActive(false);
        box3.SetActive(false);
        mirrorReady = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {
        
        if (controlBox1 && other.gameObject.tag == "Hunter")
        { 
            box1.SetActive(true);
        }
        if (controlBox2 && other.gameObject.tag == "Hunter")
        {
            box2.SetActive(true);
        }
        if (controlBox3 && other.gameObject.tag == "Hunter" && mirrorReady)
        {
            mirrorReady = false;
            game.GetComponent<TreasureHunter_1>().dBox.SetActive(true);
            game.GetComponent<TreasureHunter_1>().yesButton.SetActive(true);
            game.GetComponent<TreasureHunter_1>().noButton.SetActive(true);
            
            textObject = GameObject.Find("Dialog");
            gameText = textObject.GetComponent<Text>();
            gameText.text = "Do you think you are very handsome?";
            PauseGame();
        }
    }
    public void discover3rdBox()
    {
        box3.SetActive(true);
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
