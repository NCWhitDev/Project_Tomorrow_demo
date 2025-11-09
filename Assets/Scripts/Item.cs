using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public int ID; //Unique identifier for the item
    public string Name; //Name of the item

    public virtual void PickUp() //Method to handle item pickup logic
    {
        //Sprite itemIcon = GetComponent<Image>().sprite; //Get the item's image for UI display
        Sprite itemIcon = GetComponent<SpriteRenderer>().sprite; //Get the item's sprite for UI display
        if(ItemPickUpUIController.Instance != null)
        {
            ItemPickUpUIController.Instance.ShowItemPickup(Name, itemIcon); //Show pickup UI
        }

        
        Debug.Log($"Picked up item: {Name} (ID: {ID})");
    }
}
