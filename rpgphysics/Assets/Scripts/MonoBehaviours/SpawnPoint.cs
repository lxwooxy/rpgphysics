using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public float repeatInterval;

    // Start is called before the first frame update
    public void Start()
    {
        if (repeatInterval > 0)
        {
            //Because the repeatInterval is greater than 0, we use
            //InvokeRepeating to spawn the object at regulat, repeated intervals.
            //The method signature takes 3 parameters: 
            //the method to call, 
            //the time to wait before invoking the first time,
            //and the time to wait in between invocations.
            InvokeRepeating("SpawnObject", 0.0f, repeatInterval);
        }
    }
    public GameObject SpawnObject()
    {
        if (prefabToSpawn !=null)
        {
            //Instantiate the prefab at the location of the current SpawnPoint object.
            //This method takes a prefab, Vector3 (location), and a Quaternion(rotation).
            //Quaternion.identity represents "no rotation"
            return Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
        }
        return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
