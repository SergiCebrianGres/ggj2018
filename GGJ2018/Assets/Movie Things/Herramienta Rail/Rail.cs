using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public enum moveMode
{
    Linear,
    Catmull
}

[ExecuteInEditMode]
public class Rail : MonoBehaviour
{
    public Transform[] nodes;
    public Rail nextRail;
    public bool gizmo = true;

    private void Start()
    {
        ResetRail();
    }

    private void Update()
    {
        if (nodes.Length > transform.childCount + 1) ResetRail();
    }

    public void ResetRail()
    {
        nodes = GetComponentsInChildren<Transform>();
        foreach (var node in nodes)
        {
            if (node.GetComponent<Rail>() == null) node.gameObject.name = "Node " + node.GetSiblingIndex();
        }
    }

    public Vector3 PositionOnRail(int seg, float ratio, moveMode mode)
    {
        switch (mode)
        {
            case moveMode.Linear:
                return LinearPosition(seg, ratio);
            case moveMode.Catmull:
                return CatmullPosition(seg, ratio);
            default:
                return LinearPosition(seg, ratio);
        }
    }

    public Vector3 LinearPosition(int seg, float ratio)
    {
        Vector3 p1 = nodes[seg].position;
        Vector3 p2 = nodes[seg + 1].position;

        return Vector3.Lerp(p1, p2, ratio);
    }

    public Vector3 CatmullPosition(int seg, float ratio)
    {
        Vector3 p1, p2, p3, p4;
        if (seg == 0)
        {
            p1 = nodes[seg].position;
            p2 = p1;
            p3 = nodes[seg + 1].position;
            p4 = nodes[seg + 2].position;
        }
        else if (seg == nodes.Length - 2)
        {
            p1 = nodes[seg - 1].position;
            p2 = nodes[seg].position;
            p3 = nodes[seg + 1].position;
            p4 = p3;
        }
        else
        {
            p1 = nodes[seg - 1].position;
            p2 = nodes[seg].position;
            p3 = nodes[seg + 1].position;
            p4 = nodes[seg + 2].position;
        }

        float t2 = ratio * ratio;
        float t3 = t2 * ratio;

        float x = 0.5f * (
            (2.0f * p2.x) +
            (-p1.x + p3.x) * ratio 
            + (2.0f * p1.x - 5.0f * p2.x + 4 * p3.x - p4.x) * t2 
            + (-p1.x + 3.0f * p2.x - 3.0f * p3.x + p4.x) * t3
            );

        Vector3 v = 0.5f * (
                (2.0f * p2) +
                (-p1 + p3) * ratio
                + (2.0f * p1 - 5.0f * p2 + 4.0f * p3 - p4) * t2
                + (-p1 + 3.0f * p2 - 3.0f * p3 + p4) * t3
            );

        return v;
    }

    public Quaternion Orientation(int seg, float ratio)
    {
        Quaternion q1 = nodes[seg].rotation;
        Quaternion q2 = nodes[seg+1].rotation;
        
        return Quaternion.Lerp(q1, q2, ratio);
    }

    private void OnDrawGizmos()
    {
        if (gizmo)
        {
            for (int i = 1; i < nodes.Length - 1; i++)
            {
                Handles.color = Color.magenta;
                Handles.DrawDottedLine(nodes[i].position, nodes[i + 1].position, 3);
            }
        }
    }
}
