using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputModule : MonoBehaviour
{
    private IMovementModule _movementModule;

    private void Start()
    {
        _movementModule = GetComponent<MovementModule>();
    }
    private void Update()
    {
        MovementInput();
    }

    private void MovementInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MoveToCursor();
        }
    }

    private void MoveToCursor()
    {
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool hasHit = Physics.Raycast(ray, out RaycastHit hit);

        if (hasHit)
            _movementModule.Move(hit.point);
        
    }
}
