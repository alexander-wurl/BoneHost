using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;


public class SystemInfo : MonoBehaviour
{
    private Text txt;
    private Button SystemInfoButton;

    // Use this for initialization
    void Start()
    {
        // find Button 'SystemInfo' and attach method to onClick listener
        SystemInfoButton = GameObject.Find("SystemInfo").GetComponent<Button>();

        SystemInfoButton.GetComponent<Button>().onClick.AddListener(delegate { GetSystemInfo(); });

        // find object 'Info' for output
        txt = GameObject.Find("/Canvas/Info").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
		
	}

    public void GetSystemInfo()
    {
        txt.text = "Application.dataPath: " + Application.dataPath + "\n" + "Application.streamingAssetsPath: " + Application.streamingAssetsPath + "\n" +
            "Environment.OSVersion.Platform: " + Environment.OSVersion.Platform + "\n" +
                "Application.persistentDataPath: " + Application.persistentDataPath + "\n" +
                    "System.AppDomain.CurrentDomain.BaseDirectory: " + System.AppDomain.CurrentDomain.BaseDirectory + "\n" +
                        "Directory.GetCurrentDirectory(): " + Directory.GetCurrentDirectory() + "\n" +
                            "Application.ExecutablePath(): " + Application.absoluteURL;

        // StartCoroutine(CheckIP("http://checkip.dyndns.org"));

        // StartCoroutine(GetText());

    }

    //IEnumerator GetText()
    //{
    //    using (UnityWebRequest www = UnityWebRequest.Get("http://checkip.dyndns.org"))
    //    {
    //        yield return www.Send();

    //        if (www.isNetworkError || www.isHttpError)
    //        {
    //            txt.text += "\n" + www.error;
    //        }
    //        else
    //        {
    //            // Show results as text
    //            txt.text += "\n" + www.downloadHandler.text;

    //            // Or retrieve results as binary data
    //            byte[] results = www.downloadHandler.data;
    //            txt.text += "\n" + results;
    //        }
    //    }
    //}

    //IEnumerator CheckIP(string url)
    //{

    //    Dictionary<string, string> headers = new Dictionary<string, string>();

    //    headers.Add("Access-Control-Allow-Credentials", "true");
    //    headers.Add("Access-Control-Allow-Headers", "Accept, X-Access-Token, X-Application-Name, X-Request-Sent-Time");
    //    headers.Add("Access-Control-Allow-Methods", "GET, POST, OPTIONS");
    //    headers.Add("Access-Control-Allow-Origin", "*");
        

        

    //    txt.text += "\n" + "CheckIP ...";

    //    WWW myExtIPWWW = new WWW(url, null, headers);
    //    //WWW myExtIPWWW = new WWW(url);

    //    yield return myExtIPWWW;

    //    if (myExtIPWWW != null)
    //    {        
    //        txt.text += "\n" + myExtIPWWW;

    //        string myExtIP = myExtIPWWW.text.ToString();

    //        myExtIP = myExtIP.Substring(myExtIP.IndexOf(":") + 1);

    //        myExtIP = myExtIP.Substring(0, myExtIP.IndexOf("<"));

    //        txt.text += "\n" + "Public IP: " + myExtIP;
    //    }
    //    else
    //        txt.text += "\n" + "Error checking IP!";
    //}

}


