using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChange : MonoBehaviour
{
    public GameObject stage2locked;
    public GameObject stage2unlocked;
    public GameObject stage3locked;
    public GameObject stage3unlocked;

    public Button button2;
    public Button button3;

    public void EnteringStage1()
    {
        SceneManager.LoadScene("Stage1");
        //Debug.Log("Game Scene Changed");
    }

    public void EnteringStage2()
    {
        SceneManager.LoadScene("Stage2");
        //Debug.Log("Game Scene Changed");
    }

    public void EnteringStage3()
    {
        SceneManager.LoadScene("Stage3");
        //Debug.Log("Game Scene Changed");
    }

    // Start is called before the first frame update
    void Start()
    {
        //DontDestroyOnLoad(gameObject);

        stage2unlocked.SetActive(false);
        button2.interactable = false;

        stage3unlocked.SetActive(false);
        button3.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.stage2access == true && button2.interactable == false)
        {
            stage2unlocked.SetActive(true);
            stage2locked.SetActive(false);
            button2.interactable = true;
        }

        if (GameManager.instance.stage3access == true && button3.interactable == false)
        {
            stage3unlocked.SetActive(true);
            stage3locked.SetActive(false);
            button3.interactable = true;
        }
    }
}
