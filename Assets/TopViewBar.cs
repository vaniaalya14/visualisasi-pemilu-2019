using System.Collections;
using System.Collections.Generic;
using Mono.Data.Sqlite;
using System.Data;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.IO;

public class TopViewBar : MonoBehaviour
{
    private string dbName = "URI=file:";
    [SerializeField] float maxHeight = 10f; // Maximum height for the cube

    [SerializeField] GameObject barJokowi;
    [SerializeField] GameObject barPrabowo;
    [SerializeField] TextMeshPro LabelJokowi;
    [SerializeField] TextMeshPro LabelPrabowo;
    private object result_jokowi;
    private object result_prabowo;


    private void Start()
    {
        GetData();
        // Manipulate the cube height with random data (replace with your data logic)
        float totalData = float.Parse(result_jokowi.ToString()) + float.Parse(result_prabowo.ToString());
        /*float randomData = Random.Range(0f, maxHeight);*/
        Debug.Log(float.Parse(result_jokowi.ToString()) / totalData);
        SetCubeHeight(barJokowi, (float.Parse(result_jokowi.ToString()) / totalData) * maxHeight);
        SetCubeHeight(barPrabowo, (float.Parse(result_prabowo.ToString()) / totalData) * maxHeight);

        // Update the label
        LabelJokowi.text = Convert.ToDouble(result_jokowi).ToString("N0");
        LabelPrabowo.text = Convert.ToDouble(result_prabowo).ToString("N0");
    }

    private void SetCubeHeight(GameObject barManipulated, float dataValue)
    {
        // Clamp the data value to a positive range
        float clampedValue = Mathf.Clamp(dataValue, 0f, maxHeight);
        Debug.Log(clampedValue);

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
        using var connection = new SqliteConnection(dbName + Path.Combine(Application.persistentDataPath, "data.db"));
        connection.Open();
        using (var command = connection.CreateCommand())
        {
            command.CommandText = "SELECT SUM(jokowi) FROM data_provinsi;";
            //using IDataReader reader = command.ExecuteReader();
            result_jokowi = command.ExecuteScalar();

            Debug.Log("Total Jokowi Result: " + result_jokowi.ToString());

            command.CommandText = "SELECT SUM(prabowo) FROM data_provinsi;";
            result_prabowo = command.ExecuteScalar();
            Debug.Log("Total Prabowo Result: " + result_prabowo.ToString());
        }
        connection.Close();
    }
}
