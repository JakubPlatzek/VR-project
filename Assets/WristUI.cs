using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WristUI : MonoBehaviour
{
    public InputActionAsset inputActionAsset;

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
}
