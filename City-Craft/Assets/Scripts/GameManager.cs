using Gp7;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CameraMovement Movement;
    public Road road;
    public Inputs inputs;

    private void Start()
    {
        inputs.OnMouseClick += road.PlaceRoad;
        inputs.OnMouseHold += road.PlaceRoad;
        inputs.OnMouseUp += road.FinishPlacingRoad;
    }


    private void Update()
    {
        Movement.MoveCamera(new Vector3(inputs.CameraMovementVector.x,0, inputs.CameraMovementVector.y));
    }
}
