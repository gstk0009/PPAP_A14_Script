using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SpawnPizza : MonoBehaviour
{
    public List<GameObject> SapwnTransforms;
    public GameObject SpawnEffect;

    private void Awake()
    {
        Spawn();
    }

    public void Spawn()
    {
        int randomIndex = Random.Range(0, SapwnTransforms.Count);
        GameObject newspawn;
        newspawn = Instantiate(SpawnEffect);
        newspawn.GetComponent<Transform>().position = SapwnTransforms[randomIndex].GetComponent<Transform>().position;
    }
}
