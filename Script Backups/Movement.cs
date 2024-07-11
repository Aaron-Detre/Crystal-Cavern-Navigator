
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationThrust = 100f;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem mainEngineParticles;
    // [SerializeField] ParticleSystem leftBoosterParticles;
    // [SerializeField] ParticleSystem rightBoosterParticles;

    Rigidbody rb;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

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
        /*
        else
        {
            StopRotation();
        }
        */
    }

    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }

        PlayParticleSystem(mainEngineParticles);
    }

    void StopThrusting()
    {
        audioSource.Stop();
        mainEngineParticles.Stop();
    }

    private void StartLeftRotation()
    {
        ApplyRotation(rotationThrust);
        // PlayParticleSystem(rightBoosterParticles);
        // leftBoosterParticles.Stop();
    }

    private void StartRightRotation()
    {
        ApplyRotation(-rotationThrust);
        // PlayParticleSystem(leftBoosterParticles);
        // rightBoosterParticles.Stop();
    }

    /*
    private void StopRotation()
    {
        leftBoosterParticles.Stop();
        rightBoosterParticles.Stop();
    }
    */

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // freezing rotation to manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionZ; // unfreeze rotation so physics takes over
    }

    void PlayParticleSystem(ParticleSystem ps)
    {
        if (!ps.isPlaying)
        {
            ps.Play();
        }
    }
}
