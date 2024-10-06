using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnController : MonoBehaviour
{
    public float spawnTime = 5f;
    public GameObject[] spawnPointList;
    public AnimalController[] animalList;

    private void Start()
    {
        StartCoroutine(spawnCoroutine());
    }

    private IEnumerator spawnCoroutine()
    {
        yield return new WaitForSeconds(spawnTime);
        Spawn();
        StartCoroutine(spawnCoroutine());
        yield return null;
    }

    private void Spawn()
    {
        var randAnimal = Random.Range(0, animalList.Length);
        var randPoint = Random.Range(0, spawnPointList.Length);

        var newPoints = new HashSet<GameObject>();

        foreach (var point in spawnPointList)
        {
            newPoints.Add(point);
        }

        newPoints.Remove(spawnPointList[randPoint]);
        var newPointsList = newPoints.ToList();

        var instance = Instantiate(animalList[randAnimal], spawnPointList[randPoint].transform.position, Quaternion.identity);
        var randPoint2 = Random.Range(0, newPoints.Count);

        instance.targetPosition = newPointsList[randPoint2].transform.position;
    }
}
