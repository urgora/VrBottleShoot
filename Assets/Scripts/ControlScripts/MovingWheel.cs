using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWheel : MonoBehaviour
{
    public float Speed;
    public float lengthToMove;
    Vector3 startPos;
    Vector3 rdir, mdir;
    public Vector3 dir = Vector3.forward;
    public Vector3 moveDir = Vector3.right;
    Quaternion initialRot;


    void Start()
    {
        startPos = transform.localPosition;
        initialRot = transform.localRotation;
    }
    public float radius = 7.5f;
    void FixedUpdate()
    {
        float x = Mathf.Sin(Time.time*Speed) * lengthToMove;
        transform.localPosition = startPos + moveDir * x;
        float Zrot = ((Mathf.Rad2Deg * x) / radius);
        transform.localRotation = initialRot*Quaternion.AngleAxis(-Zrot, dir);
    }
}


