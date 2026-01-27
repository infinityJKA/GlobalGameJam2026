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
            if(gm.playerManager.isMasked == false)
            {
                foreach(SpriteRenderer s in gm.playerManager.layer2.objects)
                {
                    s.rendererPriority = 0;
                    Debug.Log(s.gameObject.name +" edited priority");
                }
                gm.playerManager.layer2.bg.rendererPriority = -1;

                foreach (SpriteRenderer s in gm.playerManager.layer1.objects)
                {
                    s.rendererPriority = -5;
                    Debug.Log(s.gameObject.name +" edited priority");

                }

                gm.playerManager.spriteRenderer.sortingOrder = -2;
                gm.playerManager.spriteRenderer2.sortingOrder = 1;

                gm.playerManager.isMasked = true;
            }
            else
            {
                foreach(SpriteRenderer s in gm.playerManager.layer1.objects)
                {
                    s.rendererPriority = 0;
                    Debug.Log(s.gameObject.name +" edited priority");
                }
                gm.playerManager.layer1.bg.rendererPriority = -1;

                foreach (SpriteRenderer s in gm.playerManager.layer2.objects)
                {
                    s.rendererPriority = -5;
                    Debug.Log(s.gameObject.name +" edited priority");
                }

                gm.playerManager.spriteRenderer2.sortingOrder = -2;
                gm.playerManager.spriteRenderer.sortingOrder = 1;

                gm.playerManager.isMasked = false;
            }
        }
    }

    private void action_UP(InputAction.CallbackContext obj)
    {
        if (gm.controlState == ControlState.Overworld)
        {
            gm.playerManager.Move(Vector2.up, 0, 1);
        }
    }

    private void action_DOWN(InputAction.CallbackContext obj)
    {
        if (gm.controlState == ControlState.Overworld)
        {
            gm.playerManager.Move(Vector2.down, 0, -1);
        }
    }

    private void action_LEFT(InputAction.CallbackContext obj)
    {
        if (gm.controlState == ControlState.Overworld)
        {
            gm.playerManager.Move(Vector2.left, -1, 0);
        }
    }

    private void action_RIGHT(InputAction.CallbackContext obj)
    {
        if (gm.controlState == ControlState.Overworld)
        {
            gm.playerManager.Move(Vector2.right, 1, 0);
        }
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