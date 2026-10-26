using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Gyroscope : MonoBehaviour
{
    public Vector3 rot;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rot = Vector3.zero;
        Input.gyro.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        //rot.y = Input.gyro.rotationRateUnbiased.y;
        Vector3 gyroRate = Input.gyro.rotationRateUnbiased * Mathf.Rad2Deg;
        rot = gyroRate * Time.deltaTime;
        transform.Rotate(rot);
    }
}
