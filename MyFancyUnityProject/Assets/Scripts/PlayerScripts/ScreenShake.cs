using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{

    public static ScreenShake Instance;

    private CinemachineVirtualCamera _cinemachineVirtualCamera;

    private float shakeTime;

    private void Awake()
    {
        Instance = this;
        _cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }
    
    public void ShakeCamera(float shakeIntensity, float time){
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = shakeIntensity;
        shakeTime = time;
    }
// Update is called once per frame
    void Update()
    {
        if (shakeTime > 0)
        {
            shakeTime -= Time.deltaTime;
        }

        if (shakeTime <= 0)
        {
            CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
                _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0.0f;
        }

    }

}
