using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;
using System;

public class Study : MonoBehaviour
{
    //private String server = "http://wurl.spdns.org:16118";

    private Text txt;
    //private InputField input;

    // dataset
    private Dropdown DatasetSelector;

    // dataset
    private Dropdown AnatomySelector;

    // dataset
    private Dropdown SideSelector;

    // dataset
    private Dropdown GenderSelector;

    // dataset
    private Dropdown EthnicGroupSelector;

    // study
    private Dropdown StudySelector;

    public void Start()
    {
        // find object 'Info' for output
        txt = GameObject.Find("/Canvas/Messages").GetComponent<Text>();

        // set input file to define server
        //input = GameObject.Find("WebServerInputField").GetComponent<InputField>();

        // selectors
        DatasetSelector = GameObject.Find("DatasetSelector").GetComponent<Dropdown>();
        AnatomySelector = GameObject.Find("AnatomySelector").GetComponent<Dropdown>();
        SideSelector = GameObject.Find("SideSelector").GetComponent<Dropdown>();
        GenderSelector = GameObject.Find("GenderSelector").GetComponent<Dropdown>();
        EthnicGroupSelector = GameObject.Find("EthnicGroupSelector").GetComponent<Dropdown>();

        // define listener 'SelectStudy' for dropdown element
        StudySelector = GameObject.Find("StudySelector").GetComponent<Dropdown>();
        StudySelector.onValueChanged.AddListener(delegate { StartStudy(); });
    }

    public void StartStudy()
    {

        if (DatasetSelector.value <= 0)
        {
            // selected dataset is mandatory
            txt.text = "Select Dataset!";
            StudySelector.value = 0;
            return;
        }
        else if (StudySelector.value <= 0)
        {
            // do not start thread if selection is <= 0
            return;
        }
        else
        {
            txt.text = "Please wait ...";
            StartCoroutine(ServerRequest1());
            StudySelector.value = 0; // default/first element does have value 1
        }

    }

    // Update is called once per frame
    void Update()
    {
    }

    // wahrscheinlich am besten BoneDoc/Server spezifisch einen request zu machen ...
    IEnumerator ServerRequest1()
    {
        // debug
        Debug.Log("DEBUG!!!");
        
        //string server = "http://192.168.1.120:61180/bonedoc";

        // send (standard) GET request (to server)
        //UnityWebRequest request = UnityWebRequest.Get("");
        UnityWebRequest request = UnityWebRequest.Get("http://localhost:61180");

        // add header with meta infos server needs for analysis
        request.SetRequestHeader("Dataset", DatasetSelector.captionText.text);
        request.SetRequestHeader("Anatomy", AnatomySelector.captionText.text);
        request.SetRequestHeader("Side", SideSelector.captionText.text);
        request.SetRequestHeader("Gender", GenderSelector.captionText.text);
        request.SetRequestHeader("EthnicGroup", EthnicGroupSelector.captionText.text);
        request.SetRequestHeader("Study", StudySelector.captionText.text);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError)
        {
            // Error 
            Debug.Log(request.error);
            txt.text = request.error;
        }
        else
        {
            // Success
            Debug.Log("Message:");
            Debug.Log(request.downloadHandler.text);
            txt.text = request.downloadHandler.text;

            // Debug: switch ethnic group (just for fun)
            string temp = txt.text.ToString();
            int asianpos = temp.IndexOf("asian");
            int pos2 = temp.IndexOf("%", asianpos) - 7;
            int l = pos2 - asianpos;
            int asianp = 0;
            Int32.TryParse(temp.Substring(asianpos + 7, l), out asianp);

            int caucasianpos = temp.IndexOf("caucasian");
            int pos3 = temp.IndexOf("%", caucasianpos) - 11;
            int l2 = pos3 - caucasianpos;

            int caucasianp = 0;
            Int32.TryParse(temp.Substring(caucasianpos + 11, l2), out caucasianp);

            if (asianp >= 50)
                EthnicGroupSelector.value = 1;
            else if (caucasianp >= 50)
                EthnicGroupSelector.value = 2;

            Debug.Log("End of Message.");
        }

        // explicit garbage collection (best practice do use it manually)
        request.Dispose();
    }

}
