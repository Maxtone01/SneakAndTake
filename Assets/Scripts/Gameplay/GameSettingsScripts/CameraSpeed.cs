using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSpeed : MonoBehaviour
{
    public CinemachineFreeLook cmFreeLook;

    void Start()
    {
        cmFreeLook = GetComponent<CinemachineFreeLook>();

        //cmFreeLook.m_XAxis.m_MaxSpeed = GameManager.Instance.defaultCamSpeed;
        //cmFreeLook.m_YAxis.m_MaxSpeed = GameManager.Instance.defaultCamSpeed;
    }
}
