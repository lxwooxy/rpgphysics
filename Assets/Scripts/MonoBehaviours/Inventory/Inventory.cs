using UnityEngine;
using UnityEngine.UI;
public class Inventory : MonoBehaviour
{
    //Reference to the Slot
    //The Inventory schript will instantiate multiple copies of 
    //this prefab to use as Inventory Slots.
    public GameObject slotPrefab;
    //Inventory Bar has 5 slots
    public const int numSlots = 5;
    //Instantiate an array that will hold Image components.
    //Each component has a Sprite property,
    //when the player adds an Item to their Inventory,
    //we set this Sprite property to the Sprite referenced in the Item.
    //The Sprite will then be displayed in the Slot in the Inventory Bar.
    Image[] itemImages = new Image[numSlots];
    //The items array will hold references to the actual Item, 
    //of type Scriptable Objects, that the player has picked up.
    Item[] items = new Item[numSlots];
    //Each index in the slots array will ref a single Slot prefab.
    //(dynamically instantiated at runtime.)
    //We'll use these references to find the Text object inside a slot.
    GameObject[] slots = new GameObject[numSlots];
    // Start is called before the first frame update
    public void Start()
    {
        CreateSlots();
    }
    public void CreateSlots()
    {
        //Check that we set the prefab
        if (slotPrefab != null)
        {
            //Loop through the number of slots.
            for(int i = 0; i < numSlots; i++)
            {
                //Instantiate a copy of the prefab, assign to newSlot.
                //Change name and append index number at the end.
                GameObject newSlot = Instantiate(slotPrefab);
                newSlot.name = "ItemSlot_" + i;
                //The script will be attached to Inventory Object. The Inventory Object prefab has a single child:Inventory
                //Set the parent of the slot to the child obj at index  0 of Inventory Object
                //(Inventory is a child object of Inventory Object at index:0)
                newSlot.transform.SetParent(gameObject.transform.GetChild(0).transform);
                //Assign this new Slot object to the slots array at the current index.
                slots[i] = newSlot;
                //The child obj of ind 1 of the slot is an item image.
                //We retrieve the image component from the item image and assign it to the item Images array.
                //The source of this image component will appear in the Inventory Slot.
                itemImages[i] = newSlot.transform.GetChild(1).GetComponent<Image>();
            }
        }
    }
public bool AddItem(Item itemToAdd)
    {
        for(int i = 0; i < items.Length; i++)
        {
            if (items[i] != null && items [i].itemType == itemToAdd.itemType && itemToAdd.stackable == true)
            {
                items[i].quantity = items[i].quantity +1;
                Slot slotScript = slots [i].GetComponent<Slot>();
                Text quantityText = slotScript.qtyText;
                quantityText.enabled = true;
                quantityText.text = items[i].quantity.ToString();
                return true;
            }
            if (items[i] == null)
            {
                items[i] = Instantiate(itemToAdd);
                items[i].quantity = 1;
                itemImages[i].sprite = itemToAdd.sprite;
                itemImages[i].enabled = true;
                return true;
            }
        }
        return false;
    }
}
