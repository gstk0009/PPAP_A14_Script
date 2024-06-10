using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void ReTrytBtn()
    {
        GameManager.Instance.PizzaCount = 0;
        GameManager.Instance.curScore = 0;
        if (GameManager.Instance.Pizza != null && GameManager.Instance.Pizza.activeSelf) 
        {
            GameManager.Instance.Pizza.gameObject.SetActive(false);
            GameManager.Instance.SpawnPizza.Spawn();
        }
    }
    public void MainStartBtn()
    {
        LoadSceneManager.LoadScene("MainScene");
    }

    public void MainMenuBtn()
    {
        LoadSceneManager.LoadScene("StartScene");
    }
}
