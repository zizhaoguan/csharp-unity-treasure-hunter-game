using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disappear : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject game;
    public GameObject rope;
    void Start()
    {
        game = GameObject.Find("Main Camera");
        rope.SetActive(false);
    }

    void OnMouseDown()
    {
        if (game.GetComponent<TreasureHunter_1>().path1 == true)
        {
            Destroy(this.gameObject);
            rope.SetActive(true);
        }

    }
}
