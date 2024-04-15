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
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
            print(hit);
            print(hit.collider);

            // TODO this isn't detecting the collider on items :(
            // Check if the ray hits an item
            if (hit.collider != null)
            {
                print("hit collider not null");
                string tag = hit.collider.gameObject.tag;
                print("obj tag: " + tag);

                if (tag == "Item")
                {
                    // Get the name of the clicked game object
                    string objectName = hit.collider.gameObject.name;
                    Debug.Log("Clicked on: " + objectName);
                }
            }
        }
    }
}   
