using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMoving : MonoBehaviour
{
    Vector3 pos;
    float speedX;
    // Start is called before the first frame update
    void Start()
    {
       transform.position = new Vector3(0.1f, -1f, 0f);
        speedX = -20f;
    }

    // Update is called once per frame
    void Update()
    {
        pos = transform.position;
        pos.x += speedX * Time.deltaTime;
        transform.position = pos;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null)
        {
            Destroy(this.gameObject);
        }
    }
}
