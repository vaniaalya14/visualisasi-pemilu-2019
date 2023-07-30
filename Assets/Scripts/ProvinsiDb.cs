using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using UnityEngine.UI;
using TMPro;
using System;
using System.IO;

public class ProvinsiDb : MonoBehaviour
{
    private string dbName = "URI=file:";
    public GameObject scrollBar;
    public ScrollRect scroll;
    public Font font;
    public string type;
    public string namaProvinsi;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("starting connection to DB...");
        GetKabupatenKotaByProvince(namaProvinsi, scrollBar);
    }

    public void GetKabupatenKotaByProvince(string namaProvinsi, GameObject scrollBar)
    {
        int i = 1;
        using var connection = new SqliteConnection(dbName + Path.Combine(Application.persistentDataPath, "data.db"));
        connection.Open();
        using (var command = connection.CreateCommand())
        {
            command.CommandText = "SELECT * FROM jumlah_suara WHERE nama_provinsi = '" + namaProvinsi + "';";
            using IDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                 //add gameobject
                GameObject rect = new GameObject("List Daerah" + i);

                rect.transform.SetParent(scrollBar.transform);

                rect.AddComponent<Image>();

                rect.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(265, 50);

                rect.GetComponent<Image>().color = new Color32(255, 255, 0, 255);

                rect.GetComponent<Image>().rectTransform.localRotation = Quaternion.Euler(0, 0, 0);

                rect.GetComponent<Image>().rectTransform.localScale = new Vector3(1, 1, 1);

                var otherPos = rect.GetComponent<Image>().transform.position;

                rect.GetComponent<Image>().transform.localPosition = new Vector3(otherPos.x, otherPos.y, 0);

                addKabupatenObject(rect, i, reader);
                addVoteObject(rect, i, reader, "jokowi", true);
                addVoteObject(rect, i, reader, "prabowo", false);

                i++;
            }

            scroll.normalizedPosition = new Vector2(0, 1);
        }
    }

    public void addKabupatenObject(GameObject rect, int i, IDataReader reader)
    {
        GameObject kabupaten = new GameObject("kabupaten-" + i);

        // text for kabupaten
        kabupaten.AddComponent<TextMeshProUGUI>();
        kabupaten.transform.SetParent(rect.transform);

        kabupaten.GetComponent<TextMeshProUGUI>().rectTransform.localRotation = Quaternion.Euler(0, 0, 0);

        kabupaten.GetComponent<TextMeshProUGUI>().rectTransform.anchorMin = new Vector2(0, 0.5f);
        kabupaten.GetComponent<TextMeshProUGUI>().rectTransform.anchorMax = new Vector2(0, 0.5f);
        kabupaten.GetComponent<TextMeshProUGUI>().rectTransform.pivot = new Vector2(0, 0);

        kabupaten.GetComponent<TextMeshProUGUI>().rectTransform.localScale = new Vector3(0.8f, 0.8f, 0.8f);

        Vector2 sizeDelta = kabupaten.GetComponent<TextMeshProUGUI>().rectTransform.sizeDelta;
        sizeDelta.x = 100; // set width
        kabupaten.GetComponent<TextMeshProUGUI>().rectTransform.sizeDelta = sizeDelta;

        kabupaten.GetComponent<TextMeshProUGUI>().transform.localPosition = new Vector3(-125, -20, 0);
        kabupaten.GetComponent<TextMeshProUGUI>().text = reader["nama_kabupaten"].ToString();

        kabupaten.GetComponent<TextMeshProUGUI>().fontSize = 14;
        kabupaten.GetComponent<TextMeshProUGUI>().color = Color.black;
        kabupaten.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.MidlineLeft;
        kabupaten.GetComponent<TextMeshProUGUI>().horizontalMapping = TextureMappingOptions.Line;
        kabupaten.GetComponent<TextMeshProUGUI>().verticalMapping = TextureMappingOptions.Line;
    }

    public void addVoteObject(GameObject rect, int i, IDataReader reader, string name, bool isLeft)
    {
        GameObject voteObj = new GameObject("vote-" + i);

        // text for kabupaten
        voteObj.AddComponent<TextMeshProUGUI>();
        voteObj.transform.SetParent(rect.transform);

        voteObj.GetComponent<TextMeshProUGUI>().rectTransform.localRotation = Quaternion.Euler(0, 0, 0);

        voteObj.GetComponent<TextMeshProUGUI>().rectTransform.anchorMin = new Vector2(0, 0.5f);
        voteObj.GetComponent<TextMeshProUGUI>().rectTransform.anchorMax = new Vector2(0, 0.5f);
        voteObj.GetComponent<TextMeshProUGUI>().rectTransform.pivot = new Vector2(0, 0);

        voteObj.GetComponent<TextMeshProUGUI>().rectTransform.localScale = new Vector3(0.8f, 0.8f, 0.8f);

        Vector2 sizeDelta = voteObj.GetComponent<TextMeshProUGUI>().rectTransform.sizeDelta;
        sizeDelta.x = 100;
        voteObj.GetComponent<TextMeshProUGUI>().rectTransform.sizeDelta = sizeDelta;

        int positionX;
        if(isLeft == true)
        {
            positionX = -5;
        } else
        {
            positionX = 75;
        }
        voteObj.GetComponent<TextMeshProUGUI>().transform.localPosition = new Vector3(positionX, -20, 0);
        voteObj.GetComponent<TextMeshProUGUI>().text = Convert.ToDouble(reader[name]).ToString("N0");

        voteObj.GetComponent<TextMeshProUGUI>().fontSize = 14;
        voteObj.GetComponent<TextMeshProUGUI>().color = Color.black;
        voteObj.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.MidlineLeft;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
