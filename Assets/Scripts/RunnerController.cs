using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RunnerController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float maxMoveSpeed = 10.0f;
    public float rotationSpeed = 100.0f;
    public float maxRotationSpeed = 20.0f;
    public Transform runnerControllerObject;
    public bool interacted = false;
    public bool allowMovement = true;
    public Rigidbody rigidbody;
    public GameObject player;
    public Transform controllerParent;
    public OrderCheckHandler orderCheckHandler;
    float yValue = 0.0f;

    void Awake(){
        yValue = transform.position.y;
    }
    void FixedUpdate()
    {
        if(Mathf.Abs(transform.position.y) != Mathf.Abs(yValue)){
            transform.position = new Vector3(transform.position.x, yValue, transform.position.z);
        }
        Vector3 currentState = runnerControllerObject.localEulerAngles;
        //Controller is pulled
        if(interacted){
            runnerControllerObject.GetComponent<Rigidbody>().freezeRotation = false;
        }
        else {
            runnerControllerObject.localEulerAngles = Vector3.zero;
            runnerControllerObject.GetComponent<Rigidbody>().freezeRotation = true;
        }
        if(currentState.x != 0.0f && allowMovement){
            float angleX = (currentState.x > 180) ? currentState.x - 360 : currentState.x;
            float angleY = (currentState.y > 180) ? currentState.y - 360 : currentState.y;
            if(angleY > maxRotationSpeed){
                angleY = maxRotationSpeed;
                if(angleY > 60.0f){
                    runnerControllerObject.Rotate(new Vector3(currentState.x, 0, currentState.z), 60.0f);
                }
            }
            if (angleY < -maxRotationSpeed)
            {
                angleY = -maxRotationSpeed;
                if (angleY < -60.0f)
                {
                    runnerControllerObject.Rotate(new Vector3(currentState.x, 0, currentState.z), -60.0f);
                }
            }
            Quaternion rotation = Quaternion.Euler(0, rotationSpeed * Time.fixedDeltaTime * angleY, 0);
            rigidbody.MoveRotation(rigidbody.rotation * rotation);
            if(angleX > maxMoveSpeed){
                angleX = maxMoveSpeed;
                if(angleX > 60.0f){
                    runnerControllerObject.Rotate(new Vector3(0, currentState.y, currentState.z), 60.0f);
                }
            }
            if(angleX < -maxMoveSpeed){
                angleX = -maxMoveSpeed;
                if(angleX < -60.0f){
                    runnerControllerObject.Rotate(new Vector3(0, currentState.y, currentState.z), -60.0f);
                }
            }
            Vector3 velocity = transform.forward * moveSpeed * -angleX;
            rigidbody.velocity = velocity;
        }
    }

    public void InteractedTrue(){
        interacted = true;
        orderCheckHandler.StickBoxes(true);
        runnerControllerObject.transform.SetParent(controllerParent.gameObject.transform);
        player.transform.SetParent(rigidbody.gameObject.transform);
    }

    public void InteractedFalse(){
        interacted = false;
        orderCheckHandler.StickBoxes(false);
        player.transform.SetParent(null);
    }
}
