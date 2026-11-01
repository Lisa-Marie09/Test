using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Android;

public class Gyroscope : MonoBehaviour
{
    private AttitudeSensor _sensor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _sensor = InputSystem.GetDevice<AndroidGameRotationVector>();

        if (_sensor == null)
            _sensor = InputSystem.GetDevice<AndroidRotationVector>();

        if (_sensor == null)
        {
            Debug.LogError("No rotation sensor available!");
            return;
        }

        InputSystem.EnableDevice(_sensor);
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion deviceRotation = _sensor.attitude.ReadValue();

        // add 90 degrees x rotation so vase stands upright
        transform.rotation = deviceRotation * Quaternion.Euler(90, 0, 0);
    }
}
