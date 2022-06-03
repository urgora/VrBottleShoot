using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floationggun : MonoBehaviour
{
    void OnCollisionStay(Collision collisionInfo)
    {
        if(collisionInfo.gameObject.layer==28)
        {
            collisionInfo.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}
