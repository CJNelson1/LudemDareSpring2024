using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawController : MonoBehaviour
{
    public Camera m_camera;
    public GameObject drawer;

    LineRenderer lineRenderer;

    Vector2 lastPos;

    public void Update()
    {
        Draw();
    }
    void Draw()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            CreateDrawer();
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Vector2 currentPos = m_camera.ScreenToWorldPoint(Input.mousePosition);
            if (currentPos != lastPos)
            {
                AddPoint(currentPos);
                lastPos = currentPos;
            }
        }
        else
        {
            lineRenderer = null;
        }
    }

    void CreateDrawer()
    {
        GameObject drawInstance = Instantiate(drawer);
        lineRenderer = drawInstance.GetComponent<LineRenderer>();

        Vector2 position = m_camera.ScreenToWorldPoint(Input.mousePosition);

        lineRenderer.SetPosition(0, position);
        lineRenderer.SetPosition(1, position);
    }

    void AddPoint(Vector2 point)
    {
        lineRenderer.positionCount++;
        int index = lineRenderer.positionCount - 1;
        lineRenderer.SetPosition(index, point);
    }
}
