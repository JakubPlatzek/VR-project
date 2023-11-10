using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderCheckHandler : MonoBehaviour
{
    BoxCollider boxCollider;
    public List<GameObject> boxes = new List<GameObject>();

    public WristUI wristUI;

    void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        foreach (var order in wristUI.orderPositions) {
            if (other.gameObject.CompareTag(order.Key)) {
                wristUI.RefreshOrder(other.gameObject.tag, -1);
                boxes.Add(other.gameObject);
                return;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {

        foreach (var order in wristUI.orderPositions) {
            if (other.gameObject.CompareTag(order.Key)) {
                boxes.Remove(other.gameObject);
                wristUI.RefreshOrder(other.gameObject.tag, 1);
                return;
            }
        }
    }
}
