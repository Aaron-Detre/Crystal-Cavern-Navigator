using UnityEngine;

/*
 * Rotates the Game Object (worm's eye) to look at the player.
 */
public class WormEyeTracker : MonoBehaviour
{
    GameObject target;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        transform.LookAt(target.transform.position);
    }
}
