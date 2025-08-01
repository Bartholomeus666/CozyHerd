using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AssignInput : MonoBehaviour
{
    private GameObject _dog;
    private GameObject[] _gates;

    private PlayerInput _playerInput;

    private bool _assignDog = false;

    private void Start()
    {
        _playerInput = GetComponent<PlayerInput>();

        _dog = GameObject.FindGameObjectWithTag("Dog");

        _gates = GameObject.FindGameObjectsWithTag("Gate");
        Debug.Log($"Found {_gates.Length} goals in the scene.");

        _assignDog = false;
    }

    private void Update()
    {
        if(_assignDog)
        {
            return;
        }
        AssignGate();
        AssignDog();
    }

    private void AssignDog()
    {
        _playerInput.actions.actionMaps[0].FindAction("OrderDog").performed += _dog.GetComponent<Dog_StateMachine>().GetMoveOrder;
        _assignDog = true;
    }
    private void AssignGate()
    {
        foreach(GameObject goal in _gates)
        {
            _playerInput.actions.actionMaps[0].FindAction("Interact").performed += goal.GetComponent<GateWork>().OpenGate;
            goal.GetComponentInChildren<GateWork>().Debugg();
        }
    }
}
