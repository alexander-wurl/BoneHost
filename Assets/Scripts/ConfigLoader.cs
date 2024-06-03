using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class ConfigLoader
{
    // private static string configFileName = @"C:\configuration\bonehost.conf";
    private static string configFileName = "bonehost.conf";
    private static string configFilePath;

    private static string persistentFilePath;

    static ConfigLoader()
    {
        // Set the file path relative to the game's directory
        configFilePath = Path.Combine(Application.dataPath, configFileName);
        Debug.Log("configFilePath: " + configFilePath);
        // Alternatively, use the persistent data path for a platform-independent location
        persistentFilePath = Path.Combine(Application.persistentDataPath, configFileName);
        Debug.Log("persistentFilePath: " + persistentFilePath);
    }

    public static Dictionary<string, string> LoadConfig()
    {
        Dictionary<string, string> configValues = new Dictionary<string, string>();

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
        else
        {
            Debug.LogError("Config file not found: " + configFilePath);
        }

        return configValues;
    }

    public static string GetConfigValue(string key)
    {
        Dictionary<string, string> configValues = LoadConfig();

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
}
