using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetlineguide : MonoBehaviour
{
    LineRenderer line;
    public GameObject end;

    void Start()
    {
        line = GetComponent<LineRenderer>();
      
        line.positionCount = 2;
    }
    void Update()
    {
        RaycastHit hit;
       // Physics.Raycast(transform.position, transform.forward,out hit);
       // end.transform.position = hit.point;
        line.SetPosition(0, transform.position);
        line.SetPosition(1, end.transform.position);
        line.enabled = true;
    }
   
}
