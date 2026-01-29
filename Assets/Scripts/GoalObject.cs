using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalObject : MonoBehaviour
{
    [Header("This is what is supposed to be pushed into the goal")]
    public Pushable pushable;

    void Start()
    {
        pushable = GetComponent<Pushable>();
    }
}
