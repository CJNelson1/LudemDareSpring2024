using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DrawController : MonoBehaviour
{
    RaycastHit2D[] hits;
    public Camera m_camera;
    public GameObject drawer;

    LineRenderer lineRenderer;

    Vector2 lastPos;

    public Material goodMaterial;
    public Material badMaterial;

    public Sigil sigil;
    public CompositeCollider2D sigilMesh;
    public bool good;
    public bool drawingStateStart;

    public List<LineRenderer> goodDrawings;
    public List<LineRenderer> badDrawings;

    public void Start()
    {
        goodDrawings = new List<LineRenderer>();
        badDrawings = new List<LineRenderer>();
    }
    public void Update()
    {
        hits = Physics2D.GetRayIntersectionAll(Camera.main.ScreenPointToRay(Input.mousePosition));
        good = false;
        foreach(RaycastHit2D hit in hits)
        {
            if(hit.collider.gameObject.name == "ColliderMesh")
            {
                good = true;
            }
            if(hit.collider.gameObject.layer == 6)
            {
                sigil.CheckForCheckpointCompletion();
            }
        }
        Draw();
        if (lineRenderer != null)
        {
            SetDrawColor(good);
        }
    }
    void Draw()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            CreateDrawer();
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (good != drawingStateStart)
            {
                EndDrawer();
                CreateDrawer();
            }
            Vector2 currentPos = m_camera.ScreenToWorldPoint(Input.mousePosition);
            if (currentPos != lastPos)
            {
                AddPoint(currentPos);
                lastPos = currentPos;
            }
        }
        else
        {
            if (lineRenderer != null)
            {
                EndDrawer();
                if(sigil.CheckForCheckpointCompletion())
                {
                    print("Sigil completed");
                }
                
            }
        }
    }

    void CreateDrawer()
    {
        GameObject drawInstance = Instantiate(drawer);
        lineRenderer = drawInstance.GetComponent<LineRenderer>();

        Vector2 position = m_camera.ScreenToWorldPoint(Input.mousePosition);

        lineRenderer.SetPosition(0, position);
        lineRenderer.SetPosition(1, position);

        drawingStateStart = good;
    }
    void EndDrawer()
    {
        if (drawingStateStart)
        {
            goodDrawings.Add(lineRenderer);
        }
        else
        {
            badDrawings.Add(lineRenderer);
        }
        lineRenderer = null;
    }
    void AddPoint(Vector2 point)
    {
        lineRenderer.positionCount++;
        int index = lineRenderer.positionCount - 1;
        lineRenderer.SetPosition(index, point);
    }
    public void SetDrawColor(bool good)
    {
        if (good)
        {
            lineRenderer.material = goodMaterial;
        }
        else
        {
            lineRenderer.material = badMaterial;
        }
    }
}
