using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public bool completed;

    public void Start()
    {
        completed = false;
    }
    public void OnMouseOver()
    {
        if (Input.GetMouseButton(0))
        {
            completed = true;
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
