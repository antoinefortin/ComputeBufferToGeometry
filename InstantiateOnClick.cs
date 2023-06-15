using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateOnClick : MonoBehaviour
{
    public GameObject prefab; // The object to be instantiated
    public Camera camera; // The camera to raycast from

    // Update is called once per frame
    void Update()
    {
        // If the left mouse button is clicked
        if (Input.GetMouseButtonDown(0))
        {
            // Create a ray from the camera going into the world from the mouse position
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Perform the raycast
            if (Physics.Raycast(ray, out hit))
            {
                // Instantiate a new object at the point where the raycast hit
                Instantiate(prefab, hit.point + new Vector3(0.0f, 1.25f, 0.0f), Quaternion.identity);
            }
        }
    }
}
