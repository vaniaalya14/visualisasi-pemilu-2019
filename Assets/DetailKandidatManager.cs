using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DetailKandidatManager : MonoBehaviour
{
    public Transform head;
    [SerializeField] float spawnDistance = 7f;
    public GameObject detailKandidat;
    public InputActionProperty showButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (showButton.action.WasPressedThisFrame()) {
            detailKandidat.SetActive(!detailKandidat.activeSelf);

            detailKandidat.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * spawnDistance;
        }

        detailKandidat.transform.LookAt(new Vector3(head.position.x, detailKandidat.transform.position.y, head.position.z));
        detailKandidat.transform.forward *= -1;
    }
}
