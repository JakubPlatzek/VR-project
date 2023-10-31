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
    Transform parent;

    void Awake(){
        parent = runnerControllerObject.transform.parent;
    }

    void FixedUpdate()
    {
        Vector3 currentState = runnerControllerObject.localEulerAngles;
        //Controller is pulled
        if(interacted){
            runnerControllerObject.GetComponent<Rigidbody>().freezeRotation = false;
        }
        else {
            // StartCoroutine(ReturnControllerToDefaultPosition(runnerControllerObject));
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
    IEnumerator ReturnControllerToDefaultPosition(Transform runnerControllerObject) {
        float duration = 1.0f;
        float elapsedTime = 0.0f;
        Quaternion startRotation = runnerControllerObject.localRotation;
        Quaternion endRotation = Quaternion.Euler(0, 0, 0);

        while (elapsedTime < duration) {
            runnerControllerObject.localRotation = Quaternion.Slerp(startRotation, endRotation, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        runnerControllerObject.localRotation = endRotation;
    }

    public void InteractedTrue(){
        interacted = true;
        // runnerControllerObject.transform.SetParent(parent);
    }

    public void InteractedFalse(){
        interacted = false;
    }
}
