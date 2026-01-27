using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{
    [SerializeField] InputActionReference ref_interact, ref_mask, ref_up, ref_down, ref_left, ref_right;
    public GameManager gm;

    void OnEnable()
    {
        ref_interact.action.started += action_INTERACT;
        ref_mask.action.started += action_MASK;
        
        ref_up.action.started += action_UP;
        ref_down.action.started += action_DOWN;
        ref_left.action.started += action_LEFT;
        ref_right.action.started += action_RIGHT;
    }

    private void action_INTERACT(InputAction.CallbackContext obj)
    {
        if (gm.controlState == ControlState.Overworld)
        {
            
        }
    }

    private void action_MASK(InputAction.CallbackContext obj)
    {
        if (gm.controlState == ControlState.Overworld)
        {
            if(gm.playerManager.layer1.activeSelf)
            {
                gm.playerManager.layer1.SetActive(false);
                gm.playerManager.layer2.SetActive(true);
            }
            else
            {
                gm.playerManager.layer1.SetActive(true);
                gm.playerManager.layer2.SetActive(false);
            }
        }
    }

    private void action_UP(InputAction.CallbackContext obj)
    {
    }

    private void action_DOWN(InputAction.CallbackContext obj)
    {
    }

    private void action_LEFT(InputAction.CallbackContext obj)
    {
    }

    private void action_RIGHT(InputAction.CallbackContext obj)
    {
    }


    private void FixedUpdate()
    {
        if (gm.controlState == ControlState.Overworld)
        {  
        }
    }
}

public enum ControlState
{
    None,
    Overworld
}