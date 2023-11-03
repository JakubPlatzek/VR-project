using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class WristUI : MonoBehaviour
{
    public InputActionAsset inputActionAsset;

    public List<PickingPositionHandler> boxPositions;

    public Dictionary<string, int> orderPositions = new Dictionary<string, int>();

    public int maxNumberOfPositions = 5;

    public int maxNumberOfBoxes = 10;

    public BoxCollider runnerBoxCollider;
    public OrderCheckHandler orderCheckHandler;

    public TextMeshProUGUI orderText;

    private Canvas _wristUICanvas;
    private InputAction _menu;
    
    void Start()
    {
        _wristUICanvas = GetComponent<Canvas>();
        _menu = inputActionAsset.FindActionMap("XRI LeftHand").FindAction("WristMenu");
        _menu.Enable();
        _menu.performed += ToggleMenu;
    }

    void OnDestroy() 
    {
        _menu.performed -= ToggleMenu;
    }

    void ToggleMenu(InputAction.CallbackContext context)
    {
        _wristUICanvas.enabled = !_wristUICanvas.enabled;
    }

    public void GenerateOrder()
    {
        foreach (var boxPosition in boxPositions)
        {
            boxPosition.RefillPosition();
        }

        foreach (var pos in boxPositions) {
            if (orderPositions.Count != maxNumberOfPositions) {
                bool choosePosition = Random.Range(0, 2) == 0;
                if (choosePosition) {
                    var amount = Random.Range(1, maxNumberOfBoxes + 1);
                    orderPositions.Add(pos.gameObject.tag, amount);
                    orderText.text += $"{pos.gameObject.tag} - {amount}\n";
                }
            }
        }
    }

    public void ClearOrder()
    {
        orderPositions.Clear();
        orderText.text = "";
    }

    public bool CheckOrder()
    {
        foreach (var box in orderCheckHandler.boxes)
        {
            if (orderPositions.ContainsKey(box.tag))
            {
                orderPositions[box.tag] -= 1;
                if (orderPositions[box.tag] == 0)
                {
                    orderPositions.Remove(box.tag);
                }
            }
        }

        return orderPositions.Count == 0; 
    }
}
