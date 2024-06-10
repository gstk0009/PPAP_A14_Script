using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CarObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject[] carObj;

    private List<float> positionX = new List<float>()
    { -3.9f, 4.44f, 56.27f, 55.95f, 63.8f, 116.4f, 116.08f, 123.93f, 176.2f, 175.88f, 183.73f, 236.3f, 235.98f,
        -3.9f, 4.44f, 3.6f, 56.2f, 64.54f, 63.7f, 55.7f, 116f, 124.34f, 123.5f, 115.5f, 176.4f, 184.74f, 183.9f, 175.9f, 236.2f, 243.7f, 235.7f,
        -3.9f, 4.44f, 3.6f, 64.74f, 63.9f, 55.9f, 116f, 124.34f, 123.5f, 115.5f, 184.94f, 184.1f, 176.1f, 236.2f, 243.7f, 235.7f,
        4.2f, 3.36f, 124.1f, 123.6f, 115.26f, 243.9f, 235.9f};

    private List<float> positionZ = new List<float>()
    {4.5f, 3.43f, 4.08f, -3.52f, 3.3f, 4.2f, -3.4f, 3.42f, 4.1f, -3.5f, 3.32f, 4.6f, -3f,
        64.6f, 63.53f, 56f, 65f, 63.93f, 56.4f, 56.9f, 64.8f, 63.73f, 56.2f, 56.7f, 64.8f, 63.73f, 56.2f, 56.7f, 65f, 56.4f, 56.9f,
        124.7f, 123.63f, 116.1f, 124.03f, 116.5f, 117f, 124.8f, 123.73f, 116.2f, 116.7f, 123.93f, 116.4f, 116.9f, 125f, 116.4f, 116.9f,
        163.3f, 155.77f, 163.4f, 155.87f, 156.37f, 156.4f, 156.9f};

    // up = 0, down = 180, left = -90, right = 90
    private List<float> rotationY = new List<float>()
    { 0f, 90f, 0f, -90f, 90f, 0f, -90f, 90f, 0f, -90f, 90f, 0f, -90f,
        0f, 90f, 180f, 0f, 90f, 180f, -90f, 0f, 90f, 180f, -90f, 0f, 90f, 180f, -90f, 0f, 180f, -90f,
        0f, 90f, 180f, 90f, 180f, -90f, 0f, 90f, 180f, -90f, 90f, 180f, -90f, 0f, 180f, -90f,
        90f, 180f, 90f, 180f, -90f, 180f, -90f};

    private void Awake()
    {
        SpawnPool();
    }

    private void SpawnPool()
    {
        for (int i = 0; i < positionX.Count; i++)
        {
            int randomIndex = Random.Range(0, carObj.Length);
            GameObject car = Instantiate(carObj[randomIndex]);
            car.transform.SetParent(gameObject.transform, false);
            car.transform.position = new Vector3(positionX[i], 0, positionZ[i]);
            car.transform.rotation = Quaternion.Euler(0, rotationY[i], 0);
            car.GetComponent<Vehicle>().SetReset(car.transform.position, car.transform.rotation);
        }
    }
}
