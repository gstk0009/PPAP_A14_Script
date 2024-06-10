using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FindOther : MonoBehaviour
{
    public SpawnPizza spawnPizza;
    public Canvas NPCUI;
    public bool spawn = false;

    private void Awake()
    {
        spawnPizza = spawnPizza.GetComponent<SpawnPizza>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6 && !spawn && GameManager.Instance.havePizza)
        {
            GameManager.Instance.PizzaCount++;
            spawnPizza.Spawn();
            spawn = true;
            GameManager.Instance.Pizza.gameObject.SetActive(false);
            NPCUIManager.Instance.MeetFakeWS();
            StartCoroutine("ReleaseObject", 2f);
        }
    }

    IEnumerator ReleaseObject(float time)
    {
        yield return new WaitForSeconds(time);

        gameObject.SetActive(false);
        spawn = false;
    }
}
