using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    private float rotationSpeed = -500f; // Adjust speed as needed
    private Camera mainCamera;
    private GameObject selectedObject;
    private bool isRotating = false;
    private int number = Int32.MaxValue - 1;
    [SerializeField] private GameManager _gameManager;

    public int Number
    {
        get => number;
        set => number = value;
    }

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        // Right mouse button press
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (!hit.transform.CompareTag("Untagged")) // Check if the object's tag is not "Untagged"
                {
                    selectedObject = hit.transform.gameObject;
                    isRotating = true;
                }
            }
        }

        // Right mouse button release
        if (Input.GetMouseButtonUp(1))
        {
            isRotating = false;
            selectedObject = null;
        }

        // Rotate the object if isRotating is true
        if (isRotating && selectedObject != null && Input.GetMouseButton(1))
        {
            float rotationY = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            selectedObject.transform.Rotate(0, rotationY, 0, Space.World);
        }

        // Left mouse button click
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (!hit.transform.CompareTag("Untagged")) // Check if the object's tag is not "Untagged"
                {
                    OnObjectClicked(hit.transform.gameObject);
                }
            }
        }
    }

    private void OnObjectClicked(GameObject obj)
    {
        
        int.TryParse(obj.tag, out number); // Try parsing the tag to an int, if possible

        _gameManager.CheckSelected();
        
    }
    
}
