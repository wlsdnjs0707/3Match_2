using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public sealed class HealthManager : MonoBehaviour
{
    public static HealthManager Instance { get; private set; }

    public int _health = 100;

    public int Health
    {
        get => _health;

        set
        {
            if (_health == value) return;

            _health = value;

            healthText.SetText($"HEALTH : {_health}");
        }

    }

    [SerializeField] private TextMeshProUGUI healthText;

    private void Awake() => Instance = this;

    // Start is called before the first frame update
    void Start()
    {
        healthText.SetText($"HEALTH : {_health}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
