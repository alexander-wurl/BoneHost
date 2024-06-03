using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathLogger : MonoBehaviour
{
    void Start()
    {
        // Log Application.dataPath
        Debug.Log("Application.dataPath: " + Application.dataPath);

        // Log Application.persistentDataPath
        Debug.Log("Application.persistentDataPath: " + Application.persistentDataPath);
    }
}
