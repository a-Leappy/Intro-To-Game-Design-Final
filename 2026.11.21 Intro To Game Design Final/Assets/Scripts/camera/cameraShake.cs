
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class cameraShake : MonoBehaviour
{
    public float shakeMagnitude = 5f;
    public bool isShaking;
    float time;

    void LateUpdate()
    {
        if (!isShaking) return;

        time -= Time.deltaTime;

        if (time <= 0)
        {
            isShaking = false;
            return;
        }

        transform.eulerAngles += Random.insideUnitSphere * shakeMagnitude;
    }

    public void ShakeCamera(float shakeTime = 3f)
    {
        isShaking = true;
        time = shakeTime;
    }
}
