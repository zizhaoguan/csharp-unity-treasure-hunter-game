using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Botton_1 : MonoBehaviour
{
    // Start is called before the first frame update
    public bool pressed;
    public int trapType;
    public GameObject wave;
    public GameObject water;
    public Vector3 pos;
    public float speedY;

    private void Start()
    {
        speedY = 5f;
    }
    private void FixedUpdate()
    {
        if (pressed && trapType == 0)
        {
            pos = transform.position;
           
            if (pos.y > 0) { speedY = -Mathf.Abs(speedY); }
            else if (pos.y < -5.5) { speedY = Mathf.Abs(speedY); }
            pos.y += speedY * Time.deltaTime;
            transform.position = pos;
        }
    }

    //to set if the trap is real and dangerous, or safe 
    public void setTrapType(int type)
    {
        this.trapType = type;
    }

    public void setPressed(bool pressed)
    {

        this.pressed = pressed;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Hunter"  &&  !pressed)
        {
            pressed = true; 
            if (trapType == 1)
            {
                GameObject aWaveComing = Instantiate<GameObject>(wave);
                Destroy(this.gameObject);
            }
            else if (trapType == 2)
            {
                Destroy(water.gameObject);
                Destroy(this.gameObject);
            }
        }
    }


    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Hunter")
        {
            if (trapType == 0)
            {
                Destroy(this.gameObject);
            }
            else if (trapType == 1)
            {
                Destroy(this.gameObject);
            }
            else if (trapType == 2)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
