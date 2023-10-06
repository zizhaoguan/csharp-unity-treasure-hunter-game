using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject target;

    public float deadZone = 1;

    public bool followVertical = true;
    public bool followHorizontal = true;
    public float minHeight = -4;

    public Vector3 targetPos;
    public float smooth = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        // can place the _Hunter GameObject in the inspector instead of using GameObject.Find("_Hunter")
        if (target == null){ target = GameObject.Find("_Hunter"); }
        targetPos = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target != null)
        {
            if (followHorizontal)
            {
               
                if (transform.position.x >= target.transform.position.x + deadZone)
                {
                    targetPos = new Vector3(target.transform.position.x + deadZone, target.transform.position.y, transform.position.z);
                }
                if (transform.position.x <= target.transform.position.x - deadZone)
                {
                    targetPos = new Vector3(target.transform.position.x - deadZone, target.transform.position.y, transform.position.z);
                }
            }
            if (followVertical)
            {
               
                if (transform.position.y >= target.transform.position.y + deadZone)
                {
                    targetPos = new Vector3(target.transform.position.x, target.transform.position.y + deadZone, transform.position.z);
                }
                if (transform.position.y <= target.transform.position.y - deadZone)
                {
                    targetPos = new Vector3(target.transform.position.x, target.transform.position.y - deadZone, transform.position.z);
                }
            }
            if (target.transform.position.y < minHeight)
            {
                
                targetPos = new Vector3(target.transform.position.x, minHeight, transform.position.z);
            }
            transform.position = Vector3.Lerp(transform.position, targetPos, smooth);
            
        }
    }
}
