using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelerCntrl : MonoBehaviour
{
    public float Speed;
    public float WheelSpeed =30;
    public float Length;
    public Vector3 direction;
    Vector3 startPos;
    public Transform[] Wheels;
    public Vector3 wdirection;

    void Start()
    {
        startPos = transform.localPosition;
    }

    private void FixedUpdate()
    {
        float _amt = Mathf.PingPong(Time.time * Speed,Length);
        transform.localPosition = startPos + direction * _amt;
        for(int i = 0; i < Wheels.Length; i++)
        {
            Wheels[i].localEulerAngles =wdirection * _amt* WheelSpeed;
        }
    }
}
