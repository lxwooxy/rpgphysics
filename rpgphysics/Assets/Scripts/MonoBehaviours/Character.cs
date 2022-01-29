using UnityEngine;
using System.Collections;

public abstract class Character : MonoBehaviour
{
    //Variables
    public float startingHitPoints;
    public float maxHitPoints;
    public enum CharacterCategory
    {
        PLAYER,
        ENEMY,
        CREATURE
    }
    public CharacterCategory characterCategory;

    //Methods
    public virtual void KillCharacter()
    {
        Destroy(gameObject);
    }
    //Set the character back to its original starting state
    public abstract void ResetCharacter();
    //Called by other Characters to damage current character.
    //Takes an amount to damage the character by, and a time interval for when damage is recurring.
    //IEnumerator is required in a Coroutine, and is part of Systems.Collections
    //Acstract methods must be implemented before the code compiles and runs, so we have to implement both methods to both classes.
    public abstract IEnumerator DamageCharacter(int damage, float interval);
    public virtual IEnumerator FlickerCharacter()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        //Yield excecution for 0.1 seconds.
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = Color.white;
;    }
}