using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderCheckHandler : MonoBehaviour
{
    public List<GameObject> orderBoxes = new List<GameObject>();
    public List<GameObject> boxes = new List<GameObject>();

    public WristUI wristUI;

    private void OnTriggerEnter(Collider other)
    {
        boxes.Add(other.gameObject);
        foreach (var order in wristUI.orderPositions) {
            if (other.gameObject.CompareTag(order.Key)) {
                wristUI.RefreshOrder(other.gameObject.tag, -1);
                orderBoxes.Add(other.gameObject);
                return;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        boxes.Remove(other.gameObject);
        foreach (var order in wristUI.orderPositions) {
            if (other.gameObject.CompareTag(order.Key)) {
                orderBoxes.Remove(other.gameObject);
                wristUI.RefreshOrder(other.gameObject.tag, 1);
                return;
            }
        }
    }

    public void StickBoxes(bool status){
        if(boxes.Count > 0){
            foreach (var box in boxes) {
                box.transform.SetParent(status ? transform.parent : null);
                box.GetComponent<boxManager>().stick = status;
                box.GetComponent<boxManager>().positionToStickTo = box.transform.localPosition;
            }
        }
    }
}
