using City.AI;
using Gp7;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class GameManager : MonoBehaviour
{
    public CameraMovement cameraMovement;
    public Road road;
    public Inputs inputs;

    public UI ui;

    public Structure structure;

    public ObjectDetector objectDetector;

    public PathVisualizer pathVisualizer;

    void Start()
    {
        ui.OnRoadPlacement += RoadPlacementHandler;
        ui.OnHousePlacement += HousePlacementHandler;
        ui.OnSpecialPlacement += SpecialPlacementHandler;
        ui.OnBigStructurePlacement += BigStructurePlacement;
        inputs.OnEscape += HandleEscape;
    }

    private void HandleEscape()
    {
        ClearInputActions();
        ui.ResetButtonColor();
        pathVisualizer.ResetPath();
        inputs.OnMouseClick += TrySelectingAgent;
    }

    private void TrySelectingAgent(Ray ray)
    {
        GameObject hitObject = objectDetector.RaycastAll(ray);
        if(hitObject != null)
        {
            var agentScript = hitObject.GetComponent<AiAgent>();
            agentScript?.ShowPath();
        }
    }

    private void BigStructurePlacement()
    {
        ClearInputActions();

        inputs.OnMouseClick += (pos) =>
        {
            ProcessInputAndCall(structure.PlaceBigStructure, pos);
        };
        inputs.OnEscape += HandleEscape;
    }

    private void SpecialPlacementHandler()
    {
        ClearInputActions();

        inputs.OnMouseClick += (pos) =>
        {
            ProcessInputAndCall(structure.PlaceSpecial, pos);
        };
        inputs.OnEscape += HandleEscape;
    }

    private void HousePlacementHandler()
    {
        ClearInputActions();

        inputs.OnMouseClick += (pos) =>
        {
            ProcessInputAndCall(structure.PlaceHouse, pos);
        };
        inputs.OnEscape += HandleEscape;
    }

    private void RoadPlacementHandler()
    {
        ClearInputActions();

        inputs.OnMouseClick += (pos) =>
        {
            ProcessInputAndCall(road.PlaceRoad, pos);
        };
        inputs.OnMouseUp += road.FinishPlacingRoad;
        inputs.OnMouseHold += (pos) =>
        {
            ProcessInputAndCall(road.PlaceRoad, pos);
        };
        inputs.OnEscape += HandleEscape;
    }

    private void ClearInputActions()
    {
        inputs.ClearEvents();
    }

    private void ProcessInputAndCall(Action<Vector3Int> callback, Ray ray)
    {
        Vector3Int? result = objectDetector.RaycastGround(ray);
        if (result.HasValue)
            callback.Invoke(result.Value);
    }



    private void Update()
    {
        cameraMovement.MoveCamera(new Vector3(inputs.CameraMovementVector.x, 0, inputs.CameraMovementVector.y));
    }
}
