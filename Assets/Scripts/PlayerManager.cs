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

    public void MoveUp()
    {
        Move(Vector2.up, 0, 1);
    }

    public void Move(Vector2 dir, int unitUp, int unitDown)
    {
        bool hitLayer1 = Physics2D.Raycast(transform.position, dir, 1f, LayerMask.GetMask("Layer1"));
        bool hitLayer2 = Physics2D.Raycast(playerLayer2.transform.position, dir, 1f, LayerMask.GetMask("Layer2"));
        
        if (!hitLayer1 && !hitLayer2)
        {
            transform.position += (Vector3) new Vector2(unitUp,unitDown);
            playerLayer2.transform.position += (Vector3) new Vector2(unitUp,unitDown);
        }
    }


}
