using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public sealed class MoveCountManager : MonoBehaviour
{
    public static MoveCountManager Instance { get; private set; }

    public int _count;

    public int Count
    {
        get => _count;

        set
        {
            if (_count == value) return;

            _count = value;

            moveCountText.SetText($"MOVE : {_count}");
        }

    }

    [SerializeField] private TextMeshProUGUI moveCountText;

    private void Awake() => Instance = this;

    // Start is called before the first frame update
    void Start()
    {
        _count = GameManager.instance.moveCount;

        moveCountText.SetText($"MOVE : {_count}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
