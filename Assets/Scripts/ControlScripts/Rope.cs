
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Rope : MonoBehaviour
{
    LineRenderer line;
    public Transform anchor, huk;

    private void Start()
    {
        line = GetComponent<LineRenderer>();
    }
    private void LateUpdate()
    {
        line.SetPosition(0, anchor.position);
        line.SetPosition(1, huk.position);
    }
}
