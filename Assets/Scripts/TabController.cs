using UnityEngine;
using UnityEngine.UI;

public class TabController : MonoBehaviour
{
    public Image[] tabimages;
    public GameObject[] pages;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ActivateTab(0); // Activate the first tab by default
    }

    public void ActivateTab(int tabNo)
    {
        for (int i = 0; i < pages.Length; i++)
        {
            // Deactivate all pages and set tab images to gray
            pages[i].SetActive(false);
            tabimages[i].color = Color.gray;
        }

        // Activate the selected page and set its tab image to white
        pages[tabNo].SetActive(true);
        tabimages[tabNo].color = Color.white;
    }
}
