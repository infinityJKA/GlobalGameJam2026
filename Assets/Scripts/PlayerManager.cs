using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float moveSpeed;

    [SerializeField] Rigidbody2D rb;
    public Vector2 moveDir;
    

    void FixedUpdate()
    {
        rb.velocity = moveDir * moveSpeed;
    }

    void Start()
    {
        GameManager.instance.playerManager = this;
    }


}
