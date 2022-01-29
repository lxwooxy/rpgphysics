using System.Collections;
using UnityEngine;
public class BirdWander : MonoBehaviour
{
    public float moveSpeed;
    private Vector2 minWalkPoint;
    private Vector2 maxWalkPoint;
    private Rigidbody2D birdRigidbody;
    public bool isWalking;
    public float walkTime;
    public float waitTime;
    private float walkCounter;
    private float waitCounter;
    private int WalkDirection;
    private bool hasWalkZone;
    public Collider2D walkZone;
    private float axisChangeTime;
    private bool verticalGoesFirst;
    
    void Start()
    {
        birdRigidbody = GetComponent<Rigidbody2D>();
        waitCounter = waitTime;
        walkCounter = walkTime;

        ChooseDirection();
        if(walkZone != null)
        {
            minWalkPoint = walkZone.bounds.min;
            maxWalkPoint = walkZone.bounds.max;
            hasWalkZone = true;
        }
    }
    private void Update()
    {
        if (isWalking)
        {
            walkCounter -= Time.deltaTime;

            if(walkCounter <= 0)
            {
                isWalking = false;
                waitCounter = waitTime;
            }
            switch (WalkDirection)
            {
                case 0:
                    birdRigidbody.velocity = new Vector2 (0,moveSpeed);
                    if(hasWalkZone && transform.position.y > maxWalkPoint.y)
                    {
                        isWalking = false;
                        waitCounter = waitTime;
                    }
                    break;

                case 1:
                    birdRigidbody.velocity = new Vector2 (moveSpeed,0);
                    if(hasWalkZone && transform.position.x > maxWalkPoint.x)
                    {
                        isWalking = false;
                        waitCounter = waitTime;
                    }
                    break;
                    
                case 2:
                    birdRigidbody.velocity = new Vector2 (0,-moveSpeed);
                    if(hasWalkZone && transform.position.y < minWalkPoint.y)
                    {
                        isWalking = false;
                        waitCounter = waitTime;
                    }
                    break;

                case 3:
                    birdRigidbody.velocity = new Vector2 (-moveSpeed,0);
                    if(hasWalkZone && transform.position.x < minWalkPoint.x)
                    {
                        isWalking = false;
                        waitCounter = waitTime;
                    }
                    break;
            }
        }
        else
        {
            waitCounter -= Time.deltaTime;
            birdRigidbody.velocity = Vector2.zero;
            if(waitCounter < 0)
            {
                ChooseDirection();
            }
        }
          /*if (moving)
          {
              walkCounter -= Time.deltaTime;
              axisChangeTime -= Time.deltaTime;
             anim.SetBool("isWalking", true);
             anim.SetFloat("xMov", moveDirection.normalized.x);
             anim.SetFloat("yMov", moveDirection.normalized.y);
              if (verticalGoesFirst)
              {
                  myRigidbody.velocity = moveDirection.y;
              }
              else
              {
                  myRigidbody.velocity = moveDirection.x;
              }
 
              if (axisChangeTime <= 0f)
              {
                 verticalGoesFirst = !verticalGoesFirst;
              }
  
              if (walkCounter < 0f)
              {
                  moving = false;
                  anim.SetBool("isWalking", false);
                  waitCounter = waitMove;
              }
          }
          else
          {
              Counter -= Time.deltaTime;
              myRigidbody.velocity = Vector2.zero;
  
              if (timeBetweenMoveCounter < 0f)
              {
                  moving = true;
                  timeToMoveCounter = timeToMove;
  
                  moveDirection = new Vector3(Random.Range(-1f, 1f) * moveSpeed, Random.Range(-1f, 1f) * moveSpeed, 0f);
                  verticalGoesFirst = Random.value >= 0.5f;
                  if (verticalGoesFirst)
                  {
                     axisChangeTime = (moveDirection.y / (moveDirection.x + moveDirection.y)) * timeToMove;
                  }
                  else
                  {
                     axisChangeTime = (moveDirection.x / (moveDirection.x + moveDirection.y)) * timeToMove;
                  }
              }
          }*/
    }
    public void ChooseDirection()
    {
        WalkDirection = Random.Range(0, 4);
        isWalking = true;
        walkCounter = walkTime;
    }
}
