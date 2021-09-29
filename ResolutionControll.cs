using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ResolutionControll : MonoBehaviour {

    [Header("AuxPanel Settings")]
    public GameObject auxPanel;

    [Header("Cinema Settings")]
    public CinemachineVirtualCamera cinemaCam;
    public bool usingCinema;

    [Header("Camera Resolution Settings")]
    public Camera cam;
    public bool ortographic;
    public float resolution;
    public float heigth;
    public float width;
    public float[] resolutions;
    public float[] camZoom;

    private void Start()
    {
        SetResolution();
        CheckResolution();
    }

    public void SetResolution()
    {
        heigth = Screen.height;
        width = Screen.width;
        resolution = heigth / width;
    }

    public void CheckResolution()
    {        
        if(resolution > resolutions[0] && resolution < resolutions[1])
        {
            ChangeCamZoom(camZoom[0]);
            return;
        }

        if(resolution > resolutions[1] && resolution < resolutions[2])
        {
            SetUpAuxPanel();
            ChangeCamZoom(camZoom[1]);
            return;
        }

        if(resolution > resolutions[2] && resolution < resolutions[3])
        {
            SetUpAuxPanel();
            ChangeCamZoom(camZoom[2]);
            return;
        }

        if (resolution > resolutions[3])
        {
            SetUpAuxPanel();
            ChangeCamZoom(camZoom[3]);
            return;
        }
    }

    public void ChangeCamZoom(float newZoom)
    {
        if (ortographic)
        {
            cam.orthographicSize = newZoom;
            CheckCinema(newZoom);
        }
        else
        {
            cam.fieldOfView = newZoom;
        }
    }

    public void CheckCinema(float _newSize)
    {
        if (usingCinema)
            cinemaCam.m_Lens.OrthographicSize = _newSize;
    }

    public void SetUpAuxPanel()
    {
        if(auxPanel!=null)
            auxPanel.SetActive(true);
    }
}
