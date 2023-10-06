using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTrap : MonoBehaviour
{
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    int randomNum;

    // Start is called before the first frame update
    void Start()
    {
        List<int> list = new List<int>();
        list.Add(0);
        list.Add(1);
        list.Add(2);
        randomNum = (int)(Random.value * 100);
        int trapType = randomNum % 3;
        list.Remove(trapType);

        button1.GetComponent<Botton_1>().setTrapType(trapType);

        int index = randomNum % 2;

        button2.GetComponent<Botton_1>().setTrapType(list[index]);

        list.RemoveAt(index);

        button3.GetComponent<Botton_1>().setTrapType(list[0]);
    }

    
}
