using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class cameraShake : MonoBehaviour
{
    public float shakeMagnitude;
    public bool isShaking;
    Vector3 cameraAngle;
    float time;
    void Update()
    {
        if (isShaking && time <= 0)
        {
            ShakeCamera();
        }
        if (isShaking)
        {
            time -= Time.deltaTime;
            transform.eulerAngles = cameraAngle + (Random.insideUnitSphere * shakeMagnitude);
        }
        if (time <= 0)
        {
            transform.eulerAngles = cameraAngle;
            isShaking = false;
        }
    }
    public void ShakeCamera(float shakeTime = 3f)
    {
        isShaking = true;
        cameraAngle = transform.eulerAngles;
        time = shakeTime;
    }
}
