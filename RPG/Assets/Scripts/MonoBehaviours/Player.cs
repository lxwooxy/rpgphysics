using UnityEngine;
using System.Collections;

public class Player : Character
{
    //Variables
    public Inventory inventoryPrefab;
    Inventory inventory;
    public HealthBar healthBarPrefab;
    HealthBar healthBar;
    public HitPoints hitPoints;

    //Methods
    public override IEnumerator DamageCharacter(int damage, float interval)
    {
        while (true)
        {
            //Momentarily tint the Character red
            StartCoroutine(FlickerCharacter());
            hitPoints.value =  hitPoints.value - damage;
            if (hitPoints.value <= float.Epsilon)
            {
                KillCharacter();
                break;
            }
            if (interval >float.Epsilon)
            {
                yield return new WaitForSeconds(interval);
            }
            else
            {
                break;
            }
        }
    }
    public override void KillCharacter()
    {
        base.KillCharacter();
        Destroy(healthBar.gameObject);
        Destroy(inventory.gameObject);
    }
    public override void ResetCharacter()
    {
        inventory = Instantiate(inventoryPrefab);
        healthBar = Instantiate(healthBarPrefab);
        healthBar.character = this;
        hitPoints.value = startingHitPoints;
    }
    private void OnEnable() 
    {
	    ResetCharacter();
    }
    public void Start()
	{
       
	}
   void OnTriggerEnter2D(Collider2D collision)
   {
       //Grab a reference to the gobj attached to the collision.
       if (collision.gameObject.CompareTag("CanBePickedUp"))
       {
           //Call GetComponent on the gobj and pass it in the script
           //name, "Consumable" to retrieve the script component.
           //Retrieve the property called item from the component and assign
           //to hitObject.
           Item hitObject = collision.gameObject.GetComponent<Consumable>().item;
           //Check to see if the hitObject is null. 
           //False = Successfully retrived it
           if (hitObject != null)
           {
               bool shouldDisappear = false;
               switch (hitObject.itemType)
               {
                   case Item.ItemType.COIN:
                    shouldDisappear = inventory.AddItem(hitObject);
                    shouldDisappear = true;
                        break;

                   case Item.ItemType.HEALTH:
                    shouldDisappear = AdjustHitPoints(hitObject.quantity);
                        break;
                        default:
                   break;
               }
               if (shouldDisappear)
               {
                   collision.gameObject.SetActive(false);
               }
           }
       }
   }
   //Method to adjust hit points
   public bool AdjustHitPoints(int amount)
   {
       {
           if (hitPoints.value < maxHitPoints)
            {
                //Add amount parameter to existing hitpoint count
                hitPoints.value = hitPoints.value + amount;
                print("Adjusted hitpoints by: " + amount + ".New value: " + hitPoints.value);
                return true;
            }
            return false;
       }
   }
}
