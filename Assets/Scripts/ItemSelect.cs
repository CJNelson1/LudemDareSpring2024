using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSelect : MonoBehaviour
{

    public int itemSelected; // send this to the Director (activeSigil) - this will determine what sigil to render

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            print(ray);
            print(hit);
            print(hit.collider);

            // TODO this isn't detecting the collider on items :(
            // Check if the ray hits an item
            if (hit.collider != null)
            {
                print("hit collider not null");
                // Check if the hit object has a SpriteRenderer component
                CanvasRenderer item = hit.collider.gameObject.GetComponent<CanvasRenderer>();

                if (item != null)
                {
                    // Get the name of the clicked game object
                    string objectName = hit.collider.gameObject.name;
                    Debug.Log("Clicked on: " + objectName);
                }
            }
        }
    }
}   
