using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Camera MainCamera;
    Quaternion OriginalRotation;

    void Start()
    {
        OriginalRotation = transform.rotation;
    }

    void Update()
    {
        transform.rotation
            = MainCamera.transform.rotation * OriginalRotation;
    }
}
