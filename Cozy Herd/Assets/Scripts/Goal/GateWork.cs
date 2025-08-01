using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GateWork : MonoBehaviour
{
    public GameObject GateL;
    public GameObject GateR;

    Vector3 initialPositionL;
    Vector3 initialPositionR;

    private bool isGateOpen = false;
    private bool isGateClosed = true;

    private bool isGateOpening = false;
    private bool isGateClosing = false;

    private void Start()
    {
        initialPositionR = GateR.transform.position;
        initialPositionL = GateL.transform.position;
    }

    public void OpenGate(InputAction.CallbackContext context)
    {
        Debug.Log("OpenGate called");

        Collider[] colliders = Physics.OverlapSphere(transform.position, 5f);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                if (isGateClosed)
                {
                    isGateOpening = true;
                }
                else if (isGateOpen)
                {
                    isGateClosing = true;
                }
                else
                {
                    
                }
            }
        }
    }

    private void Update()
    {
        if (isGateOpening)
        {
            PlayGateOpenAnimation();
        }
        else if (isGateClosing)
        {
            PlayGateCloseAnimation();
        }
        else if (isGateOpen || isGateClosed)
        {
        }
    }

    private void PlayGateOpenAnimation()
    {

        GateL.transform.position = Vector3.Lerp(GateL.transform.position, initialPositionL + transform.right * 4f, Time.deltaTime * 2f);
        GateR.transform.position = Vector3.Lerp(GateR.transform.position, initialPositionR + transform.right * -4f, Time.deltaTime * 2f);

        if (Vector3.Distance(GateL.transform.position, initialPositionL + transform.right * 4f) < 0.1f &&
            Vector3.Distance(GateR.transform.position, initialPositionR + transform.right * -4f) < 0.1f)
        {
            isGateOpen = true;
            isGateClosed = false;
            isGateOpening = false;
        }
    }

    private void PlayGateCloseAnimation()
    {
        GateL.transform.position = Vector3.Lerp(GateL.transform.position, initialPositionL, Time.deltaTime * 2f);
        GateR.transform.position = Vector3.Lerp(GateR.transform.position, initialPositionR, Time.deltaTime * 2f);

        if (Vector3.Distance(GateL.transform.position, initialPositionL) < 0.1f &&
            Vector3.Distance(GateR.transform.position, initialPositionR) < 0.1f)
        {
            isGateClosed = true;
            isGateOpen = false;
            isGateClosing = false;
        }
    }
}
