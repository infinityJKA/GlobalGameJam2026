using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{
    [SerializeField] InputActionReference ref_interact, ref_mask, ref_up, ref_down, ref_left, ref_right, ref_reset;
    public GameManager gm;

    void OnEnable()
    {
        ref_interact.action.started += action_INTERACT;
        ref_mask.action.started += action_MASK;
        ref_reset.action.started += action_RESET;
        
        ref_up.action.started += action_UP;
        ref_down.action.started += action_DOWN;
        ref_left.action.started += action_LEFT;
        ref_right.action.started += action_RIGHT;
    }

    private void action_RESET(InputAction.CallbackContext obj)
    {
        if (gm.controlState == ControlState.Overworld || gm.controlState == ControlState.WinMenu)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            gm.controlState = ControlState.Overworld;
        }
    }

    private void action_INTERACT(InputAction.CallbackContext obj)
    {
        if (gm.controlState == ControlState.Overworld)
        {
            Debug.Log("Checking to push...");
            string layerToCheck;
            if(gm.playerManager.isMasked == false){
                layerToCheck = "Layer1";
            }
            else{
                layerToCheck = "Layer2";
            }

            RaycastHit2D hit;

            if(gm.playerManager.facing == PlayerFacing.Up)
            {
                hit = Physics2D.Raycast(gm.playerManager.transform.position, Vector2.up, 1.1f, LayerMask.GetMask(layerToCheck,"Don't Collide"));  
            }
            else if(gm.playerManager.facing == PlayerFacing.Down)
            {
                hit = Physics2D.Raycast(gm.playerManager.transform.position, Vector2.down, 1.1f, LayerMask.GetMask(layerToCheck,"Don't Collide"));   
            }
            else if(gm.playerManager.facing == PlayerFacing.Left)
            {
                hit = Physics2D.Raycast(gm.playerManager.transform.position, Vector2.left, 1.1f, LayerMask.GetMask(layerToCheck,"Don't Collide"));   
            }
            else
            {
                hit = Physics2D.Raycast(gm.playerManager.transform.position, Vector2.right, 1.1f, LayerMask.GetMask(layerToCheck,"Don't Collide"));   
            }

            if (hit)
            {
                GameObject hitObject = hit.collider.gameObject;

                if (hitObject.GetComponent<Pushable>())
                {

                    Debug.Log("Interacted with a pushable object");

                    hitObject.GetComponent<BoxCollider2D>().enabled = false;

                    RaycastHit2D objectBlocking;

                    if(gm.playerManager.facing == PlayerFacing.Up)
                    {
                        objectBlocking = Physics2D.Raycast(hitObject.transform.position, Vector2.up, 1.1f, LayerMask.GetMask(layerToCheck));  
                    }
                    else if(gm.playerManager.facing == PlayerFacing.Down)
                    {
                        objectBlocking = Physics2D.Raycast(hitObject.transform.position, Vector2.down, 1.1f, LayerMask.GetMask(layerToCheck));   
                    }
                    else if(gm.playerManager.facing == PlayerFacing.Left)
                    {
                        objectBlocking = Physics2D.Raycast(hitObject.transform.position, Vector2.left, 1.1f, LayerMask.GetMask(layerToCheck));   
                    }
                    else
                    {
                        objectBlocking = Physics2D.Raycast(hitObject.transform.position, Vector2.right, 1.1f, LayerMask.GetMask(layerToCheck));   
                    }

                    if (!objectBlocking || objectBlocking.transform.gameObject.GetComponent<MovementTile>() != null)
                    {
                        Debug.Log("Valid push");

                        Vector3 newPosition = hitObject.transform.position;

                        if(gm.playerManager.facing == PlayerFacing.Up)
                        {
                            newPosition += new Vector3(0, 1, 0);
                        }
                        else if(gm.playerManager.facing == PlayerFacing.Down)
                        {
                            newPosition += new Vector3(0, -1, 0);
                        }
                        else if(gm.playerManager.facing == PlayerFacing.Left)
                        {
                            newPosition += new Vector3(-1, 0, 0);
                        }
                        else
                        {
                            newPosition += new Vector3(1, 0, 0);
                        }

                        hitObject.transform.position = newPosition;

                        gm.audioManager.PlaySfx("Push");
                    }
                    else
                    {
                        Debug.Log("Can't push, "+objectBlocking.transform.name+" is blocking the way");
                    }

                    hitObject.GetComponent<BoxCollider2D>().enabled = true;

                    // check for linked object to move //////////////////////////////////

                    GameObject linked = hitObject.GetComponent<Pushable>().linkedObject;
                    if(linked != null)
                    {
                        Debug.Log("Has linked object to push");

                        linked.GetComponent<BoxCollider2D>().enabled = false;

                        string layerToCheckLinked;

                        if(gm.playerManager.isMasked == false){
                            layerToCheckLinked = "Layer2";
                        }
                        else{
                            layerToCheckLinked = "Layer1";
                        }

                        RaycastHit2D objectBlockingLinked;

                        if(gm.playerManager.facing == PlayerFacing.Up)
                        {
                            objectBlockingLinked = Physics2D.Raycast(linked.transform.position, Vector2.up, 1.1f, LayerMask.GetMask(layerToCheckLinked));  
                        }
                        else if(gm.playerManager.facing == PlayerFacing.Down)
                        {
                            objectBlockingLinked = Physics2D.Raycast(linked.transform.position, Vector2.down, 1.1f, LayerMask.GetMask(layerToCheckLinked));   
                        }
                        else if(gm.playerManager.facing == PlayerFacing.Left)
                        {
                            objectBlockingLinked = Physics2D.Raycast(linked.transform.position, Vector2.left, 1.1f, LayerMask.GetMask(layerToCheckLinked));   
                        }
                        else
                        {
                            objectBlockingLinked = Physics2D.Raycast(linked.transform.position, Vector2.right, 1.1f, LayerMask.GetMask(layerToCheckLinked));   
                        }

                        if (!objectBlockingLinked || objectBlockingLinked.transform.gameObject.GetComponent<MovementTile>() != null)
                        {
                            Debug.Log("Valid push (linked object)");

                            Vector3 newPosition = linked.transform.position;
                            if(gm.playerManager.facing == PlayerFacing.Up)
                            {
                                newPosition += new Vector3(0, 1, 0);
                            }
                            else if(gm.playerManager.facing == PlayerFacing.Down)
                            {
                                newPosition += new Vector3(0, -1, 0);
                            }
                            else if(gm.playerManager.facing == PlayerFacing.Left)
                            {
                                newPosition += new Vector3(-1, 0, 0);
                            }
                            else
                            {
                                newPosition += new Vector3(1, 0, 0);
                            }

                            linked.transform.position = newPosition;

                            gm.audioManager.PlaySfx("Push");
                        }
                        else
                        {
                            Debug.Log("Can't push, "+objectBlockingLinked.transform.name+" is blocking the way");
                        }
                        linked.GetComponent<BoxCollider2D>().enabled = true;
                    }
                }
                else
                {
                    Debug.Log(hit.transform.name + " is not pushable");
                }
            }
            else
            {
                Debug.Log("Nothing to push (no raycast hit)");
            }

        }
        else if (gm.controlState == ControlState.WinMenu)
        {
            GameManager.instance.controlState = ControlState.None;
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            if (nextSceneIndex < SceneManager.sceneCountInBuildSettings){
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                GameManager.instance.controlState = ControlState.Overworld;
            }
            else
            {
                GameManager.instance.audioManager.StopMusic();
                SceneManager.LoadScene("MainMenu");
            }
        }
    }

    private void action_MASK(InputAction.CallbackContext obj)
    {
        if (gm.controlState == ControlState.Overworld)
        {
            RaycastHit2D invalidSwitch;
            if(gm.playerManager.isMasked == false){
                invalidSwitch = Physics2D.Raycast(gm.playerManager.playerLayer2.transform.position, Vector2.up, 0.1f, LayerMask.GetMask("Layer2"));
            }
            else{
                invalidSwitch = Physics2D.Raycast(gm.playerManager.gameObject.transform.position, Vector2.up, 0.1f, LayerMask.GetMask("Layer1"));
            }

            if (invalidSwitch && !invalidSwitch.transform.gameObject.GetComponent<MovementTile>())
            {
                Debug.Log("Can't switch right now!");
                return;
            }

            gm.audioManager.PlaySfx("Mask");

            if(gm.playerManager.isMasked == false)
            {
                foreach(SpriteRenderer s in gm.playerManager.layer2.objects)
                {
                    s.sortingOrder = 0;
                    Debug.Log(s.gameObject.name +" edited priority");
                }
                gm.playerManager.layer2.tilemapRenderer.sortingOrder = 0;
                gm.playerManager.layer2.noCollisionRenderer.sortingOrder = -1;
                gm.playerManager.layer2.bg.sortingOrder = -2;

                foreach (SpriteRenderer s in gm.playerManager.layer1.objects)
                {
                    s.sortingOrder = -5;
                    Debug.Log(s.gameObject.name +" edited priority");

                }
                gm.playerManager.layer1.bg.sortingOrder = -6;
                gm.playerManager.layer1.tilemapRenderer.sortingOrder = -5;
                gm.playerManager.layer1.noCollisionRenderer.sortingOrder = -5;

                gm.playerManager.spriteRenderer.sortingOrder = -2;
                gm.playerManager.spriteRenderer2.sortingOrder = 2;

                gm.playerManager.goal.spriteRenderer.sortingOrder = -10;
                gm.playerManager.goal.layer2SpriteRenderer.sortingOrder = 1;


                gm.playerManager.isMasked = true;
                gm.audioManager.SwitchMusicSource(true);
            }
            else
            {
                foreach(SpriteRenderer s in gm.playerManager.layer1.objects)
                {
                    s.sortingOrder = 0;
                    Debug.Log(s.gameObject.name +" edited priority");
                }
                gm.playerManager.layer1.tilemapRenderer.sortingOrder = 0;
                gm.playerManager.layer1.noCollisionRenderer.sortingOrder = -1;
                gm.playerManager.layer1.bg.sortingOrder = -2;

                foreach (SpriteRenderer s in gm.playerManager.layer2.objects)
                {
                    s.sortingOrder = -5;
                    Debug.Log(s.gameObject.name +" edited priority");
                }
                gm.playerManager.layer2.bg.sortingOrder = -6;
                gm.playerManager.layer2.tilemapRenderer.sortingOrder = -5;
                gm.playerManager.layer2.noCollisionRenderer.sortingOrder = -5;

                gm.playerManager.spriteRenderer2.sortingOrder = -2;
                gm.playerManager.spriteRenderer.sortingOrder = 2;

                gm.playerManager.goal.spriteRenderer.sortingOrder = 1;
                gm.playerManager.goal.layer2SpriteRenderer.sortingOrder = -10;

                gm.playerManager.isMasked = false;
                gm.audioManager.SwitchMusicSource(false);
            }

            gm.playerManager.gameObject.SetActive(false);
            gm.playerManager.gameObject.SetActive(true);
        
            gm.playerManager.UpdateSprite();

        }
        else if (gm.controlState == ControlState.WinMenu)
        {
            GameManager.instance.controlState = ControlState.None;
            Debug.Log("ReturnToHomeMenu");
            GameManager.instance.audioManager.StopMusic();
            SceneManager.LoadScene("MainMenu");
        }
    }

    private void action_UP(InputAction.CallbackContext obj)
    {
        if (gm.controlState == ControlState.Overworld)
        {
            gm.playerManager.Move(Vector2.up, false, 0, 1, null);
            gm.playerManager.facing = PlayerFacing.Up;
            gm.playerManager.UpdateSprite();
            gm.audioManager.PlaySfx("Move");
        }
    }

    private void action_DOWN(InputAction.CallbackContext obj)
    {
        if (gm.controlState == ControlState.Overworld)
        {
            gm.playerManager.Move(Vector2.down, false, 0, -1, null);
            gm.playerManager.facing = PlayerFacing.Down;
            gm.playerManager.UpdateSprite();
            gm.audioManager.PlaySfx("Move");
        }
    }

    private void action_LEFT(InputAction.CallbackContext obj)
    {
        if (gm.controlState == ControlState.Overworld)
        {
            gm.playerManager.Move(Vector2.left, false, -1, 0, null);
            gm.playerManager.facing = PlayerFacing.Left;
            gm.playerManager.UpdateSprite();
            gm.audioManager.PlaySfx("Move");
        }
    }

    private void action_RIGHT(InputAction.CallbackContext obj)
    {
        if (gm.controlState == ControlState.Overworld)
        {
            gm.playerManager.Move(Vector2.right, false, 1, 0, null);
            gm.playerManager.facing = PlayerFacing.Right;
            gm.playerManager.UpdateSprite();
            gm.audioManager.PlaySfx("Move");
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
    Overworld,
    WinMenu
}