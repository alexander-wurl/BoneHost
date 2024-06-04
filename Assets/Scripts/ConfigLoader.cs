using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;

public class ConfigLoader : MonoBehaviour
{
    //private static string configFilePath;
    private Dictionary<string, string> configValues;
    private string configFilePath;

    void Start()
    {
        // Set the config file path for bonehost
        configFilePath = Path.Combine(Application.dataPath, "bonehost.conf");
        Debug.Log("Path for config file: " + configFilePath);

        // Start loading the config file
        StartCoroutine(LoadConfig());
    }

    IEnumerator LoadConfig()
    {
       
        // variable to define all config keys and values with
        configValues = new Dictionary<string, string>();

        Debug.Log("Load " + configFilePath + " from disc ...");

        // Get File locally
        if (File.Exists(configFilePath))
        {
            string[] lines = File.ReadAllLines(configFilePath);
            foreach (string line in lines)
            {
                if (!string.IsNullOrWhiteSpace(line) && line.Contains("="))
                {
                    string[] keyValue = line.Split('=');
                    if (keyValue.Length == 2)
                    {
                        configValues[keyValue[0].Trim()] = keyValue[1].Trim();
                    }
                }
            }
        }
        // Use UnityWebRequest to load the config file via web request
        else
        {
            Debug.Log("File not found on disc! Trying to get it via UnityWebRequest ...");

            UnityWebRequest request = UnityWebRequest.Get(configFilePath);
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Failed to load " + configFilePath + ": " + request.error);
            }
            else
            {
                string[] lines = request.downloadHandler.text.Split('\n');
                foreach (string line in lines)
                {
                    if (!string.IsNullOrWhiteSpace(line) && line.Contains("="))
                    {
                        string[] keyValue = line.Split('=');
                        if (keyValue.Length == 2)
                        {
                            configValues[keyValue[0].Trim()] = keyValue[1].Trim();
                        }
                    }
                }
            }

        }


    }

    public string GetConfigValue(string key)
    {
        if (configValues.TryGetValue(key, out string value))
        {
            return value;
        }
        else
        {
            Debug.LogError("Key not found in config: " + key);
            return null;
        }
    }

    public Dictionary<string, string> GetConfigValues()
    {
        return configValues;
    }

}
