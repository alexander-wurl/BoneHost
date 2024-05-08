using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LandmarkHover : MonoBehaviour
{
    // ray
    Ray ray;

    // ray hit
    RaycastHit hit;

    // text of anatomical landmarks
    private Text LandmarksText;

    // gui element for selected anatomical landmark
    private GameObject LandmarksSelector;

    // selected anatomical landmark
    private GameObject Landmark = null;

    // init elements to be used
    private void Start()
    {
        LandmarksText = GameObject.Find("/Canvas/LandmarksPanel/Landmarks").GetComponent<Text>();
        LandmarksSelector = GameObject.Find("/Canvas/LandmarksPanel/Panel");
        LandmarksSelector.GetComponent<Image>().color = Color.clear;
    }

    // each frame a ray looks for intersections with landmark collider (OnMouseOver might be used as well)
    // if one is found extracted name of collider is used to color text in list of anatomical landmarks
    // landmark size will be adjusted as well
    void Update()
    {
        // ray from camera
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            // find landmark based on collider name
            Landmark = GameObject.Find("/Bone/" + hit.collider.name);
            Landmark.transform.localScale = new Vector3(15, 15, 15);

            // find hovered landmark in landmark list
            string[] Array = LandmarksText.text.Split("\n"[0]);

            // cut string because label contains 'Sphere' (= 6 chars)
            string l = hit.collider.name.Substring(6);

            // loop anatomical landmark list until hovered element is found
            for (int i = 0; i < Array.Length; ++i)
            {
                if (Array[i] == l)
                {
                    // adjust local position and color to make anatomical landmark 'selected'
                    LandmarksSelector.transform.localPosition = new Vector3(LandmarksSelector.transform.localPosition.x, 270 - (i * 16), LandmarksSelector.transform.localPosition.z);
                    Color c = Color.red;
                    c.a = 0.5f;
                    LandmarksSelector.GetComponent<Image>().color = c;
                }

            }
        }

    }

    // reset landmark size and color
    private void OnMouseExit()
    {
        if (Landmark)
        {
            Landmark.transform.localScale = new Vector3(7.5f, 7.5f, 7.5f);
            LandmarksSelector.GetComponent<Image>().color = Color.clear;
        }
    }

}
