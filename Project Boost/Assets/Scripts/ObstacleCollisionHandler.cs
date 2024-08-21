using UnityEngine;

/*
 * Handles the Game Objects behavior when it is collided with.
 */
public class ObstacleCollisionHandler : MonoBehaviour
{
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void OnCollisionEnter(Collision other) {
        // Activates gravity causing a sort of ragdoll effect on the Game Object when collided.
        rb.useGravity = true; 

        // Disables Game Object's Pulsator component if it has one.
        if (TryGetComponent(out Pulsator pulsator))
        {
            pulsator.enabled = false;
        }
        
        // Disables Game Object's Spinner component if it has one.
        if (TryGetComponent(out Spinner spinner))
        {
            spinner.enabled = false;
        }
    }
    
}
