using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNewArea : MonoBehaviour
{
    public GameObject landingTarget;
    public string levelToLoad;
    public float movespeed;
    private Vector3 landingPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(levelToLoad);
            landingPos = new Vector3(landingTarget.transform.position.x, landingTarget.transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, landingPos, movespeed * Time.deltaTime);
        }
    }
}
