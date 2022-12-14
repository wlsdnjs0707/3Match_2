using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public bool stage2access = false;
    public bool stage3access = false;

    public float coin = 0;

    public int damage = 0;
    public int moveCount = 10;
    public float stageHealth = 100;
}
