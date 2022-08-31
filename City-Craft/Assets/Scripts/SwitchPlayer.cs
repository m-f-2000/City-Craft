using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPlayer : MonoBehaviour
{
    public GameObject Builder;
    public GameObject Player;

    public AudioListener BuilderLis;
    public AudioListener PlayerLis;

    private void Start()
    {
        cameraPositionChange(PlayerPrefs.GetInt("CameraPosition"));
    }

    private void Update()
    {
            SwitchCamera();
    }

    private void SwitchCamera()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            cameraChangeCounter();
        }
    }

    private void cameraPositionChange(int camPosition)
    {
        if (camPosition > 1)
        {
            camPosition = 0;
        }

        PlayerPrefs.SetInt("CameraPosition", camPosition);

        if (camPosition == 0)
        {
            Builder.SetActive(true);
            BuilderLis.enabled = true;

            Player.SetActive(false);
            PlayerLis.enabled = false;

            Cursor.visible = true;
        }

        if (camPosition == 1)
        {
            Player.SetActive(true);
            PlayerLis.enabled = true;

            Builder.SetActive(false);
            BuilderLis.enabled = false;
            
            Cursor.visible = false;
        }
    }

    public void cameraChangeCounter()
    {
        int cameraPositionCounter = PlayerPrefs.GetInt("CameraPosition");
        cameraPositionCounter++;
        cameraPositionChange(cameraPositionCounter);
    }
    


}
