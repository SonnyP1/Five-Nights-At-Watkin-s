using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Numerics;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.Windows;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerInputs : MonoBehaviour
{
    [SerializeField] float MouseSense;
    [SerializeField] float MoveOffset;
    [SerializeField] LayerMask ClickableMask;
    PlayerInputAction _playerInputAction;

    private UIManager _uiManager;
    private float percentage;
    private float _inputX;
    private float _inputY;
    private float _midPoint;
    private void OnEnable()
    {

    }
    private void OnDisable()
    {
        _playerInputAction.Disable();
    }
    void Start()
    {
        _playerInputAction = new PlayerInputAction();
        _uiManager = FindObjectOfType<UIManager>();
        _playerInputAction.Enable();
        SetInputs();
    }

    void SetInputs()
    {
        _playerInputAction.Gameplay.Look.performed += UpdateLook;
        //_playerInputAction.Gameplay.Click.performed += OnClick;
    }


    private void UpdateLook(InputAction.CallbackContext obj)
    {
        Vector2 playerMouseInput = obj.ReadValue<Vector2>();
        _inputX = playerMouseInput.x;
        _inputY = playerMouseInput.y;
        _midPoint = Screen.width/2;;
        percentage = _inputX / _midPoint;
    }

    void Update()
    {
        if(!_uiManager.GetInTablet())
        {
            if (_inputX > _midPoint + MoveOffset && transform.rotation.y < 0.15f)
            {
                transform.Rotate(Vector3.up, 1 * MouseSense* (percentage - 1.0f));
            }
            else if (_inputX < _midPoint - MoveOffset && transform.rotation.y > -0.15f)
            {
                transform.Rotate(Vector3.up, -1 * MouseSense* ((percentage-1)*-1));
            }
        }
    }
}
