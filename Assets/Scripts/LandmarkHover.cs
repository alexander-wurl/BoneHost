using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LandmarkHover : MonoBehaviour
{
    // Ray and raycast hit
    private Ray ray;
    private RaycastHit hit;

    // Text component showing the landmarks
    private Text landmarksText;

    // The currently selected landmark GameObject
    private GameObject landmark = null;

    // Store the original text for reset purposes
    private string originalText;

    // Init elements to be used
    private void Start()
    {
        // Find the landmarks text object
        landmarksText = GameObject.Find("/Canvas/LandmarksPanel/Landmarks").GetComponent<Text>();
        // Store the original text to reset later
        originalText = landmarksText.text;
    }

    // Method to highlight the target string in the landmarks text
    void HighlightTargetString(string targetString)
    {
        if (originalText.Contains(targetString))
        {
            // Replace the target string with a highlighted version
            string highlightedText = originalText.Replace(targetString, $"<color=red>{targetString}</color>");

            // Only update the text if it has changed (to avoid unnecessary mesh rebuilds)
            if (landmarksText.text != highlightedText)
            {
                landmarksText.text = highlightedText;
            }
        }
    }

    // Method to reset the highlighted text to the original
    void ResetHighlightedText()
    {
        landmarksText.text = originalText; // Set back to original text
    }

    // Update is called once per frame
    void Update()
    {
        // Ray from the camera to the mouse position
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Perform raycast and check if it hits something
        if (Physics.Raycast(ray, out hit))
        {
            // Find the landmark by collider name
            landmark = GameObject.Find("/Bone/" + hit.collider.name);
            if (landmark != null)
            {
                // Scale the landmark for visibility
                landmark.transform.localScale = new Vector3(15, 15, 15);

                // Cut the collider name to match the target string format
                string targetString = hit.collider.name.Substring(6);

                // Highlight the target string in the text
                HighlightTargetString(targetString);
            }
        }
        else
        {
            // Reset highlighted text if the mouse is not over any landmark
            ResetHighlightedText();
        }
    }

    // Reset the landmark size when the mouse exits
    private void OnMouseExit()
    {
        if (landmark)
        {
            // Reset the scale of the landmark
            landmark.transform.localScale = new Vector3(7.5f, 7.5f, 7.5f);
            ResetHighlightedText(); // Reset text highlighting when mouse exits
        }
    }
}
