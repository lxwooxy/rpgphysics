using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Weapon : MonoBehaviour
{
    bool isFiring;
    [HideInInspector]
    public Animator animator;
    Camera localCamera;
    float positiveSlope;
    float negativeSlope;
    //Describe the direction the player is firing in
    enum Quadrant
    {
        East,
        South,
        West,
        North
    }
    public GameObject ammoPrefab;
    static List<GameObject> ammoPool;
    public int poolSize;
    public float weaponVelocity;
    void Awake()
    {
        if (ammoPool == null)
        {
            ammoPool = new List<GameObject>();
        }
        for (int i = 0; i < poolSize; i++)
        {
            GameObject ammoObject = Instantiate(ammoPrefab);
            ammoObject.SetActive(false);
            ammoPool.Add(ammoObject);
        }
    }
    public GameObject SpawnAmmo(Vector3 location)
    {
        foreach (GameObject ammo in ammoPool)
        {
            if (ammo.activeSelf == false)
            {
                ammo.SetActive(true);
                ammo.transform.position = location;
                return ammo;
            }
        }
        return null;
    }
    float GetSlope(Vector2 pointOne, Vector2 pointTwo)
    {
        return(pointTwo.y - pointOne.y) /  (pointTwo.x -  pointOne.x);
    }
    bool HigherThanPositiveSlopeLine(Vector2 inputPosition)
    {
        //Save a reference to the current transform.position for clarity. This script is attached to the Player object, so this will be the Players position.
        Vector2 playerPosition = gameObject.transform.position;
        //Convert the inputPosition, which is the mouse position, to WorldSpace and save a reference.
        Vector2 mousePosition = localCamera.ScreenToWorldPoint(inputPosition);
        //Rearrange y = mx + b a bit to solve for b. This will make it easy to compare the y-intercept of each line. The form on this line is: b = y – mx.
        float yIntercept = mousePosition.y - (positiveSlope * playerPosition.x);
        //Using the rearranged form: b = y – mx, find the y-intercept for the positive sloped line created by the inputPosition (the mouse).
        float inputIntercept = mousePosition.y - (positiveSlope *mousePosition.x);
        //Compare the y-intercept of the mouse-click to the y-intercept of the line running through the player and return if the mouse-click was higher.
        return inputIntercept > yIntercept;
    }
    bool HigherThanNegativeSlopeLine(Vector2 inputPosition)
    {
        Vector2 playerPosition = gameObject.transform.position;
        Vector2 mousePosition = localCamera.ScreenToWorldPoint(inputPosition);
        float yIntercept =  playerPosition.y - (negativeSlope * playerPosition.x);
        float inputIntercept = mousePosition.y - (negativeSlope * mousePosition.x);
        return inputIntercept > yIntercept;
    }
    //Return a Quadrant describing where the user clicked.
    Quadrant GetQuadrant()
    {
        //Check if the user clicked above (higher than) the positive sloped and negative sloped lines.
        bool higherThanPositiveSlopeLine = HigherThanPositiveSlopeLine(Input.mousePosition);
        bool higherThanNegativeSlopeLine = HigherThanNegativeSlopeLine(Input.mousePosition);

        //If the user’s click is not higher than the positive sloped line,
        //but is higher than the negative sloped line, 
        //the user clicked in the east quadrant.
        if (!higherThanPositiveSlopeLine && higherThanNegativeSlopeLine)
        {
            return Quadrant.East;
        }
        else if (!higherThanPositiveSlopeLine && !higherThanNegativeSlopeLine)
        {
            return Quadrant.South;
        }
        else if (higherThanPositiveSlopeLine && !higherThanNegativeSlopeLine)
        {
            return Quadrant.West;
        }
        else
        {
            return Quadrant.North;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isFiring = false;
        localCamera = Camera.main;
        Vector2 lowerLeft = localCamera.ScreenToWorldPoint(new Vector2(0,0));
        Vector2 upperRight = localCamera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        Vector2 upperLeft =  localCamera.ScreenToWorldPoint(new Vector2(0, Screen.height));
        Vector2 lowerRight = localCamera.ScreenToWorldPoint(new Vector2(Screen.width, 0));
        positiveSlope = GetSlope(lowerLeft, upperRight);
        negativeSlope = GetSlope(upperLeft, lowerRight);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isFiring = true;
            FireAmmo();
        }
        UpdateState();
    }
    void UpdateState()
    {
        //we check if the user clicked the mouse button. 
        //If so, the isFiring variable is set equal to true.
        if(isFiring)
        {
            //Create a Vector2 to save the values that we’ll pass to the Blend Tree.
            Vector2 quadrantVector;
            //Call GetQuadrant() to determine which quadrant the user clicked in and assign the result to quadEnum.
            Quadrant quadEnum = GetQuadrant();
            //Switch on the quadrant (quadEnum).
            switch (quadEnum)
            {
                //If the quadEnum is East, assign the quadrantVector the values (1, 0) in anew Vector2.
                case Quadrant.East:
                quadrantVector = new Vector2(1.0f, 0.0f);
                break;
                case Quadrant.South:
                quadrantVector = new Vector2 (0.0f, -1.0f);
                break;
                case Quadrant.West:
                quadrantVector = new Vector2(-1.0f, 0.0f);
                break;
                case Quadrant.North:
                quadrantVector = new Vector2(0.0f, 1.0f);
                break;
                default:
                quadrantVector = new Vector2(0.0f, 0.0f);
                break;
            }
            //Set the isFiring parameter inside the animator to true, so it transitions to the Fire Blend Tree.
            animator.SetBool("isFiring", true);
            //Set the fireXDir and fireYDir variables in the animator, to the corresponding value for the quadrant the user clicked in. These variables will be picked up by the Fire Blend Tree.
            animator.SetFloat("fireXDir", quadrantVector.x);
            animator.SetFloat("fireYDir", quadrantVector.y);
            //The animation will play all the way through before stopping, because we set Exit Time in the transition to 1.
            isFiring = false;
        }
        else //If isFiring is false, set the isFiring parameter inside the animator to false as well.
        {
            animator.SetBool("isFiring", false);
        }
    }
    void FireAmmo()
    {
        //Mouse uses Screen space, converts it to WorldSpace.
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
       //Retrieve activated Ammo Object from the pool, pass the weapons transform as the starting point.
        GameObject ammo = SpawnAmmo(transform.position);
        //Check that SpawnAmmo returned an AmmoObject.
        if (ammo != null)
        {
            //retrieve a reference to the Arc component of the Ammo Object and save it to the variable arcScript.
            Arc arcScript = ammo.GetComponent<Arc>();
            //dividing 1 by weapon velocity results in the fraction used as travel duration
            //eg. 1/2=0.5, so ammo takes half a second to travel across the screen.
            //Therefor Velocity speeds up when it is futher away.
            float travelDuration = 1.0f / weaponVelocity;
            StartCoroutine(arcScript.TravelArc(mousePosition,travelDuration));
        }
    }
    void OnDestroy()
    {
        ammoPool = null;
    }
}
