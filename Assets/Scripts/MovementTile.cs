using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovementTile : MonoBehaviour
{
    [Header("Only ever have a 1 and a 0, and one of each")]
    public Vector2 moveDirection;
    public MovementTile linkedTile;
    public bool isMaskedLayer;

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            if(GameManager.instance.playerManager.isMasked  && isMaskedLayer || !GameManager.instance.playerManager.isMasked && !isMaskedLayer){
                Debug.Log("Trigger entered");
                GameManager.instance.playerManager.Move(moveDirection, true, (int)moveDirection.x, (int)moveDirection.y, this.gameObject);
            }
        }
    }
}
