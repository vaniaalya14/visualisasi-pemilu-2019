using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

public class ReadGoogleSheet : MonoBehaviour
{
    public Text displayWilayah;
    public Text displayJokowi;
    public Text displayPrabowo;
    string[] notes;
    int i = 0;
    int n = 0;
    public int idWilayah = 1;
    string rowsjson="";
    string[] lines;
    List<string> eachrow;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (n < 1)
        {
            takefromCSV();
            n += 1;
        }

    }

    private void OnTriggerEnter(Collider trigger) {
        if(trigger.gameObject.CompareTag("Aceh")) {
            idWilayah = 1;
        }
    }

    public void takefromCSV()
    {
        StartCoroutine(ObtainSheetData());
    }

    IEnumerator ObtainSheetData()
    {
        UnityWebRequest www = UnityWebRequest.Get("https://sheets.googleapis.com/v4/spreadsheets/1ACQiVq7jK785mzYrgAzPoRy792iKxTrgJZBdkI0VkBI/values/Sheet1?key=AIzaSyCD9H60NTaKOKlItN9AbaEiG0ZyxTlE5dk");
        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError || www.timeout>2)
        {
            Debug.Log("error" + www.error);
            Debug.Log("Offline");
        }
        else
        {
            //networkerror.SetActive(false);
            string json = www.downloadHandler.text;
            var o = JSON.Parse(json);
            foreach (var item in o["values"])
            {
                var itemo = JSON.Parse(item.ToString());
                eachrow = itemo[0].AsStringList;
                foreach (var bro in eachrow)
                {
                    rowsjson += bro+",";
                }
                rowsjson += "\n";
            }
            lines = rowsjson.Split(new char[] { '\n' });
            notes = lines[idWilayah].Split(new char[] { ',' });
            displayWilayah.text = notes[0];
            displayJokowi.text = Convert.ToDouble(notes[1].ToString()).ToString("N0");
            displayPrabowo.text = Convert.ToDouble(notes[2].ToString()).ToString("N0");
        }
    }


}
