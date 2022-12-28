using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float speed = 0.5f;
    public Vector3 dirVec;// = new Vector3();

    Rigidbody rigidBody;
    

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        this.transform.Translate(dirVec * speed * Time.deltaTime);
    }
}
