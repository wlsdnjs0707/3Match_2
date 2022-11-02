using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public sealed class HealthManager : MonoBehaviour
{
    public static HealthManager Instance { get; private set; }

    public Image HealthBar;

    public float _health;
    private float maxHealth;

    public float Health
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
            maxHealth = 100;
        }

        if (SceneManager.GetActiveScene().name == "Stage2")
        {
            maxHealth = 200;
            _health += 100;
        }

        if (SceneManager.GetActiveScene().name == "Stage3")
        {
            maxHealth = 400;
            _health += 300;
        }

        healthText.SetText($"HEALTH : {_health}");
    }

    // Update is called once per frame
    void Update()
    {
        HealthBar.fillAmount = _health / maxHealth;
    }
}
