using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float pauseTime = 1f;
    [SerializeField] AudioClip successSFX;
    [SerializeField] AudioClip crashSFX;

    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    AudioSource audioSource;
    Rigidbody rb;
    bool isTransitioning;
    bool collisionsActive = true;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionZ;
        rb.constraints = RigidbodyConstraints.FreezeRotationX;
        rb.constraints = RigidbodyConstraints.FreezeRotationY;
        isTransitioning = false;
    }

    void Update()
    {
        HandleDebugKeys();
    }

    private void HandleDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L)) // go to next level
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C)) // toggle collisions
        {
            collisionsActive = !collisionsActive;
        }
    }

    void OnCollisionEnter(Collision other) 
    {
        if (isTransitioning || !collisionsActive) { return; }

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
        rb.constraints = RigidbodyConstraints.None;
        isTransitioning = true;
        crashParticles.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(crashSFX);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", pauseTime);
    }

    void StartLoadSequence()
    {
        isTransitioning = true;
        successParticles.Play();
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
