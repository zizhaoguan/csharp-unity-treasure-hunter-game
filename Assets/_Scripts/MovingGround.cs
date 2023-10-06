using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingGround : MonoBehaviour
{
    // Start is called before the first frame update
    // Start is called before the first frame update
    [Header("Set in Inspector")]
    public float speedX = 5f;
    public float speedY = 3f;
    public float leftEdge;
    public float rightEdge;
    public float topEdge;
    public float buttomEdge;

    public Vector3 pos;
    public bool movingTowardY = false;
    // Start is called before the first frame update

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!movingTowardY)
        {
            pos = transform.position;
            if (pos.x < leftEdge) { speedX = Mathf.Abs(speedX); }
            else if (pos.x > rightEdge) { speedX = -Mathf.Abs(speedX); }
            pos.x += speedX * Time.deltaTime;
            transform.position = pos;
        }
        else if(movingTowardY)
        {
            pos = transform.position;
            if (pos.y < buttomEdge)
            {
                speedX = -Mathf.Abs(speedX);
                speedY = Mathf.Abs(speedY);
            }
            else if (pos.y > topEdge)
            {
                speedX = Mathf.Abs(speedX);
                speedY = -Mathf.Abs(speedY);
            }
            pos.x += speedX * Time.deltaTime;
            pos.y += speedY * Time.deltaTime;

            transform.position = pos;
        }
       



    }
}
