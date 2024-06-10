using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GetPizza : MonoBehaviour
{
    public GameObject Pizza;
    public GameObject Back;
    public List<GameObject> WsRandomSpawn;
    public GameObject[] WsObject;

    private List<GameObject> WsSpawnObject;
    private bool isFirstGame = true;
    private int randomIndex;

    private void Awake()
    {
        WsSpawnObject = new List<GameObject>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Spawn Layer = 8
        if (other.gameObject.layer == 8)
        {
            Destroy(other.gameObject);

            if (isFirstGame)
            {
                GameObject pizza = Instantiate(Pizza);
                GameManager.Instance.Pizza = pizza;
                pizza.transform.position = new Vector3(0, -0.23f, 0f);
                pizza.transform.rotation = Quaternion.Euler(90f, -90f, 0f);
                pizza.transform.SetParent(Back.transform, false);
            }
            else
            {
                GameManager.Instance.Pizza.gameObject.SetActive(true);
            }

            GameManager.Instance.havePizza = true;

            // 피자를 얻었을 때 50점 증가하도록 
            GameManager.Instance.curScore += 50;
            SpawnWS();
        }
    }

    private void SpawnWS()
    {
        if (GameManager.Instance.PizzaCount == 0 && isFirstGame)
        {
            int count = 0;
            while (true)
            {
                randomIndex = Random.Range(0, WsRandomSpawn.Count);
                GameObject newspawn;

                if (count == 2)
                    newspawn = Instantiate(WsObject[0]);
                else
                    newspawn = Instantiate(WsObject[1]);

                WsSpawnObject.Add(newspawn);
                newspawn.GetComponent<Transform>().position = WsRandomSpawn[randomIndex].GetComponent<Transform>().position;
                newspawn.GetComponent<Transform>().rotation = WsRandomSpawn[randomIndex].GetComponent<Transform>().rotation;
                WsRandomSpawn.RemoveAt(randomIndex);
                count += 1;
                
                if (WsRandomSpawn.Count == 0)
                    break;
            }
        }
        else if(GameManager.Instance.PizzaCount == 0)
        {
            foreach (var newspawn in WsSpawnObject)
            {
                newspawn.SetActive(true);
            }
        }
    }

    public void resetSpawnWs()
    {
        foreach (var newspawn in WsSpawnObject)
        {
            newspawn.SetActive(false);
            isFirstGame = false;
        }
    }
}
