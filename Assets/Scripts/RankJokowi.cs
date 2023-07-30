using System.Collections.Generic;
using System.IO;
using Mono.Data.Sqlite;
using System.Data;
using UnityEngine;
using TMPro;
using System.Linq;
using System;

public class RankJokowi : MonoBehaviour
{
    private string dbName = "URI=file:";
    [SerializeField] float maxHeight = 10f; // Maximum height for the cube

    [SerializeField] GameObject barRank1;
    [SerializeField] GameObject barRank2;
    [SerializeField] GameObject barRank3;
    [SerializeField] GameObject barRank4;
    [SerializeField] GameObject barRank5;

    [SerializeField] TextMeshPro LabelRank1;
    [SerializeField] TextMeshPro LabelRank2;
    [SerializeField] TextMeshPro LabelRank3;
    [SerializeField] TextMeshPro LabelRank4;
    [SerializeField] TextMeshPro LabelRank5;

    [SerializeField] TextMeshPro DaerahRank1;
    [SerializeField] TextMeshPro DaerahRank2;
    [SerializeField] TextMeshPro DaerahRank3;
    [SerializeField] TextMeshPro DaerahRank4;
    [SerializeField] TextMeshPro DaerahRank5;

    IDictionary<string, float> ranking = new Dictionary<string, float>();

    private object result_jokowi;
    private object result_prabowo;


    private void Start()
    {
        GetData();
        // Manipulate the cube height with random data (replace with your data logic)
        float totalData = ranking.ElementAt(0).Value + ranking.ElementAt(1).Value + ranking.ElementAt(2).Value + ranking.ElementAt(3).Value + ranking.ElementAt(4).Value;
        SetCubeHeight(barRank1, (ranking.ElementAt(0).Value / totalData) * maxHeight);
        SetCubeHeight(barRank2, (ranking.ElementAt(1).Value / totalData) * maxHeight);
        SetCubeHeight(barRank3, (ranking.ElementAt(2).Value / totalData) * maxHeight);
        SetCubeHeight(barRank4, (ranking.ElementAt(3).Value / totalData) * maxHeight);
        SetCubeHeight(barRank5, (ranking.ElementAt(4).Value / totalData) * maxHeight);

        // Update the label - Angka
        UpdateLabel(barRank1, ranking.ElementAt(0).Value, LabelRank1);
        UpdateLabel(barRank2, ranking.ElementAt(1).Value, LabelRank2);
        UpdateLabel(barRank3, ranking.ElementAt(2).Value, LabelRank3);
        UpdateLabel(barRank4, ranking.ElementAt(3).Value, LabelRank4);
        UpdateLabel(barRank5, ranking.ElementAt(4).Value, LabelRank5);


        // Update the Label - Nama Daerah
        DaerahRank1.text = ranking.ElementAt(0).Key.ToString();
        DaerahRank2.text = ranking.ElementAt(1).Key.ToString();
        DaerahRank3.text = ranking.ElementAt(2).Key.ToString();
        DaerahRank4.text = ranking.ElementAt(3).Key.ToString();
        DaerahRank5.text = ranking.ElementAt(4).Key.ToString();
    }

    private void SetCubeHeight(GameObject barManipulated, float dataValue)
    {
        // Clamp the data value to a positive range
        float clampedValue = Mathf.Clamp(dataValue, 0f, maxHeight);
        // Set the Y scale of the cube's Transform component to adjust its height
        barManipulated.transform.localScale = new Vector3(1f, clampedValue, 1f);
    }

    private void UpdateLabel(GameObject parent, object jumlah, TextMeshPro label)
    {
        label.transform.position = new Vector3(parent.transform.position.x, (parent.transform.position.y + parent.transform.localScale.y + 0.5f), parent.transform.position.z);
        label.text = Convert.ToDouble(jumlah).ToString("N0");
    }

    public void GetData()
    {
        int i = 1;
        using var connection = new SqliteConnection(dbName + Path.Combine(Application.persistentDataPath, "data.db"));
        connection.Open();
        using (var command = connection.CreateCommand()) {
            command.CommandText = "SELECT * FROM  \"data_provinsi\" WHERE jokowi > prabowo ORDER BY \"jokowi\" DESC;";
            using IDataReader reader = command.ExecuteReader();
            while (reader.Read()) {
                //add gameobject
                ranking.Add(reader["nama_provinsi"].ToString(), float.Parse(reader["jokowi"].ToString()));

            }
        }

        using (var command = connection.CreateCommand())
        {
            command.CommandText = "SELECT SUM(jokowi) FROM data_provinsi;";
            result_jokowi = command.ExecuteScalar();
            command.CommandText = "SELECT SUM(prabowo) FROM data_provinsi;";
            result_prabowo = command.ExecuteScalar();
        }
        connection.Close();
    }
}
