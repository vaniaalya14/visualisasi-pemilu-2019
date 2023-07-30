// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using System.IO;
// using UnityEngine.UI;

// public class PrintJSON : MonoBehaviour
// {
//     public TextAsset json;
//     public Text daerahOutput;
//     public Text jokowiOutput;
//     public Text prabowoOutput;
//     // Start is called before the first frame update
//     void Start()
//     {
//         // Debug.Log(json);       
//         // Debug.Log(JSONReader.GetJSON(json));
//         Debug.Log(JSONReader.GetJSON({"daerah": "ACEH", "vote_jokowi": 123, "vote_prabowo": 123, "kategori": "PROVINSI"}));
//     }

//     // Update is called once per frame
//     void Update()
//     {
        
//     }
// }

// public static class JSONReader 
// {
//     public static JSONExample GetJSON(TextAsset json) {
//         JSONExample example = JsonUtility.FromJson<JSONExample>(json.text);
//         Debug.Log(example);
//         return example;
//     }

// }
// [System.Serializable]
// public class JSONExample {
//     public string daerah;
//     public int vote_jokowi;
//     public int vote_prabowo;
//     public string kategori;
// }