using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Android;

public class PlayerControl : MonoBehaviour
{   
    //Rotation
    private AttitudeSensor _attitudeSensor;
    private float _initialYAngle = 0f;
    private float _appliedGyroYAngle = 0f;
    private float _calibrationYAngle = 0f;
    private Transform _rawGyroRotation;
    private float _tempSmoothing;
    
    //Settings
    private float _smoothing = 0.05f;

    private void Update()
    {
        /*Vector3 linearAccel = Input.gyro.userAcceleration;

        Vector3 move = new Vector3(linearAccel.x * speed * Time.deltaTime, 0, linearAccel.z * speed * Time.deltaTime);
        Vector3 rotMovement = transform.TransformDirection(move);
        controller.Move(rotMovement);*/

        ApplyGyroRotation();
        ApplyCalibration();

        transform.rotation = Quaternion.Slerp(transform.rotation, _rawGyroRotation.rotation, _smoothing);
    }

    private IEnumerator Start()
    {
        _attitudeSensor = InputSystem.GetDevice<AndroidGameRotationVector>();

        if (_attitudeSensor == null)
            _attitudeSensor = InputSystem.GetDevice<AndroidRotationVector>();

        if (_attitudeSensor == null)
        {
            Debug.LogError("No rotation sensor available!");
            yield break;
        }

        InputSystem.EnableDevice(_attitudeSensor);
        Application.targetFrameRate = 60;
        _initialYAngle = transform.eulerAngles.y;

        _rawGyroRotation = new GameObject("GyroRaw").transform;
        _rawGyroRotation.position = transform.position;
        _rawGyroRotation.rotation = transform.rotation;
        yield return new WaitForSeconds(1);
        StartCoroutine(CalibrateYAngle());
    }

    private IEnumerator CalibrateYAngle()
    {
        _tempSmoothing = _smoothing;
        _smoothing = 1;
        _calibrationYAngle = _appliedGyroYAngle - _initialYAngle;
        yield return null;
        _smoothing = _tempSmoothing;
    }

    private void ApplyGyroRotation()
    {
        if (_attitudeSensor == null || !_attitudeSensor.enabled)
        {
            return;
        }
        _rawGyroRotation.rotation = _attitudeSensor.attitude.ReadValue();
        _rawGyroRotation.Rotate(0f, 0f, 180f, Space.Self);
        _rawGyroRotation.Rotate(90f, 180f, 0f, Space.World);
        _appliedGyroYAngle = _rawGyroRotation.eulerAngles.y;
    }

    private void ApplyCalibration()
    {
        _rawGyroRotation.Rotate(0f, -_calibrationYAngle, 0f, Space.World);
    }
}
