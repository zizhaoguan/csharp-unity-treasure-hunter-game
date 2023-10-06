using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureHunter_1 : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Set in Inspector")]

    public bool winned;
    public bool lost;

    public GameObject hunter;
    public GameObject dBox;
    public GameObject continueBotton;
    public GameObject okButton;
    public GameObject cancelButton;
    public GameObject yesButton;
    public GameObject noButton;

    public bool path1;
    public bool path2;
    public bool path3;

    public List<string> mapsList;

    // Start is called before the first frame update
    void Start()
    {
        mapsList = Global.instance.getMapList();
        //can place the _Hunter GameObject in the inspector to instead of this.
        if (hunter == null)
        {
            hunter = GameObject.Find("_Hunter");
        }

        if (mapsList[0].Equals("_Map2")&& mapsList[1].Equals("_Map3"))
        {
            path1 = true;
        }
        else if (mapsList[1].Equals("_Map2")&& mapsList[0].Equals("_Map3"))
        {
            path1 = true;
        }
        else if (mapsList[0].Equals("_Map2")&& mapsList[1].Equals("_Map4"))
        {
            path2 = true;
        }
        else if (mapsList[1].Equals("_Map2")&& mapsList[0].Equals("_Map4"))
        {
            path2 = true;
        }
        else if (mapsList[0].Equals("_Map3")&& mapsList[1].Equals("_Map4"))
        {
            path3 = true;
        }
        else if (mapsList[1].Equals("_Map3")&& mapsList[0].Equals("_Map4"))
        {
            path3 = true;
        }

        
        if (path2)
        {
            GameObject.Find("_SpecialBoard2").SetActive(false);
        }
        else if (path3)
        {
            GameObject.Find("_SpecialBoard3").SetActive(false);
        }
        //Cannot use GameObject.Find() to return the GameObject that is inactive, therefore, the GameObjects below must be all active first
        if (dBox == null || continueBotton == null || hunter == null || okButton == null || cancelButton == null || yesButton ==null || noButton == null) 
        {
            dBox = GameObject.Find("DialogBox");
            continueBotton = GameObject.Find("Continue");
            okButton = GameObject.Find("Ok");
            cancelButton = GameObject.Find("Cancel");
            yesButton = GameObject.Find("Yes");
            noButton = GameObject.Find("No");
            hunter = GameObject.Find("_Hunter");
        }
        dBox.SetActive(true);
        continueBotton.SetActive(true);
        okButton.SetActive(false);
        cancelButton.SetActive(false);
        yesButton.SetActive(false);
        noButton.SetActive(false);

        winned = false;
        lost = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (hunter.GetComponent<Hunter>().meetItem == true)
        {
            dBox.SetActive(true);
            continueBotton.SetActive(false);
            okButton.SetActive(true);
            cancelButton.SetActive(true);
            PauseGame();
        }
        else if (hunter.GetComponent<Hunter>().lostChance == true)
        {
            dBox.SetActive(true);
            cancelButton.SetActive(true);
            if (hunter.GetComponent<Hunter>().GetGameChances() <= 0)
            {
                okButton.SetActive(true);
                lost = true;
            }
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
        hunter.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f);
    }

}
