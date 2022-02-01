using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    Rigidbody rb;
    public float force;
    public bool i;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
      //  rb.AddForce(transform.right * force, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="floor")
        {
            Destroy(gameObject);
        }
    }
}
