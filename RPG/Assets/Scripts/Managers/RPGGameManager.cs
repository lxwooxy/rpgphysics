using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGGameManager : MonoBehaviour
{
    //Variables
    public RPGCameraManager cameraManager;
    public SpawnPoint playerSpawnPoint;
    //A static variable, sharedInstance is used to access the Singleton object.
    //the singleton should ONLY be accessed through this property
    //Static variables belong to the class itself (RPGGameManager), not an instance of the class.
    //This means only one copy of RPGGameManager.sharedInstance exists in memory.
    public static RPGGameManager sharedInstance = null;
    void Awake()
    {
        //Check if sharedinstance is already initialized or
        //if its not equal to this current instance.
        if (sharedInstance != null && sharedInstance != this)
        {
            //There must only be one.
            Destroy(gameObject);
        }
        else
        {
            //Assign the sharedinstance variable to the current object.
            sharedInstance = this;
        }
    }

    //Methods
    public void SpawnPlayer()
    {
        if (playerSpawnPoint != null)
        {
            GameObject player = playerSpawnPoint.SpawnObject();
            cameraManager.virtualCamera.Follow = player.transform;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //Consolidate all the logic to set up the scene in a single method.
        SetupScene();
    }

    // Update is called once per frame
    public void SetupScene()
    {
        SpawnPlayer();
    }
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
}
