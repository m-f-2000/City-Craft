using Gp7;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CameraMovement Movement;

    public Inputs inputs;

    private void Start()
    {
        inputs.OnMouseClick += HandleMouseClick;
    }

    private void HandleMouseClick(Vector3Int position)
    {
        Debug.Log(position);
    }

    private void Update()
    {
        Movement.MoveCamera(new Vector3(inputs.CameraMovementVector.x,0, inputs.CameraMovementVector.y));
    }
}
