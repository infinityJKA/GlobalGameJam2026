using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerManager : MonoBehaviour
{
    public float moveSpeed;

    public GameObject playerLayer2;

    public LayerClass layer1, layer2;
    public Goal goal;
    public bool isMasked = false;
    public PlayerFacing facing = PlayerFacing.Down;

    public SpriteRenderer spriteRenderer, spriteRenderer2;

    public Sprite up, upMasked, down, downMasked, left, leftMasked, right, rightMasked;

    void Start()
    {
        GameManager.instance.playerManager = this;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer2 = playerLayer2.GetComponent<SpriteRenderer>();
    }

    public void Move(Vector2 dir, bool isForced, int unitUp, int unitDown, GameObject disableOnClose)
    {
        RaycastHit2D hitClose;
        RaycastHit2D hitFar;
        if(isMasked == false){

            if(disableOnClose != null){disableOnClose.SetActive(false);}

            hitClose = Physics2D.Raycast(transform.position, dir, 1.1f, LayerMask.GetMask("Layer1"));
            
            if(hitClose){
                Debug.Log("hitClose: " + hitClose.transform.gameObject.name);
                if (hitClose.transform.gameObject.GetComponent<BoxCollider2D>()) {hitClose.transform.gameObject.GetComponent<BoxCollider2D>().enabled = false;}
                else if( hitClose.transform.gameObject.GetComponent<TilemapCollider2D>()) { hitClose.transform.gameObject.GetComponent<TilemapCollider2D>().enabled = false;}
            }
            else{Debug.Log("hitClose: null");}

            if(disableOnClose != null){disableOnClose.SetActive(true);}

            hitFar = Physics2D.Raycast(transform.position, dir, 2.1f, LayerMask.GetMask("Layer1"));
            if(hitFar){Debug.Log("hitFar: " + hitFar.transform.gameObject.name);}
            else{Debug.Log("hitFar: null");}

            if(hitClose){
                if (hitClose.transform.gameObject.GetComponent<BoxCollider2D>()) {hitClose.transform.gameObject.GetComponent<BoxCollider2D>().enabled = true;}
                else if( hitClose.transform.gameObject.GetComponent<TilemapCollider2D>()) { hitClose.transform.gameObject.GetComponent<TilemapCollider2D>().enabled = true;}
            }

        }
        else{
            if(disableOnClose != null){disableOnClose.SetActive(false);}

            hitClose = Physics2D.Raycast(playerLayer2.transform.position, dir, 1.1f, LayerMask.GetMask("Layer2"));

            if(hitClose){
                Debug.Log("hitClose: " + hitClose.transform.gameObject.name);
                if (hitClose.transform.gameObject.GetComponent<BoxCollider2D>()) {hitClose.transform.gameObject.GetComponent<BoxCollider2D>().enabled = false;}
                else if( hitClose.transform.gameObject.GetComponent<TilemapCollider2D>()) { hitClose.transform.gameObject.GetComponent<TilemapCollider2D>().enabled = false;}
            }
            else{Debug.Log("hitClose: null");}

            if(disableOnClose != null){disableOnClose.SetActive(true);}

            hitFar = Physics2D.Raycast(playerLayer2.transform.position, dir, 2.1f, LayerMask.GetMask("Layer2"));
            if(hitFar){Debug.Log("hitFar: " + hitFar.transform.gameObject.name);}
            else{Debug.Log("hitFar: null");}

            if(hitClose){
                if (hitClose.transform.gameObject.GetComponent<BoxCollider2D>()) {hitClose.transform.gameObject.GetComponent<BoxCollider2D>().enabled = true;}
                else if( hitClose.transform.gameObject.GetComponent<TilemapCollider2D>()) { hitClose.transform.gameObject.GetComponent<TilemapCollider2D>().enabled = true;}
            }
        }

        if ( isForced && ( hitFar == false || (hitFar && hitFar.transform.gameObject.GetComponent<MovementTile>()) ) ){
            if(hitFar)Debug.Log("Moving to far tile (hitfar = "+hitFar.transform.gameObject.name+")");
            else Debug.Log("Moving to far tile (hitfar = null)");

            transform.position += (Vector3) new Vector2(unitUp*2,unitDown*2);
            playerLayer2.transform.position += (Vector3) new Vector2(unitUp*2,unitDown*2);
        }
        else if (hitClose == false || hitClose.transform.gameObject.GetComponent<MovementTile>() )
        {
            if(hitClose)Debug.Log("Moving to close tile (hitClose = "+hitClose.transform.gameObject.name+")");
            else Debug.Log("Moving to close tile (hitClose = null)");

            transform.position += (Vector3) new Vector2(unitUp,unitDown);
            playerLayer2.transform.position += (Vector3) new Vector2(unitUp,unitDown);
        }
        else
        {
            Debug.Log("No valid move");
        }
    }

    public void UpdateSprite()
    {
        if(isMasked)
        {
            switch(facing)
            {
                case PlayerFacing.Up:
                    spriteRenderer.sprite = upMasked;
                    spriteRenderer2.sprite = upMasked;
                    break;
                case PlayerFacing.Down:
                    spriteRenderer.sprite = downMasked;
                    spriteRenderer2.sprite = downMasked;
                    break;
                case PlayerFacing.Left:
                    spriteRenderer.sprite = leftMasked;
                    spriteRenderer2.sprite = leftMasked;
                    break;
                case PlayerFacing.Right:
                    spriteRenderer.sprite = rightMasked;
                    spriteRenderer2.sprite = rightMasked;
                    break;
            }
        }
        else
        {
            switch(facing)
            {
                case PlayerFacing.Up:
                    spriteRenderer.sprite = up;
                    spriteRenderer2.sprite = up;
                    break;
                case PlayerFacing.Down:
                    spriteRenderer.sprite = down;
                    spriteRenderer2.sprite = down;
                    break;
                case PlayerFacing.Left:
                    spriteRenderer.sprite = left;
                    spriteRenderer2.sprite = left;
                    break;
                case PlayerFacing.Right:
                    spriteRenderer.sprite = right;
                    spriteRenderer2.sprite = right;
                    break;
            }
        }
    }

}

public enum PlayerFacing
{
    Up,
    Down,
    Left,
    Right
}
