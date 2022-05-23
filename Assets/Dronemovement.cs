using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dronemovement : MonoBehaviour
{
    Rigidbody ourDrone;
    private float upForce;
    // Start is called before the first frame update
    public void Start()
    {
        ourDrone = GetComponent<Rigidbody>();
        ourDrone.AddRelativeForce(Vector3.up*upForce);
    }

    private void FixedUpdate()
    {
        DroneMoveUpandDown();

    }
    void DroneMoveUpandDown()
    {
        if(Input.GetKey(KeyCode.I))
        {
            upForce = 400;
        }
        else if(Input.GetKey(KeyCode.K)){
            upForce = -250;
        }
        else if (!Input.GetKey(KeyCode.I) && !Input.GetKey(KeyCode.K))
        {
            upForce = 98.1f;
        }
    }
}
