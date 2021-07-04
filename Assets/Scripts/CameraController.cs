using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineComposer composer;
    [SerializeField] private float sensitivity = 1f;

    void Start()
    {
        composer = GetComponent<CinemachineVirtualCamera>().
            GetCinemachineComponent<CinemachineComposer>();
    }

    private void Update()
    {
        float vertical = Input.GetAxis("Mouse Y") * sensitivity;
        composer.m_TrackedObjectOffset.y += vertical;
        composer.m_TrackedObjectOffset.y = Mathf.Clamp(composer.m_TrackedObjectOffset.y, -5, 20);
    }
}
