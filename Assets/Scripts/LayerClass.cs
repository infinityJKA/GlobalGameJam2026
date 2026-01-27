using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerClass : MonoBehaviour
{
    public List<SpriteRenderer> objects;
    public SpriteRenderer bg;

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
