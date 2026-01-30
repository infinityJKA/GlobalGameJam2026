using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LayerClass : MonoBehaviour
{
    public List<SpriteRenderer> objects;
    public SpriteRenderer bg;
    public TilemapRenderer tilemapRenderer, noCollisionRenderer;

    void Start()
    {
        foreach(Transform child in transform)
        {
            if(child.GetComponent<SpriteRenderer>() != null)
            {
                objects.Add(child.GetComponent<SpriteRenderer>());
            }
        }
    }
}
