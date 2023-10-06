using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
    

public class TreasureHunter : MonoBehaviour
{
    
    [Header("Set in Inspector")]

    public bool winned;
    public bool lost;
    public GameObject hunter;
    public GameObject dBox;
    public GameObject continueBotton;
    public GameObject okButton;
    public GameObject cancelButton;
   


    // Start is called before the first frame update
    void Start()
    {
        Global.instance.DoSomeThings();
        //can place the _Hunter GameObject in the inspector to instead of this.
        if (hunter == null)
        {
            hunter = GameObject.Find("_Hunter");
        }
        //Cannot use GameObject.Find() to return the GameObject that is inactive, therefore, the GameObjects below must be all active first
        if (dBox == null || continueBotton == null || hunter == null || okButton == null || cancelButton == null)
        {
            dBox = GameObject.Find("DialogBox");
            continueBotton = GameObject.Find("Continue");
            okButton = GameObject.Find("Ok");
            cancelButton = GameObject.Find("Cancel");
            hunter = GameObject.Find("_Hunter");
        }
        dBox.SetActive(true);
        continueBotton.SetActive(true);
        okButton.SetActive(false);
        cancelButton.SetActive(false);
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
        else if (hunter.GetComponent<Hunter>().mapsList.Count >= 2)
        {
            dBox.SetActive(true);
            okButton.SetActive(true);
            cancelButton.SetActive(true);
            winned = true;
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
