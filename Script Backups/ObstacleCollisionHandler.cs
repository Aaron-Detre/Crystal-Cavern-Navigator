using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class ObstacleCollisionHandler : MonoBehaviour
{
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Player")
        {
            rb.useGravity = true;

            // Only works in the case where oscillator is on parent and spinner is on this.
            // Not sure if there's a more abstracted way of doing it.
            if (GetComponentInParent<Oscillator>() != null)
            {
                GetComponentInParent<Oscillator>().enabled = false;
            }
            
            if (TryGetComponent(out Spinner spinner))
            {
                spinner.enabled = false;
            }
        }
    }
}
