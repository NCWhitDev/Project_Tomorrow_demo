using System.Collections;   
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickUpUIController : MonoBehaviour
{
    public static ItemPickUpUIController Instance { get; private set; } // Singleton instance - ensures only one instance exists, any other script can access it via ItemPickUpUIController.Instance

    public GameObject popupPrefab; // Reference to the UI GameObject for item pickup
    public int maxPopups; // Reference to the UI GameObject for max item notification
    public float popupDuration = 2f; // Duration for which the popup is displayed

    private readonly Queue<GameObject> activePopups = new(); // Queue to manage active popups

    private void Awake()
    {
        // Implementing Singleton pattern
        if (Instance == null) // If no instance exists, set this as the instance
        {
            Instance = this;
        }
        else
        {
            // If an instance already exists, destroy this duplicate
            Debug.LogWarning("Multiple instances of ItemPickUpUIController.cs detected. Destroying duplicate.");
            Destroy(gameObject);
        }
    }

    public void ShowItemPickup(string itemName, Sprite itemIcon)
    {
        GameObject newPopup = Instantiate(popupPrefab, transform); // Instantiate a new popup from the prefab
        newPopup.GetComponentInChildren<TMP_Text>().text = itemName; // Set the item name text

        Image itemImage = newPopup.transform.Find("ItemIcon")?.GetComponent<Image>();

        if (itemImage != null)
        {
            itemImage.sprite = itemIcon; // Set the item icon image
        }
        else
        {
            Debug.LogWarning("ItemIcon Image component not found in popup prefab.");
        }

        activePopups.Enqueue(newPopup); // Add the new popup to the queue
        if (activePopups.Count > maxPopups) // Limit the number of active popups to 3
        {
            Destroy(activePopups.Dequeue()); // Remove the oldest popup if limit exceeded
        }

        // Fade out and destroy the popup after the specified duration
        StartCoroutine(FadeOutAndDestroy(newPopup));
    }

    private IEnumerator FadeOutAndDestroy(GameObject popup)
    {
        yield return new WaitForSeconds(popupDuration); // Wait for the popup duration
        if (popup == null) yield break; // Exit if the popup has already been destroyed
        CanvasGroup canvasGroup = popup.GetComponent<CanvasGroup>();
        for(float timePassed = 0f; timePassed < 1f; timePassed += Time.deltaTime)
        {
            if (popup == null) yield break; ; // Exit if CanvasGroup is not found
            canvasGroup.alpha = 1f - timePassed; // Gradually reduce alpha to create fade-out effect
            yield return null; // Wait for the next frame
        }
        Destroy(popup); // Destroy the popup after fading out
    }
}
