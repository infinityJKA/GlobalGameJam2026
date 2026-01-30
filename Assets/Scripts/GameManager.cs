using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ControlState controlState;
    public AudioManager audioManager;
    [SerializeField] GameObject eventSystem;

    [Header("Automatic (don't edit in inspector)")]
    public static GameManager instance;
    public PlayerManager playerManager;
 

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this);

        eventSystem.gameObject.SetActive(true);


    }

}
