using System.Collections.Generic;
using Mono.Data.Sqlite;
using System.Data;
using UnityEngine;
using System.Globalization;
using System.IO;

public class IndoColorChanger : MonoBehaviour
{
    private string dbName = "URI=file:";
    private List<string> color1 = new List<string>(); //prabowo
    private List<string> color2 = new List<string>(); //jokowi

    [SerializeField] Material material1;
    [SerializeField] Material material2;


    // Start is called before the first frame update
    void Start()
    {
        GetData();
        for (int i = 0; i < color1.Count; i++) {
            changeColor(ChildAccessor(color1[i]), material1);
        }

        for (int i = 0; i < color2.Count; i++) {
            changeColor(ChildAccessor(color2[i]), material2);
        }
    }

    public Transform ChildAccessor(string childObjectName) {
        // Change to Title Case
        childObjectName = ToTitleCase(childObjectName.ToLower());
        // Find a child component by name
        Transform childTransform = transform.Find(childObjectName);

        // Check if the child component exists
        if (childTransform != null) {
            return childTransform;
        } else {
            return null;
        }
    }

    public void changeColor(Transform myObject, Material color) {
        Renderer renderer = myObject.GetComponent<Renderer>();
        renderer.material = color;
    }

    public void GetData() {
        int i = 1;
        using var connection = new SqliteConnection(dbName + Path.Combine(Application.persistentDataPath, "data.db"));
        connection.Open();
        using (var command = connection.CreateCommand()) {
            command.CommandText = "SELECT * FROM  \"data_provinsi\" WHERE prabowo > jokowi;";
            using IDataReader reader = command.ExecuteReader();
            while (reader.Read()) {
                //add gameobject
                color1.Add(reader["nama_provinsi"].ToString());

            }
        }

        using (var command = connection.CreateCommand()) {
            command.CommandText = "SELECT * FROM  \"data_provinsi\" WHERE jokowi > prabowo;";
            using IDataReader reader = command.ExecuteReader();
            while (reader.Read()) {
                //add gameobject
                color2.Add(reader["nama_provinsi"].ToString());

            }
        }
        connection.Close();
    }

    public static string ToTitleCase(string input) {
        TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
        return textInfo.ToTitleCase(input);
    }
}
