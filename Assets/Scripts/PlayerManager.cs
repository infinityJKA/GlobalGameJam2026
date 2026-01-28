using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float moveSpeed;

    public GameObject playerLayer2;

    public LayerClass layer1, layer2;
    public bool isMasked = false;

    public SpriteRenderer spriteRenderer, spriteRenderer2;

    void Start()
    {
        GameManager.instance.playerManager = this;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer2 = playerLayer2.GetComponent<SpriteRenderer>();
    }

    public void Move(Vector2 dir, bool isForced, int unitUp, int unitDown)
    {
        bool hitlayer;
        bool hitlayerFar;
        if(isMasked == false){
            hitlayer = Physics2D.Raycast(transform.position, dir, 1.1f, LayerMask.GetMask("Layer1"));
            hitlayerFar = Physics2D.Raycast(transform.position, dir, 2.1f, LayerMask.GetMask("Layer1"));
        }
        else{
            hitlayer = Physics2D.Raycast(playerLayer2.transform.position, dir, 1.1f, LayerMask.GetMask("Layer2"));
            hitlayerFar = Physics2D.Raycast(playerLayer2.transform.position, dir, 2.1f, LayerMask.GetMask("Layer2"));
        }
        
        if (hitlayer == false)
        {
            if(hitlayerFar == false && isForced){
                transform.position += (Vector3) new Vector2(unitUp*2,unitDown*2);
                playerLayer2.transform.position += (Vector3) new Vector2(unitUp*2,unitDown*2);
            }
            else
            {
                transform.position += (Vector3) new Vector2(unitUp,unitDown);
                playerLayer2.transform.position += (Vector3) new Vector2(unitUp,unitDown);
            }
        }
    }


}
