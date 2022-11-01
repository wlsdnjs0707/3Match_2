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
    public Button Item1Button;

    private int count = 0;

    public GameObject Item1_Star1;
    public GameObject Item1_Star2;
    public GameObject Item1_Star3;

    public void ShopExit()
    {
        SceneManager.LoadScene("Main");
        //Debug.Log("Game Scene Changed");
    }

    public void BuyItem1()
    {
        if (GameManager.instance.moveCount < 13 && GameManager.instance.coin >= 250)
        {
            GameManager.instance.coin -= 250;
            GameManager.instance.moveCount += 1;

            shopRefresh();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // for test
        //GameManager.instance.coin += 1000;

        shopRefresh();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void shopRefresh()
    {
        count = GameManager.instance.moveCount;

        coinText.SetText($"{GameManager.instance.coin}");

        if (count >= 11)
        {
            if (count == 11)
            {
                Item1_Star1.SetActive(true);
            }
            else if (count == 12)
            {
                Item1_Star1.SetActive(true);
                Item1_Star2.SetActive(true);
            }
            else if (count == 13)
            {
                Item1_Star1.SetActive(true);
                Item1_Star2.SetActive(true);
                Item1_Star3.SetActive(true);

                Item1Button.interactable = false;
            }
        }
    }
}
