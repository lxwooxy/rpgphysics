using UnityEngine;

[CreateAssetMenu(menuName = "HitPoints")]
public class HitPoints : ScriptableObject
{
    //Use a float to hold the hit-points.
    //We'll need to assign the float to the Image object property: 
    //Fill Amount, in the Meter object of our health bar.
    public float value;
}
