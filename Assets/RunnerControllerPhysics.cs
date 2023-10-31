using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RunnerControllerPhysics : MonoBehaviour
{
    Vector3 originalPosition;
    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        transform.localPosition = originalPosition;
    }

    void FixedUpdate(){
        transform.localPosition = originalPosition;
    }
    public void onInteraction(){
        // rb.useGravity = true;
        // rb.isKinematic = true;
        // rb.drag = 1;
        // rb.angularDrag = 0.5f;
        // rb.mass = 10;
    }
}
