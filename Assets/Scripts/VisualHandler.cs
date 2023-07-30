using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualHandler : MonoBehaviour
{
    public GameObject colorIndo;
    public GameObject baseIndo;
    public static bool isShown;

    // Start is called before the first frame update
    void Start()
    {
        colorIndo.SetActive(false);
        baseIndo.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (isShown)
            {
                Hide();
            }
            else
            {
                Show();
            }
        }

    }

    public void Show()
    {
        colorIndo.SetActive(true);
        baseIndo.SetActive(false);
        isShown = true;
    }

    public void Hide()
    {
        colorIndo.SetActive(false);
        baseIndo.SetActive(true);
        isShown = false;
    }
}
