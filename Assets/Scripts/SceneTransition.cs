using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public static SceneTransition Instance;

    [SerializeField] private Animator animator;
    [SerializeField] private float transitionTime = 1f; // match animation length

    private void Awake()
    {
        // Simple singleton so we can call this from anywhere
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // keep between scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadSceneWithFade(string sceneName)
    {
        StartCoroutine(LoadSceneCoroutine(sceneName));
    }

    private IEnumerator LoadSceneCoroutine(string sceneName)
    {
        // play fade animation
        animator.SetTrigger("StartFade");

        // wait for the fade to finish
        yield return new WaitForSeconds(transitionTime);

        // load the new scene
        SceneManager.LoadScene(sceneName);
    }
}
