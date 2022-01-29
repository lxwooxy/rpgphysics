using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float movementSpeed = 3.0f;
    Vector2 movement = new Vector2();

    Animator animator;

    Rigidbody2D rb2D;

    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateState();
    }

    void FixedUpdate()
    {
        MoveCharacter();
    }
    private void MoveCharacter()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        movement.Normalize();

        rb2D.velocity = movement * movementSpeed;
    }
        void UpdateState()
        {
            //Check if the movement vector is approximately equal to 0, indicating the player is standing still.
            if (Mathf.Approximately(movement.x,0) && Mathf.Approximately(movement.y, 0))
            {
                //Because the player is standing still, set isWalking to false.
                animator.SetBool("isWalking", false);
            }
            else //Otherwise movement.x, movement.y, or both, are non-zero numbers, which means the player is moving.
            {
                animator.SetBool("isWalking",true);
            }
            //Update the animator with the new movement values.
            animator.SetFloat("xDir",movement.x);
            animator.SetFloat("yDir",movement.y);
        }
}
