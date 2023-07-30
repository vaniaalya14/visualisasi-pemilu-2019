using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemCollector : MonoBehaviour
{
    int blangko = 0;

    [SerializeField] AudioSource collectionSound;
    [SerializeField] AudioSource victorySound;
    public GameObject portal;
    public Text displayJumlahBlangko;

    public GameObject HiddenTask;
    public GameObject SelamatPrompt;

    void Start()
    {
        portal.SetActive(false);
        HiddenTask.SetActive(true);
        SelamatPrompt.SetActive(false);
    }

    void Update()
    {
        if (blangko == 3)
        {
            portal.SetActive(true);
            SelamatPrompt.SetActive(true);
            HiddenTask.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Blangko"))
        {
            Destroy(other.gameObject);
            blangko++;
            displayJumlahBlangko.text = blangko.ToString();
            if (blangko != 3)
            {
                collectionSound.Play();
            } else
            {
                victorySound.Play();
            }
        }
    }
}