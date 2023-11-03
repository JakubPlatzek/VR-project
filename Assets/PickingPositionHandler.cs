using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickingPositionHandler : MonoBehaviour
{

    BoxCollider boxCollider;
    public int maxAmountOfBoxes = 10;
    public List<GameObject> boxes = new List<GameObject>();
    
    void Awake() 
    {
        boxCollider = GetComponent<BoxCollider>();
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
        EmptyPosition();
        for (var i = 0; i < maxAmountOfBoxes; i++) {
            Vector3 pos = new Vector3(Random.Range(boxCollider.bounds.min.x, boxCollider.bounds.max.x), boxCollider.bounds.max.y, Random.Range(boxCollider.bounds.min.z, boxCollider.bounds.max.z));
            GameObject box = Instantiate(Resources.Load($"boxes/{this.gameObject.tag}") as GameObject, pos, Quaternion.identity);
            box.transform.SetParent(this.transform);
        }
    }

    private void EmptyPosition()
    {
        foreach (var box in boxes)
        {
            Destroy(box);
        }
    }
}
