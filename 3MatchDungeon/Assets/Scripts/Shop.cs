using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;

    public Button ExitButton;

    public void ShopExit()
    {
        SceneManager.LoadScene("Main");
        //Debug.Log("Game Scene Changed");
    }

    // Start is called before the first frame update
    void Start()
    {
        coinText.SetText($"{GameManager.instance.coin}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
