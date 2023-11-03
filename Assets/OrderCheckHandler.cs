using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderCheckHandler : MonoBehaviour
{
    BoxCollider boxCollider;
    public List<GameObject> boxes = new List<GameObject>();

    void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        boxes.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        boxes.Remove(other.gameObject);
    }
}
