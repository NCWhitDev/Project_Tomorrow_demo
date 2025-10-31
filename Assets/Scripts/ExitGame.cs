using UnityEngine;
using UnityEngine.UI;

public class ExitGame : MonoBehaviour
{
    public void QuitGame()
    {
        Debug.Log("Quitting game...See you Tomorrow.");
        Application.Quit();
    }

}
