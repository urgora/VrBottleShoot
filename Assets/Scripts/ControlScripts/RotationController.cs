using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationController : MonoBehaviour
{
    public Vector3 direction=new Vector3 (0,.5f,0);
    public float Speed=1;
    public bool isConinues=true;
    public float AngleToRotate;
    public bool lastbullet = false;
    Quaternion initialRot;

    private void Start()
    {
        initialRot = transform.rotation;
    }

    public void Update()
    {
        if (lastbullet)
        {
            transform.Rotate(new Vector3(0, 0, Time.unscaledTime*Speed), Space.Self);
        }
    }

    private void FixedUpdate()
    {
        if (isConinues)
        {
            transform.Rotate(direction * Speed, Space.Self);
        }
        else
        {
            //transform.eulerAngles = direction * (Mathf.Sin(Time.time*Speed)*AngleToRotate);
            transform.rotation = initialRot * Quaternion.AngleAxis(Mathf.Sin(Time.time * Speed) * AngleToRotate, direction);
        }
    }
}
