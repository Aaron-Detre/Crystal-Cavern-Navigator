using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float pauseTime = 1f;
    [SerializeField] AudioClip successSFX;
    [SerializeField] AudioClip crashSFX;

    AudioSource audioSource;
    bool isTransitioning;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        isTransitioning = false;
    }

    void OnCollisionEnter(Collision other) 
    {
        if (isTransitioning) { return; }

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Hit Friendly");
                break;
            case "Finish":
                StartLoadSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        // Particle effect
        audioSource.Stop();
        audioSource.PlayOneShot(crashSFX);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", pauseTime);
    }

    void StartLoadSequence()
    {
        isTransitioning = true;
        // Particle effect
        audioSource.Stop();
        GetComponent<AudioSource>().PlayOneShot(successSFX);
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", pauseTime);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
