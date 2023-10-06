using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSplinter : MonoBehaviour
{

    [Header("Set in Inspector")]
    public float speedY = 3f;
    public float topEdge;
    public float buttomEdge;

    public Vector3 pos;

    // Update is called once per frame
    void FixedUpdate()
    {
        pos = transform.position;
        if (pos.y > topEdge) { speedY = -Mathf.Abs(speedY); }
        else if (pos.y < buttomEdge) { speedY = Mathf.Abs(speedY); }
        pos.y += speedY * Time.deltaTime;
        transform.position = pos;
    }
}
