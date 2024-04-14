using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sigil : MonoBehaviour
{
    public int checkpointTolerance;
    public float checkpointRadius;
    public List<Checkpoint> checkpoints;


    void Start()
    {
        foreach (Checkpoint c in checkpoints)
        {
            c.GetComponent<SpriteRenderer>().enabled = false;
            c.GetComponent<CircleCollider2D>().radius = checkpointRadius;
        }
    }

    public bool CheckForCheckpointCompletion()
    {
        int CompletedCheckpoints = 0;
        foreach(Checkpoint c in checkpoints)
        {
            if (c.completed)
            {
                CompletedCheckpoints++;
            }
        }

        print(string.Format("{0} checkpoints completed out of {1}", CompletedCheckpoints, checkpoints.Count));
        if (CompletedCheckpoints >= checkpoints.Count - checkpointTolerance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
