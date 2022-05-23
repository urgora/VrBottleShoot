using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ropejoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<CharacterJoint>().connectedBody = transform.parent.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
