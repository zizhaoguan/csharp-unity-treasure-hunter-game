using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public bool trap = false;

    public bool pressed = false;
    // Start is called before the first frame update
    
    //to set if the trap is real and dangerous, or safe 
    public void setTrap(bool trap)
    {
        this.trap = trap;
    }

    public void setPressed(bool pressed)
    {
       
        this.pressed = pressed;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Hunter") { pressed = true; }
    }
    public bool checkTrap()
    {
        return trap;
    }
    public bool checkPressed()
    {
        return pressed;
    }
}
