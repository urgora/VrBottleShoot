using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetlineguide : MonoBehaviour
{
    LineRenderer line;
    void Start()
    {
        line = GetComponent<LineRenderer>();
      
        line.positionCount = 2;
    }
    void Update()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, transform.forward,out hit);
        line.SetPosition(0, transform.position);
        line.SetPosition(1, hit.point);
        line.enabled = true;
    }
   
}
