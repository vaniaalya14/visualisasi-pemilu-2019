using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour
{
    public ReadGoogleSheet logic;
    public int idWilayah;

    void Start()
    {
        /* logic = GameObject.FindGameObjectsWithTag("Logic").GetComponent<ReadGoogleSheet>(); */
    }

    private void OnTriggerEnter(Collider other)
    {
        logic.idWilayah = idWilayah;
        logic.takefromCSV();
    }
}
