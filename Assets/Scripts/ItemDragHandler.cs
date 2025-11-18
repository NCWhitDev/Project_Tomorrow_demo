using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Transform originalParent;
    CanvasGroup canvasGroup;

    public float minDropDistance = 2f; //Minimum distance from player to drop item
    public float maxDropDistance = 3f; //Maximum distance from player to drop item

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    // Start of interface implementation

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent; //Save original parent to return to it later
        transform.SetParent(transform.root); //Move to top of hierarchy to avoid being clipped by other UI elements (Above all canvases)
        canvasGroup.blocksRaycasts = false; //Disable raycast blocking so we can drop it on other UI elements
        canvasGroup.alpha = 0.6f; //Make item semi-transparent while dragging
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position; //Follow mouse position
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true; //Enables raycasts
        canvasGroup.alpha = 1f; //No longer transparent

        Slot dropSlot = eventData.pointerEnter?.GetComponent<Slot>(); //Slot where item dropped
        if (dropSlot == null)
        {
            GameObject dropItem = eventData.pointerEnter;
            if (dropItem != null)
            {
                dropSlot = dropItem.GetComponentInParent<Slot>();
            }
        }
        Slot originalSlot = originalParent.GetComponent<Slot>();

        if (dropSlot != null)
        {
            //Is a slot under drop point
            if (dropSlot.currentItem != null)
            {
                //Slot has an item - swap items
                dropSlot.currentItem.transform.SetParent(originalSlot.transform);
                originalSlot.currentItem = dropSlot.currentItem;
                dropSlot.currentItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            }
            else
            {
                originalSlot.currentItem = null;
            }

            //Move item into drop slot
            transform.SetParent(dropSlot.transform);
            dropSlot.currentItem = gameObject;
        }
        else
        {
            //If where we are dropping is not within the inventory slots
            if(!isWithinInventory(eventData.position))
            {
                //Drop item
                dropItem(originalSlot);
            }
            else
            {
                //Snap back to original slot
                transform.SetParent(originalParent);
            }
        }

        GetComponent<RectTransform>().anchoredPosition = Vector2.zero; //Center
    }

    bool isWithinInventory(Vector2 mousePos)
    {
        RectTransform inventoryRect = originalParent.parent.GetComponent<RectTransform>();
        return RectTransformUtility.RectangleContainsScreenPoint(inventoryRect, mousePos);
    }

    void dropItem(Slot orginalSlot)
    {
        orginalSlot.currentItem = null;

        //Find player to drop item near
        Transform playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;
        //if (playerTransform == null)
        //{
        //    Debug.LogError("Missing Player tag, cant drop item.");
        //    return;
        //}
        //////Random offset so item doesn't drop exactly on player
        ////Vector2 dropOffset = Random.insideUnitCircle.normalized * Random.Range(minDropDistance, maxDropDistance); //Random direction and distance within min and max drop distance
        ////Vector2 dropPosition = (Vector2)playerTransform.position + dropOffset;

        ////Instantiate item in world
        //Instantiate(gameObject, dropPosition, Quaternion.identity); //Instantiate item prefab at drop position in world

        //Destroy item in inventory UI
        Destroy(gameObject);
    }
}