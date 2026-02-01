using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;


public class MovementTile : MonoBehaviour
{
    [Header("Only ever have a 1 and a 0, and one of each")]
    public Vector2 moveDirection;
    public bool isMaskedLayer;

    private BoxCollider2D thisCollider;

    void Start()
    {
        thisCollider = GetComponent<BoxCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            if(GameManager.instance.playerManager.isMasked  && isMaskedLayer || !GameManager.instance.playerManager.isMasked && !isMaskedLayer){
                Debug.Log("Trigger entered");
                GameManager.instance.playerManager.Move(moveDirection, true, (int)moveDirection.x, (int)moveDirection.y, this.gameObject);
            }
        }
        if (col.gameObject.CompareTag("Pushable") && col.gameObject.GetComponent<MovementTile>() == null)
        {
            if( (col.gameObject.layer == LayerMask.NameToLayer("Layer1") && !isMaskedLayer) || (col.gameObject.layer == LayerMask.NameToLayer("Layer2") && isMaskedLayer) ){
                Debug.Log("Pushable Trigger entered on layer " + (isMaskedLayer ? "Layer2" : "Layer1") + " ("+col.gameObject.name+")");
                ForceMove(moveDirection, true, (int)moveDirection.x, (int)moveDirection.y, col.gameObject, isMaskedLayer ? "Layer2" : "Layer1");
            }
        }
    }

    public void ForceMove(Vector2 dir, bool isForced, int unitUp, int unitDown, GameObject objToMove, string layer)
    {
        thisCollider.enabled = false;

        RaycastHit2D hitClose;
        RaycastHit2D hitFar;

        objToMove.SetActive(false);

        hitClose = Physics2D.Raycast(transform.position, dir, 1.1f, LayerMask.GetMask(layer)); // LayerMask.GetMask("Layer1")
        
        if(hitClose){
            Debug.Log("hitClose: " + hitClose.transform.gameObject.name);
            if (hitClose.transform.gameObject.GetComponent<BoxCollider2D>()) {hitClose.transform.gameObject.GetComponent<BoxCollider2D>().enabled = false;}
            else if( hitClose.transform.gameObject.GetComponent<TilemapCollider2D>()) { hitClose.transform.gameObject.GetComponent<TilemapCollider2D>().enabled = false;}
        }
        else{Debug.Log("hitClose: null");}

        hitFar = Physics2D.Raycast(transform.position, dir, 2.1f, LayerMask.GetMask(layer));
        if(hitFar){Debug.Log("hitFar: " + hitFar.transform.gameObject.name);}
        else{Debug.Log("hitFar: null");}

        objToMove.SetActive(true);

        if(hitClose){
            if (hitClose.transform.gameObject.GetComponent<BoxCollider2D>()) {hitClose.transform.gameObject.GetComponent<BoxCollider2D>().enabled = true;}
            else if( hitClose.transform.gameObject.GetComponent<TilemapCollider2D>()) { hitClose.transform.gameObject.GetComponent<TilemapCollider2D>().enabled = true;}
        }

        if ( isForced && ( hitFar == false || (hitFar && hitFar.transform.gameObject.GetComponent<MovementTile>()) ) ){
            if(hitFar)Debug.Log("Moving to far tile (hitfar = "+hitFar.transform.gameObject.name+")");
            else Debug.Log("Moving to far tile (hitfar = null)");

            objToMove.transform.position += (Vector3) new Vector2(unitUp*2,unitDown*2);
        }
        else if (hitClose == false || hitClose.transform.gameObject.GetComponent<MovementTile>() )
        {
            if(hitClose)Debug.Log("Moving to close tile (hitClose = "+hitClose.transform.gameObject.name+")");
            else Debug.Log("Moving to close tile (hitClose = null)");

            objToMove.transform.position += (Vector3) new Vector2(unitUp,unitDown);
        }
        else
        {
            Debug.Log("No valid move");
        }

        thisCollider.enabled = true;
    }
}
