using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Windows;

public class CameraRot : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] float _angle;

    private float xRot;
    private float yRot;
    private float zRot;
    private void Start()
    {
        xRot = transform.eulerAngles.x;
        yRot = transform.eulerAngles.y;
        zRot = transform.eulerAngles.z;
    }
    void Update()
    {
        transform.rotation = Quaternion.Euler(xRot, yRot + Mathf.Sin(Time.realtimeSinceStartup * _speed) * _angle, zRot);
    }
}
