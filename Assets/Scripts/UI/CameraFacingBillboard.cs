using UnityEngine;
using System.Collections;


public class CameraFacingBillboard : MonoBehaviour
{

    public Camera m_Camera;
    public bool amActive = false;
    public bool autoInit = false;


    void Awake()
    {
        if (autoInit == true)
        {
            m_Camera = Camera.main;
            amActive = true;
        }
    }


    void Update()
    {
        if (amActive == true)
        {
            GetComponent<Canvas>().transform.LookAt(GetComponent<Canvas>().transform.position + m_Camera.transform.rotation * Vector3.back, m_Camera.transform.rotation * Vector3.up);
        }
    }
}