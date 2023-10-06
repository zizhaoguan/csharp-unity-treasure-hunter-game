using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Hunter : MonoBehaviour
{
    static public Hunter hunter;

    [Header("Set in Inspector")]
    private Rigidbody rb2d;
    private GameObject map;
    private Animator animator;
   
    private Vector3 originalPos;
    private Vector3 groundPos;
   

    public float speed = 3.5f;
    public float maximumSpeed = 0.5f;
    public float climbSpeed = 4f;
    public float jumpSpeed = 220;
    public float friction = 27.5f;
    public float goDownSpeed = 2f;
    public float originalJumpSpeed;

    public bool secondLevel = false;
    public bool grounded = false;
    public bool catchRope = false;
    public bool meetItem = false;
    public bool foundRealMap = false;
    public bool lostChance = false;
    public bool movingGroundUp = false;
    public bool movingGroundDown = false;


    public bool liar;

    public int gameChances = 5;
    public List<GameObject> mapsList;
    public List<GameObject> trashList;

    public float leftAndRightEdge = 6.4f;
    // Start is called before the first frame update
    void Start()
    {
        animator = hunter.GetComponent<Animator>();
        
}

    void Awake()
    {
        if (hunter == null)
        {
            hunter = this;
            originalPos = hunter.transform.position;
        }
        else { Debug.LogError("Attempted to create second Hunter instance");  }

        rb2d = GetComponent<Rigidbody>();

        if (secondLevel)
        {
            Physics.gravity = new Vector3(0, -30, 0);
        }
        else if (!secondLevel) {
            Physics.gravity = new Vector3(0, -10, 0);
        }

    }
    // Update is called once per frame
    private void Update()
    {
        GameObject gameChancesTextObject = GameObject.Find("GameChances");
        Text gameChancesText = gameChancesTextObject.GetComponent<Text>();
        gameChancesText.text = "Game Chances: " + gameChances.ToString();
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        if (movingGroundDown)
        {
            if (grounded)
            {
                rb2d.velocity += new Vector3(0f, -goDownSpeed, 0f);
                jumpSpeed = 500;
            }
        }
        else
        {
            jumpSpeed = originalJumpSpeed;
        }
    }
    void FixedUpdate()
    {

        float xAxis = Input.GetAxis("Horizontal");
        Vector3 pos = transform.position;
        if (xAxis < 0)
        {
            pos.x += xAxis * speed * Time.deltaTime;
            transform.position = pos;
            animator.SetBool("walkLeft", true);
            animator.SetBool("walkRight", false);

            animator.SetBool("faceLeft", true);
            animator.SetBool("faceRight", false);
        }
        else if (xAxis > 0)
        {
            pos.x += xAxis * speed * Time.deltaTime;
            transform.position = pos;
            animator.SetBool("walkLeft", false);
            animator.SetBool("walkRight", true);

            animator.SetBool("faceLeft", false);
            animator.SetBool("faceRight", true);
        }
        else
        {
            animator.SetBool("walkLeft", false);
            animator.SetBool("walkRight", false);
        }
    }
    private void Jump()
    {
        if (this.grounded)
        {
            rb2d.AddForce(new Vector3(0f, jumpSpeed));
            
            grounded = false;
        }
        
    }
    private void Climb()
    {
        float yAxis = Input.GetAxis("Vertical");
        float xAxis = Input.GetAxis("Horizontal");

        Vector3 pos = transform.position;
        if (pos.x > -leftAndRightEdge && xAxis < 0)
        {
            pos.x += xAxis * climbSpeed * Time.deltaTime;
            pos.y += yAxis * climbSpeed * Time.deltaTime;
            transform.position = pos;
        }
        else if (pos.x < leftAndRightEdge && xAxis > 0)
        {
            pos.x += xAxis * climbSpeed * Time.deltaTime;
            pos.y += yAxis * climbSpeed * Time.deltaTime;
            transform.position = pos;
        }
        transform.position = pos;
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Rope")
        {
            print("hit");
            catchRope = true;
            grounded = true;
            movingGroundUp = false;
            movingGroundDown = false;
        }
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "MovingGround")
        {
            catchRope = false;
        }
        if (collision.gameObject.tag == "Real Map" || collision.gameObject.tag == "Fake Map")
        {
            map = collision.gameObject;
            if (!mapsList.Contains(map) && !trashList.Contains(map)) { meetItem = true; }
        }
        if (collision.gameObject.tag == "Slabstone")
        {
            if (gameChances > 0)
            {
                gameChances--;
                hunter.transform.position = originalPos;

                collision.gameObject.GetComponent<Danger>().SetBackOriginalPos();
                lostChance = true;
                hunter.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f);
                animator.SetBool("walkLeft", false);
                animator.SetBool("walkRight", false);
            }
        }
        if (collision.gameObject.tag == "Water" || collision.gameObject.tag == "Splinter")
        {
            if (gameChances > 0)
            {
                print("hit........");
                gameChances--;
                hunter.transform.position = originalPos;

                lostChance = true;
                hunter.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f);
                animator.SetBool("walkLeft", false);
                animator.SetBool("walkRight", false);
            }
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Rope")
        {
            Climb();
        }

    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Rope") { catchRope = false; }
        if (collision.gameObject.tag == "Real Map" || collision.gameObject.tag == "Fake Map") { meetItem = false; }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ground" || other.gameObject.tag == "Real Map" || other.gameObject.tag == "Fake Map")
        {
            grounded = true;
            catchRope = false;
            movingGroundUp = false;
            movingGroundDown = false;
        }
        if (other.gameObject.tag == "Moving Ground")
        {
            
            grounded = true;
            catchRope = false;
            if (other.GetComponent<MovingGround>().speedX > 0)
            {
                
                friction = Math.Abs(friction);
                rb2d.AddForce(new Vector3(friction, 0f));
                if (hunter.GetComponent<Rigidbody>().velocity.x >= maximumSpeed)
                {
                    hunter.GetComponent<Rigidbody>().velocity = new Vector3(maximumSpeed, hunter.GetComponent<Rigidbody>().velocity.y);
                }
            }
            else if (other.GetComponent<MovingGround>().speedX < 0)
            {
                friction = -Math.Abs(friction);
                rb2d.AddForce(new Vector3(friction, 0f));
                if (hunter.GetComponent<Rigidbody>().velocity.x <= -maximumSpeed)
                {
                    hunter.GetComponent<Rigidbody>().velocity = new Vector3(-maximumSpeed, hunter.GetComponent<Rigidbody>().velocity.y);
                }
            }

            if (other.GetComponent<MovingGround>().speedY < 0)
            {
                movingGroundUp = false;
                movingGroundDown = true;
                
            }
            else if (other.GetComponent<MovingGround>().speedY < 0)
            {
                movingGroundUp = true;
                movingGroundDown = false;
            }
        }   
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Ground" || other.gameObject.tag == "Real Map" || other.gameObject.tag == "Fake Map")
        { 
            grounded = true;
            movingGroundUp = false;
            movingGroundDown = false;
        }
        if (other.gameObject.tag == "Moving Ground")
        {
            grounded = true;
            
            if (other.GetComponent<MovingGround>().speedX > 0)
            {
                friction = Math.Abs(friction);
                rb2d.AddForce(new Vector3(friction, 0f));
                if (hunter.GetComponent<Rigidbody>().velocity.x >= maximumSpeed)
                {
                    hunter.GetComponent<Rigidbody>().velocity = new Vector3(maximumSpeed, hunter.GetComponent<Rigidbody>().velocity.y);
                }
            }
            else if (other.GetComponent<MovingGround>().speedX < 0)
            {
                friction = -Math.Abs(friction);
                rb2d.AddForce(new Vector3(friction, 0f));
                if (hunter.GetComponent<Rigidbody>().velocity.x <= -maximumSpeed)
                {
                    hunter.GetComponent<Rigidbody>().velocity = new Vector3(-maximumSpeed, hunter.GetComponent<Rigidbody>().velocity.y);
                }
            }
            if (other.GetComponent<MovingGround>().speedY < 0)
            {
                movingGroundUp = false;
                movingGroundDown = true;
            }
            else if (other.GetComponent<MovingGround>().speedY > 0)
            {
                movingGroundUp = true;
                movingGroundDown = false;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ground" || other.gameObject.tag == "Real Map" || other.gameObject.tag == "Fake Map")
        {
            grounded = false;
        }
        if (other.gameObject.tag == "Moving Ground")
        {
            grounded = false;
           
        }
    }

    public bool CheckMap()
    {
        bool realMap = false;
        if (map.tag == "Real Map")
        {
            if (!mapsList.Contains(map))
            {
                realMap = true;
                mapsList.Add(map);
            }
        }
        else if (map.tag == "Fake Map")
        {
            if (!trashList.Contains(map))
            {
                trashList.Add(map);
            }
        }
        meetItem = false;
        return realMap;
    }
    public void setMeetItem(bool meetItem)
    {
        this.meetItem = meetItem;
    }
   

    public int GetMapCount()
    {
        return mapsList.Count;
    }
    public int GetGameChances()
    {
        return gameChances;
    }
    public void setLostChance(bool lostChanceOrNot)
    {
        lostChance = lostChanceOrNot;
    }
}
