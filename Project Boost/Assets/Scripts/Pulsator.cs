using UnityEngine;

/*
 * Smoothly contracts then expands a Game Object.
 */
public class Pulsator : MonoBehaviour
{
    // pulsPercent dictatates the minimum scale the Game Object will contract to
    [SerializeField] [Range(0,1)] float pulsePercent = .8f; 
    [SerializeField] float pulseSpeed = 1f;
    Vector3 scale;
    
    void Start()
    {
        scale = transform.localScale;
    }

    void Update()
    {        
        float t = Mathf.Sin(Time.time * pulseSpeed);
        t += 1;
        t /= 2;
        transform.localScale = Vector3.Lerp(scale, pulsePercent * scale, t);
        
    }
}
