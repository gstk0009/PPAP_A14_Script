using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] GameObject roadMap;

    private ObjectPool pool;
    public List<Vector3> spawnPositions = new List<Vector3>();

    public void Initialize()
    {
        pool = GameManager.Instance.pool;
        FindRoadPos();
    }

    private void FindRoadPos()
    {

        GameObject[] roadObjects = roadMap.GetComponentsInChildren<Transform>(true)
            .Where(t => LayerMask.LayerToName(t.gameObject.layer) == "Road").Select(t => t.gameObject).ToArray();

        foreach (GameObject road in roadObjects)
        {
            Renderer renderer = road.GetComponent<Renderer>();
            if (renderer != null)
            {
                Vector3 center = renderer.bounds.center;
                spawnPositions.Add(new Vector3(center.x, center.y + 0.8f, center.z)); // 0.8f는 약간 위로 올리기 위해 사용
            }
        }
    }

    public void StartSpawningItems()
    {
        foreach(var items in pool.pools)
        {
            for(int i=0;i<items.size;i++)
            {
                SpawnItemRandomPos(items.tag);
            }
        }

    }

    private void SpawnItemRandomPos(string tag)
    {
        //중복으로 위치가 나오는 것을 방지하기 위해 Remove를 시도하는 중에,
        //모든 위치에 대한 정보가 사라졌을 경우 다시 List에 위치 정보를 초기화
        if(spawnPositions.Count == 0)
        {
            FindRoadPos();
        }

        Vector3 rndPos = spawnPositions[Random.Range(0, spawnPositions.Count)];
        GameObject item = pool.SpawnFromPool(tag);

        if(item != null)
        {
            item.transform.position = rndPos;
            item.SetActive(true);
        }

        //동일한 랜덤 값이 나올 수 있으므로 랜덤으로 나온 위치 정보는 List에서 삭제
        // → 보석이 리스폰 될 때 같은 위치에 두 아이템이 생성될 수 있기 때문
        spawnPositions.Remove(rndPos);

    }

    public void RespawnItem(GameObject item)
    {
        StartCoroutine(RespawnItemRoutine(item));
    }

    private IEnumerator RespawnItemRoutine(GameObject item)
    {
        yield return new WaitForSeconds(15f);
        item.transform.position = spawnPositions[Random.Range(0, spawnPositions.Count)];
        item.SetActive(true);
    }
}
