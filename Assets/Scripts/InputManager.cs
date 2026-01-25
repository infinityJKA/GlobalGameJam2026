using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{
    [SerializeField] InputActionReference ref_interact, ref_mask, ref_move;
    public GameManager gm;

    void OnEnable()
    {
        ref_interact.action.started += action_INTERACT;
        ref_mask.action.started += action_MASK;
        ref_move.action.Enable();
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
            
        }
    }

    private void FixedUpdate()
    {
        if (gm.controlState == ControlState.Overworld)
        {  
            Vector2 moveValue = ref_move.action.ReadValue<Vector2>();

            if (moveValue != Vector2.zero)
            {
                moveValue.Normalize();
                gm.playerManager.moveDir = moveValue;
                // gm.moveManager.player.animator.SetFloat("Speed", 1);
                //Debug.Log("RETURN (state = overworld)");
            }
            else
            {
                gm.playerManager.moveDir = moveValue;
                // gm.moveManager.StopWalkSound();
                // gm.moveManager.player.animator.SetFloat("Speed", 0);
            }
        }
    }
}

public enum ControlState
{
    None,
    Overworld
}