using System.Collections;
using UnityEngine;

public class Arc : MonoBehaviour
{
    public IEnumerator TravelArc(Vector3 destination, float duration)
    {
        var startPosition = transform.position;
        var percentComplete = 0.0f;
        while (percentComplete < 1.0f)
        {
            percentComplete += Time.deltaTime / duration;
            //"period" of a wave is 1 cycle = 2pi. half period = pi
            //Passing the result of (percent complete times mathf.pi):
            //Travelling pi duration down the sine curve every duration second
            //the trsult is assigned to current height.
            var currentHeight = Mathf.Sin(Mathf.PI * percentComplete);
            //Adding Vector3.up * currentHeight to the result of Vector3.Lerp() makes the ammo object move up then down on the Y axis towards the end position.
            transform.position = Vector3.Lerp(startPosition, destination, percentComplete) + Vector3.up * currentHeight;
            yield return null;
        }
        gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
