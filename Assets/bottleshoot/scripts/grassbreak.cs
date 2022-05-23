using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grassbreak : MonoBehaviour
{
    public float radius;
    public float force;


    private void Start()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach(Collider glassobjects in colliders)
        {
            Rigidbody rb = glassobjects.GetComponent<Rigidbody>();
            if(rb!=null&&glassobjects.tag=="glass")
            {
                rb.AddExplosionForce(force,transform.position,radius);
            }
        }
    }
}
