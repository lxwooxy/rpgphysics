using UnityEngine;
using System.Collections;

public class Enemy : Character
{
    //Variables
    public int damageStrength;
    Coroutine damageCoroutine;
    //Methods
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (damageCoroutine == null)
            {
                damageCoroutine = StartCoroutine(player.DamageCharacter(damageStrength,1.0f));
            }
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
         }
    }
    private void OnEnable()
    {
        ResetCharacter();
    }
    public override void ResetCharacter()
    {
        hitPoints = startingHitPoints;
    }
    //We're overriding the KillCharacter method from the parent class.
    public override IEnumerator DamageCharacter(int damage, float interval)
    {
        //Will loop and continue inflicting damage until the character dies
        //or if the interval = 0, it will break and return.
        while (true)
        {
            //Momentarily tint the Character red
            StartCoroutine(FlickerCharacter());
            hitPoints = hitPoints - damage;
            //Check if hitPoints are less than 0.
            //as hitPOints is of type:float, and prone to rounding errors,
            //compare a float value to float.Epsilon ("smallest positive value greater than zero")
            //Therefore if hitpoints are less than float.Episilon, the character has zero health.
            if (hitPoints <= float.Epsilon)
            {
                KillCharacter();
                break;
            }
            if (interval > float.Epsilon)
            {
                //We want to yield execution, wait for intercal seconds, 
                //then resume excecuting the while loop. 
                //In this scenario, the loop will only exit when the character dies.
                yield return new WaitForSeconds(interval);
            }
            else
            {
                //if interval is not greater than float.Epsilon (meaning 0)
                //The while loop will be broken and the method will be returned.
                break;
            }
        }
    }
    float hitPoints;
}
