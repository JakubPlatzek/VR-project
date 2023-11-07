using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PickingPositionHandler : MonoBehaviour
{

    public BoxCollider triggerCollider;
    public List<BoxCollider> boxColliders = new List<BoxCollider>();
    public int maxAmountOfBoxes = 10;
    public List<GameObject> boxes = new List<GameObject>();
    
    void Awake() 
    {
        RefillPosition();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(this.gameObject.tag))
        {
            boxes.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(this.gameObject.tag))
        {
            boxes.Remove(other.gameObject);
        }
    }

    public void RefillPosition()
    {
        foreach (var box in boxColliders) {
            box.isTrigger = true;
        }
        EmptyPosition();
        for (var i = 0; i < maxAmountOfBoxes; i++) {
            Vector3 pos = new Vector3(Random.Range(triggerCollider.bounds.min.x, triggerCollider.bounds.max.x), triggerCollider.bounds.max.y, Random.Range(triggerCollider.bounds.min.z, triggerCollider.bounds.max.z));
            GameObject box = Instantiate(Resources.Load($"boxes/{this.gameObject.tag}") as GameObject, pos, Quaternion.identity);
            box.transform.SetParent(this.transform);
        }

        Invoke("DisableColliders", 2.0f);
    }

    void DisableColliders() {
        foreach (var box in boxColliders) {
            box.isTrigger = false;
        }
    }

    private void EmptyPosition()
    {
        foreach (var box in boxes)
        {
            Destroy(box);
        }

        boxes.Clear();
    }
}
