using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap2 : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject button1;
    public GameObject button2;
    public GameObject dangerMat;
    public GameObject damagedStep;
    public GameObject hiddenMap;
    private int numOfHiddenMap = 0;
    void Start()
    {
        if ( ((int)Random.Range(0,100)) % 2 == 0)
        {
            button1.GetComponent<Button>().setTrap(true);
            button2.GetComponent<Button>().setTrap(false);
        }
        else
        {
            button1.GetComponent<Button>().setTrap(false);
            button2.GetComponent<Button>().setTrap(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (button1.GetComponent<Button>().checkTrap() && button1.GetComponent<Button>().checkPressed())
        {
            dangerMat.GetComponent<Rigidbody>().useGravity = true;
            button1.GetComponent<Button>().setPressed(false);
            button2.GetComponent<Button>().setPressed(false);

        }
        else if (button2.GetComponent<Button>().checkTrap() && button2.GetComponent<Button>().checkPressed())
        {
            dangerMat.GetComponent<Rigidbody>().useGravity = true;
            button2.GetComponent<Button>().setPressed(false);
            button2.GetComponent<Button>().setPressed(false);
        }
        else if (!button1.GetComponent<Button>().checkTrap() && button1.GetComponent<Button>().checkPressed())
        {
            Destroy(damagedStep.gameObject);
            if (numOfHiddenMap == 0)
            {
                hiddenMap = Instantiate<GameObject>(hiddenMap);
                hiddenMap.transform.position = new Vector3(-4.28f, -0.69f, 0);
                numOfHiddenMap++;
            }
        }
        else if (!button2.GetComponent<Button>().checkTrap() && button2.GetComponent<Button>().checkPressed())
        {
            Destroy(damagedStep.gameObject);
            if (numOfHiddenMap == 0)
            {
                hiddenMap = Instantiate<GameObject>(hiddenMap);
                hiddenMap.transform.position = new Vector3(-4.28f, -0.69f, 0);
                numOfHiddenMap++;
            }

        }

    }
 
}
