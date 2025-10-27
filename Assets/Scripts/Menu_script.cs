using UnityEngine;

public class Menu_script : MonoBehaviour
{
    public GameObject menuUI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Hide the menu UI at the start of the game
        menuUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // Toggle the menu UI visibility when the Escape key is pressed
            menuUI.SetActive(!menuUI.activeSelf);
        }
    }
}
