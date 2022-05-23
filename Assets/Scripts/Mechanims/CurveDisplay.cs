using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveDisplay : MonoBehaviour
{
    public Transform[] controlPoints;
    public Transform target;
    public float speed = 1.0f;

    private int curveCount = 0;
    private int layerOrder = 0;
    private int SEGMENT_COUNT = 50;
    private List<Vector3> pVectors = new List<Vector3>();

    float rProgress;
    void Start()
    {
        curveCount = (int)controlPoints.Length / 3;
        DrawCurve();
    }
    int index;
    void Update()
    {
        if (target)
        {
            Vector3 tPos = pVectors[index];
            target.localPosition = Vector3.MoveTowards(target.localPosition, tPos, Time.deltaTime * speed);
            if(Vector3.Distance(target.localPosition,tPos) < 0.05f)
            {
                index += 1;
                if (index >= pVectors.Count)
                    index = 0;
            }
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        curveCount = (int)controlPoints.Length / 3;
        Vector3 prvPixel = Vector3.zero;
        for (int j = 0; j < curveCount; j++)
        {
            for (int i = 1; i <= SEGMENT_COUNT; i++)
            {
                float t = i / (float)SEGMENT_COUNT;
                int nodeIndex = j * 3;
                Vector3 pixel = CalculateCubicBezierPoint(t, controlPoints[nodeIndex].position, controlPoints[nodeIndex + 1].position, controlPoints[nodeIndex + 2].position, controlPoints[nodeIndex + 3].position);
                Gizmos.DrawLine(prvPixel, pixel);
                prvPixel = pixel;
            }
        }
        Gizmos.color = Color.red;
        Gizmos.DrawLine(controlPoints[0].localPosition,controlPoints[1].localPosition);
        Gizmos.DrawLine(controlPoints[3].localPosition,controlPoints[2].localPosition);
        Gizmos.DrawLine(controlPoints[4].localPosition,controlPoints[3].localPosition);
        Gizmos.DrawLine(controlPoints[6].localPosition,controlPoints[5].localPosition);
    }
    void DrawCurve()
    {
        for (int j = 0; j < curveCount; j++)
        {
            for (int i = 1; i <= SEGMENT_COUNT; i++)
            {
                float t = i / (float)SEGMENT_COUNT;
                int nodeIndex = j * 3;
                Vector3 pixel = CalculateCubicBezierPoint(t, controlPoints[nodeIndex].localPosition, controlPoints[nodeIndex + 1].localPosition, controlPoints[nodeIndex + 2].localPosition, controlPoints[nodeIndex + 3].localPosition);
                pVectors.Add(pixel);
            }
        }
    }

    Vector3 CalculateCubicBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        Vector3 p = uuu * p0;
        p += 3 * uu * t * p1;
        p += 3 * u * tt * p2;
        p += ttt * p3;

        return p;
    }

}
