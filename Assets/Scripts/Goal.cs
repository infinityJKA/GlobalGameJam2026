using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public SpriteRenderer spriteRenderer, layer2SpriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Trigger entered on goal tile");

        GoalObject p = col.transform.GetComponent<GoalObject>();
    
        if(p != null)
        {
            Debug.Log("Level complete!");
            GameManager.instance.controlState = ControlState.None;
            GameManager.instance.playerManager.levelUiCanvas.SetActive(true);
        }
    }
}
