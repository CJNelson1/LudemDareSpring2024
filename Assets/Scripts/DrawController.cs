using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public GameObject sigilContainer;
    public SigilScorer sigilScorer;
    private Score score;

    public bool good;
    public bool drawingStateStart;
    private bool drawingCompleted;

    public List<LineRenderer> goodDrawings;
    public List<LineRenderer> badDrawings;

    private float elapsedTime = 0f;
    private bool timerActive = false;
    private Score prevBestScore = 0;

    private Director Director;
    public Sigil[] AllSigils;
    int sessionSigilIndex;

    public void Awake() 
    {
        Director = GameObject.Find("Director").GetComponentInParent<Director>();
        sessionSigilIndex = Director.GetActiveSigilIndex();
        sigil = Instantiate(AllSigils[sessionSigilIndex], sigilContainer.transform);
        sigil.transform.localPosition = new Vector3(0,0,15);
    }

    public void Start()
    {
        goodDrawings = new List<LineRenderer>();
        badDrawings = new List<LineRenderer>();
        timerActive = true;
    }
    public void Update()
    {
        hits = Physics2D.GetRayIntersectionAll(Camera.main.ScreenPointToRay(Input.mousePosition));
        good = false;
        foreach(RaycastHit2D hit in hits)
        {
            if(hit.collider.gameObject.name == "SigilDrawing")
            {
                good = true;
            }
            if(hit.collider.gameObject.layer == 6)
            {
                drawingCompleted = sigil.CheckForCheckpointCompletion();
                if (drawingCompleted)
                {
                    timerActive = false;
                    UpdateScoreAndDemondex();
                    SceneManager.LoadScene("Storefront");
                }
            }
        }
        Draw();
        if (lineRenderer != null)
        {
            SetDrawColor(good);
        }
        if (timerActive)
        {
            elapsedTime += Time.deltaTime;
            // print("Elapsed Time: " + elapsedTime.ToString("F2") + " seconds");
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

    private void UpdateScoreAndDemondex()
    {
        score = sigilScorer.AddScore(sigil, goodDrawings, badDrawings, elapsedTime);
        Director.latestScore = score;
        prevBestScore = Director.demondex[sessionSigilIndex];
        print("elapsedTime = " + elapsedTime);
        print("score = " + score);
        print("prevBestScore = " + prevBestScore);

        if (score > prevBestScore)
        {
            Director.demondex[sessionSigilIndex] = score;
            print("overwrote old demondex score");
        }
    }
}
