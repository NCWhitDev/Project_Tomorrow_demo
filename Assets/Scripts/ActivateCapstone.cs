using UnityEngine;
using UnityEngine.SceneManagement;
public class ActivateCapstone : MonoBehaviour
{
    private SaveController save; // Reference to the SaveController script
    [SerializeField] BoxCollider2D action;
    //[SerializeField] GameObject GameController;
    private Transform player; // Reference to the player's Transform
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            save = FindFirstObjectByType<SaveController>();
            save.FirstSaveGame();
            SceneManager.LoadScene("Chamber_action");
        }
    }
}

// Source: https://docs.unity3d.com/ScriptReference/SceneManagement.SceneManager.LoadScene.html
//https://www.youtube.com/watch?v=J4Spej4mfEg