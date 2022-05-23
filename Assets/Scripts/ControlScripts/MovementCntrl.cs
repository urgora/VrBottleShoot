using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementCntrl : MonoBehaviour
{
    public Vector3 direction;
    public float speed;
    public float length;
    public bool _isPingpong;

    Transform mTransform;
    Vector3 startPos;
    
    private void Start()
    {
        mTransform = transform;
        startPos = transform.localPosition;
    }

    private void FixedUpdate()
    {
        if (_isPingpong)
        {
            mTransform.localPosition = startPos + direction * Mathf.PingPong(Time.time * speed, length);
        }
        else
        {
            float amplitude = Mathf.Sin(Time.time * speed) * length;
            Vector3 updatedPos = startPos + direction * amplitude;
            mTransform.localPosition = updatedPos;
        }
    }
}
