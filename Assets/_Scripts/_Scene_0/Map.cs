using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public string mapName;
    // Start is called before the first frame update
    void Start()
    {
        mapName = gameObject.ToString().Substring(0, 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
