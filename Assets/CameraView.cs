using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraView : MonoBehaviour
{
    public Camera camera1;
    public Camera camera2;
    public InputActionProperty showButton;
    [SerializeField] Canvas AllCanvas;

    private bool isCamera1Active = true;

    private void Start()
    {
        // Enable the first camera at the beginning
        camera1.gameObject.SetActive(true);
        camera2.gameObject.SetActive(false);
        AllCanvas.gameObject.SetActive(true);
    }

    private void Update()
    {
        // Switch cameras on button press
        if (showButton.action.WasPressedThisFrame())
        {
            isCamera1Active = !isCamera1Active;

            // Enable or disable cameras based on the active state
            camera1.gameObject.SetActive(isCamera1Active);
            camera2.gameObject.SetActive(!isCamera1Active);
            AllCanvas.gameObject.SetActive(isCamera1Active);
        }
    }
}
