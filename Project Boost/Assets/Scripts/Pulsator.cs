using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulsator : MonoBehaviour
{
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
