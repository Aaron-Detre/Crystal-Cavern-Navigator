using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Handles rocket/game behavior when the rocket collides with different Game Objects.
 */
public class CollisionHandler : MonoBehaviour
{
    // How long the game pauses when the player fails/beats a level.
    [SerializeField] float pauseTime = 1f; 
    [SerializeField] AudioClip successSFX;
    [SerializeField] AudioClip crashSFX;

    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    AudioSource audioSource;
    Rigidbody rb;
    bool isTransitioning; // True when the player has either failed or succeeded.
    bool collisionsActive = true; // Only relevant for debugging

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionZ;
        isTransitioning = false;
    }

// Debug controls removed for final build
/*
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
*/

    void OnCollisionEnter(Collision other) 
    {
        // If the scene is transitioning, ignore any further collisions.
        if (isTransitioning || !collisionsActive) { return; }

        switch (other.gameObject.tag)
        {
            case "Friendly": // Do nothing
                break; 
            case "Finish": // Player beats level
                StartLoadSequence();
                break;
            default: // Player crashes
                StartCrashSequence();
                break;
        }
    }

    /*
     * Handles the various elements that occur when the player crashes the rocket.
     */
    void StartCrashSequence()
    {
        // Allows the rocket to move along the Z axis and rotate along the X Axis, making 
        // crashes more interesting.
        rb.constraints = RigidbodyConstraints.None; 
        
        isTransitioning = true;
        crashParticles.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(crashSFX);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", pauseTime);
    }

    /*
     * Handles the various elements that occur when the player beats a level.
     */
    void StartLoadSequence()
    {
        isTransitioning = true;
        successParticles.Play();
        audioSource.Stop();
        GetComponent<AudioSource>().PlayOneShot(successSFX);
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", pauseTime);
    }

    /*
     * Restarts the current level.
     */
    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    /*
     * Proceeds to the next level in the build sequence.
     */
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
