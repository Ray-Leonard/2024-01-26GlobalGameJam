using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] pickupPrefabList;

    [SerializeField] int maxItem;

    [SerializeField] Vector2 spawnIntervalMinMax;

    private float spawnInterval;
    private float timer = 0;



    private void Update()
    {
        timer += Time.deltaTime;

        if(timer > spawnInterval)
        {
            timer = 0;
            spawnInterval = Random.Range(spawnIntervalMinMax.x, spawnIntervalMinMax.y);

            SpawnPickup();
        }
    }

    private void SpawnPickup()
    {
        // count current item number
        int count = 0;
        foreach(Transform t in transform)
        {
            count += t.childCount;
        }

        if(count < maxItem)
        {
            int randomPositionIndex = Random.Range(0, transform.childCount);
            while(transform.GetChild(randomPositionIndex).childCount > 0)
            {
                randomPositionIndex = Random.Range(0, transform.childCount);
            }

            Transform spawnPoint = transform.GetChild(randomPositionIndex);
            Instantiate(pickupPrefabList[Random.Range(0, pickupPrefabList.Length)], spawnPoint);
        }
    }
}
