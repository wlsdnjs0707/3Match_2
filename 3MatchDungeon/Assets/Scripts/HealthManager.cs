using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public sealed class HealthManager : MonoBehaviour
{
    public static HealthManager Instance { get; private set; }

    public int _health;

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
        _health = GameManager.instance.stageHealth;

        if (SceneManager.GetActiveScene().name == "Stage1")
        {
            _health += 0;
        }

        if (SceneManager.GetActiveScene().name == "Stage2")
        {
            _health += 50;
        }

        if (SceneManager.GetActiveScene().name == "Stage3")
        {
            _health += 150;
        }

        healthText.SetText($"HEALTH : {_health}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
