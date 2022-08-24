using Gp7;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CameraMovement cameraMovement;
    public Road road;
    public Inputs input;
    public UI ui;
    public Structure structure;

    private void Start()
    {
        ui.OnRoadPlacement += RoadPlacementHandler;
        ui.OnHousePlacement += HousePlacementHandler;
        ui.OnSpecialPlacement += SpecialPlacementHandler;
        ui.OnBigStructurePlacement += BigStructurePlacementHandler;

    }

    private void BigStructurePlacementHandler()
    {
        ClearInputActions();
        input.OnMouseClick += structure.PlaceBigStructure;
    }

    private void SpecialPlacementHandler()
    {
        ClearInputActions();
        input.OnMouseClick += structure.PlaceSpecial;
    }

    private void HousePlacementHandler()
    {
        ClearInputActions();
        input.OnMouseClick += structure.PlaceHouse;
    }

    private void RoadPlacementHandler()
    {
        ClearInputActions();

        input.OnMouseClick += road.PlaceRoad;
        input.OnMouseHold += road.PlaceRoad;
        input.OnMouseUp += road.FinishPlacingRoad;
    }

    private void ClearInputActions()
    {
        input.OnMouseClick = null;
        input.OnMouseHold = null;
        input.OnMouseUp = null;
    }

    private void Update()
    {
        cameraMovement.MoveCamera(new Vector3(input.CameraMovementVector.x, 0, input.CameraMovementVector.y));
    }
}