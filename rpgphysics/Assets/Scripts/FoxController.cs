using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxController : MonoBehaviour
{
    private Rigidbody2D foxRigidbody;
    Transform spawnTransform;
    Transform currentTransform;
    public float pauseTime;
    public float runSpeed;
    float moveSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        foxRigidbody = GetComponent<Rigidbody2D>();
        spawnTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        currentTransform = GetComponent<Transform>();
        spawnCheck();
    }

    public IEnumerator spawnCheck()
    {
        if (currentTransform != spawnTransform)
        {
            
            yield return new WaitForSeconds(pauseTime);
            moveSpeed = runSpeed;
            currentTransform = spawnTransform;
        }
    }
}
