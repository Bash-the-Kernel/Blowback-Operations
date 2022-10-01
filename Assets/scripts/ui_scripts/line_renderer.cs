using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class line_renderer : MonoBehaviour
{
    private LineRenderer Lr;
    private Transform[] points;
    private void Awake()
    {
        Lr = GetComponent<LineRenderer>();
    }

    public void setupLine(Transform[] points)
    {
        Lr.positionCount = points.Length;
        this.points = points;
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < points.Length; i++)
        {
            Lr.SetPosition(i, points[i].position);
        }
    }
}
