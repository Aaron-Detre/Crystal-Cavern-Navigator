using UnityEngine;

/*
 * Rotates the Game Object around its Z axis
 */
public class Spinner : MonoBehaviour
{
    [SerializeField] float rotation = 30;

    void Update()
    {
        transform.Rotate(0, 0, rotation * Time.deltaTime);
    }
}
