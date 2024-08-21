using UnityEngine;

/*
 * Handles the rocket's player controls and thruster particle/sound effects.
 */
public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationThrust = 100f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainEngineParticles;

    Rigidbody rb;
    AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    /*
     * Applies the main thruster when the Spacebar is held down.
     */
    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    /*
     * Applies the left/right rotation thrusters when the A/D is held down.
     */
    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            StartLeftRotation();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            StartRightRotation();
        }
    }

    /*
     * Starts the main thruster by applying force to the rocket, playing the engine 
     * sfx, and activating the thruster particle system.
     */
    void StartThrusting()
    {
        // Applies a positive force onto the rocket's Y axis. 
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);

        // Starts the engine audio if it isn't already playing.
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }

        // Starts the thruster particle system if it isn't already playing.
        if (!mainEngineParticles.isPlaying)
        {
            mainEngineParticles.Play();
        }
    }

    /*
     * Stops the sound/particle effects of the thruster.
     */
    void StopThrusting()
    {
        audioSource.Stop();
        mainEngineParticles.Stop();
    }

    /*
     * Applies a left rotation (positive rotation on the Z axis) to the rocket.
     */
    private void StartLeftRotation()
    {
        ApplyRotation(rotationThrust);
    }

    /*
     * Applies a right rotation (negative rotation on the Z axis) to the rocket.
     */
    private void StartRightRotation()
    {
        ApplyRotation(-rotationThrust);
    }

    /*
     * Applies a given rotation to the rocket.
     */
    void ApplyRotation(float rotationThisFrame)
    {
        // Freezing rotation to manually rotate.
        rb.freezeRotation = true; 

        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);

        // Unfreeze rotation so physics takes over.
        rb.constraints = RigidbodyConstraints.FreezeRotationX 
                        | RigidbodyConstraints.FreezeRotationY 
                        | RigidbodyConstraints.FreezePositionZ; 
    }
}
