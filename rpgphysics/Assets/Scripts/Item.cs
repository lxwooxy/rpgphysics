using UnityEngine;

//Creates an entry in the Create submenu, allows us to create instances of the Item Scriptable Object.
[CreateAssetMenu(menuName = "Item")]
public class Item : ScriptableObject
{
    public string objectName;
    public Sprite sprite;
    public int quantity;
    public bool stackable;
    public enum ItemType
    {
        COIN,
        HEALTH
    }
    public ItemType itemType;
}
