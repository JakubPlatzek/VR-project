using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RunnerControllerPhysics : MonoBehaviour
{
    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void onInteraction(){
        rb.useGravity = true;
        rb.isKinematic = true;
        rb.drag = 1;
        rb.angularDrag = 0.5f;
        rb.mass = 10;
    }

    void onTriggerEnter(Collider other){
        print("Trigger enter");
    }
}
