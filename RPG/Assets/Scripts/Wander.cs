using System.Collections;
using UnityEngine;
//Must have these components, will add if they dont exist
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Animator))]
public class Wander : MonoBehaviour
{
    //Variables
    public float pursuitSpeed;
    public float wanderSpeed;
    float currentSpeed;
    public float directionChangeInterval;
    public bool followPlayer;
    CircleCollider2D circleCollider;
    //Responsible for moving the Enemy a little bit each frame towards destination.
    Coroutine moveCoroutine;
    Rigidbody2D rb2d;
    Animator animator;
    //We will retrieve the Player transform and assign it to targetTransform.
    Transform targetTransform = null;
    Vector3 endPosition;
    //Add a new angle when choosing a new direction, will generate a vector.
    float currentAngle = 0;
    public IEnumerator WanderRoutine()
    {
        //we want the enemy to wander in a loop
        while(true)
        {
            ChooseNewEndpoint();
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }
            //Start the Move()Coroutine and save a reference to it in moveCoroutine.
            moveCoroutine = StartCoroutine(Move(rb2d,currentSpeed));
            yield return new WaitForSeconds(directionChangeInterval);
        }
    }
    //Methods
    //Omit the access modifier because its only needed in the Wander class.
    void ChooseNewEndpoint()
    {
        currentAngle += Random.Range(0,360);
        //Loop the value:currentAngle so it's never smaller than 0 and bigger than 360.
        //Effectively keeping the new angle in range of 0 to 360, then replacing currentAngle with the result.
        currentAngle = Mathf.Repeat(currentAngle, 360);
        //Convert an Angle to a Vector  3 and add result to endPosition
        endPosition += Vector3FromAngle(currentAngle);
    }
    Vector3 Vector3FromAngle(float inputAngleDegrees)
    {
        //convert the input angle from degrees to radians by multiplying  by the deg-rad conversion constant.
        float inputAngleRadians =  inputAngleDegrees * Mathf.Deg2Rad;
        //Use the input angle in Radians to create a normalized directional vector for enemy direction.
        return new Vector3(Mathf.Cos(inputAngleRadians),Mathf.Sin(inputAngleRadians), 0);
    }
    public IEnumerator Move(Rigidbody2D rigidBodyToMove, float speed)
    {
        //The equation results in a Vector3.
        //the property:sqrMagnitude retrieves the rough distance between the enemy and the destination.
        float remainingDistance = (transform.position - endPosition).sqrMagnitude;
        //Check distance is greater than zero
        while (remainingDistance > float.Epsilon)
        {
            //When in pursuit, target Transform = Player transform instead of null.
            if (targetTransform != null)
            {
                //Override the original endPOsition anduse targetTransform (player) instead.
                endPosition = targetTransform.position;
            }
            //Checking that we have the required RigidBody2D to move.
            if (rigidBodyToMove != null)
            {
                //Set the Animation parameter bool:isWalking to true.
                animator.SetBool("isWalking", true);
                //Vector3.MoveTowards calculates movement of a RigidBody2d, doesnt actually move.
                //The variable speed will change in the pursuit code.
                Vector3 newPosition = Vector3.MoveTowards(rigidBodyToMove.position,endPosition,speed * Time.deltaTime);
                rb2d.MovePosition(newPosition);
                remainingDistance = (transform.position - endPosition).sqrMagnitude;
            }
            yield return new WaitForFixedUpdate();
        }
        animator.SetBool("isWalking", false);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && followPlayer)
        {
            currentSpeed = pursuitSpeed;
            targetTransform = collision.gameObject.transform;
            if(moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }
            moveCoroutine = StartCoroutine(Move(rb2d,currentSpeed));
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.SetBool("isWalking", false);
            currentSpeed = wanderSpeed;
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }
            targetTransform = null;
        }
    }
    void OnDrawGizmos()
    {
        if (circleCollider != null)
        {
            Gizmos.DrawWireSphere(transform.position,circleCollider.radius);
        }
    }
    void Start()
    {
        animator = GetComponent<Animator>();
        currentSpeed = wanderSpeed;
        rb2d = GetComponent<Rigidbody2D>();
        StartCoroutine(WanderRoutine());
        circleCollider = GetComponent<CircleCollider2D>();
    }
    void Update()
    {
        Debug.DrawLine(rb2d.position, endPosition, Color.red);
    }
}
