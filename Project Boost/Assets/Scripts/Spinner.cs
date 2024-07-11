using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    [SerializeField] float rotation = 30;

    void Update()
    {
        transform.Rotate(0, 0, rotation * Time.deltaTime);
    }
}
