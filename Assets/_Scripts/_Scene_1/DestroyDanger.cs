using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyDanger : MonoBehaviour
{
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Hunter")
        {
            Destroy(this.gameObject);
            GameObject mirror = GameObject.Find("Mirror");
            mirror.GetComponent<DiscoverBox>().mirrorReady = true;

        }
        else if (collision.gameObject.tag == "Ground")
        {
            Destroy(this.gameObject);
            GameObject mirror = GameObject.Find("Mirror");
            mirror.GetComponent<DiscoverBox>().mirrorReady = true;
        }
    }
}
