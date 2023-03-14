using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamChanger : MonoBehaviour
{

    public float newFov = 0;
    private float currnetFov;
    public GameObject player;
    public CinemachineVirtualCamera cam;

    // Start is called before the first frame update
    void Start()
    {
        var m_MainCamera = Camera.main;

        var brain = (m_MainCamera == null) ? null : m_MainCamera.GetComponent<CinemachineBrain>();

        var vcam = (brain == null) ? null : brain.ActiveVirtualCamera as CinemachineVirtualCamera;

        if(vcam != null)
        {
            Debug.Log("CamHere");
            currnetFov = cam.m_Lens.OrthographicSize;
            Debug.Log(currnetFov);
        }
        else
        {
            Debug.Log("No Cam, Sad Time.");
        }
    }

    private void OnTriggerEnter (Collider player)
    {
        Debug.Log("Cam Change");
        currnetFov = newFov;
        Debug.Log(currnetFov);
        cam.m_Lens.OrthographicSize = currnetFov;
    }
}
