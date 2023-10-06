using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Global : MonoBehaviour
{
    // Start is called before the first frame update
    public static Global instance;
    public static List<string> mapsList;
    GameObject hunter;
    static Global()
    {
        GameObject go = new GameObject("Global");
        DontDestroyOnLoad(go);
        instance = go.AddComponent<Global>();
        mapsList = new List<string>();
    }

    public void DoSomeThings()
    {
        print("hi");
        Debug.Log("DoSomeThings");
        //SceneManager.UnloadSceneAsync("_Scene_1");

    }
    public void storeMap()
    {
        hunter = GameObject.Find("_Hunter");
        //print(hunter.GetComponent<Hunter>().mapsList[0].GetComponent<Map>().mapName);
        //print(hunter.GetComponent<Hunter>().mapsList[1].GetComponent<Map>().mapName);
        
        mapsList.Add(hunter.GetComponent<Hunter>().mapsList[0].GetComponent<Map>().mapName);
        mapsList.Add(hunter.GetComponent<Hunter>().mapsList[1].GetComponent<Map>().mapName);


    }
    public List<string> getMapList()
    {
        return mapsList;
    }

    public void clearMapsList()
    {
        mapsList.Clear();
    }
    void Start()
    {
        
        print("hi");
        Debug.Log("Start");
    }
}
