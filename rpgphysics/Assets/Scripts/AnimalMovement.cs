using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
 
 public class AnimalMovement : MonoBehaviour 
 {
 
    private Animator anim;
    public float moveSpeed;
    private Rigidbody2D myRigidbody;
    private bool moving;
    public float timeBetweenMove;
    private float timeBetweenMoveCounter;
    public float timeToMove;
    private float timeToMoveCounter;
    private Vector3 moveDirection;
 
     void Start () 
     {
        anim = GetComponent<Animator> ();
        myRigidbody = GetComponent<Rigidbody2D> ();
        timeBetweenMoveCounter = timeBetweenMove;
        timeToMoveCounter = timeToMove;
     }
   
    void Update()
    {
        if (moving)
        {
            timeToMoveCounter -= Time.deltaTime;
            myRigidbody.velocity = moveDirection;
 
            anim.SetBool("isWalking", true);
            anim.SetFloat("xDir", moveDirection.normalized.x);
            anim.SetFloat("yDir", moveDirection.normalized.y);
 
            if (timeToMoveCounter < 0f)
            {
                moving = false;
                anim.SetBool("isWalking", false);
                timeBetweenMoveCounter = timeBetweenMove;
            }
        }
        else
        {
            timeBetweenMoveCounter -= Time.deltaTime;
            myRigidbody.velocity = Vector2.zero;
 
             if (timeBetweenMoveCounter < 0f)
            {
                 moving = true;
                 timeToMoveCounter = timeToMove;
 
                 if (Random.value >= 0.5f) //random boolean
                 {
                     moveDirection = new Vector3(Random.Range(-1f, 1f) * moveSpeed, 0f, 0f);
                 }
                 else
                 {
                     moveDirection = new Vector3(0, Random.Range(-1f, 1f) * moveSpeed, 0f);
                 }
            }
         }
     }
}