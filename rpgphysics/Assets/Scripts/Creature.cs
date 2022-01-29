using UnityEngine;
using System.Collections;

public class Creature : Character
{
    private void OnEnable()
    {
        ResetCharacter();
    }
    public override void ResetCharacter()
    {
        hitPoints = startingHitPoints;
    }
    public override IEnumerator DamageCharacter(int damage, float interval)
    {
        while (true)
        {
            StartCoroutine(FlickerCharacter());
            hitPoints = hitPoints - damage;
            if (hitPoints <= float.Epsilon)
            {
                KillCharacter();
                break;
            }
            if (interval > float.Epsilon)
            {
                yield return new WaitForSeconds(interval);
            }
            else
            {
                break;
            }
        }
    }
    float hitPoints;
}
