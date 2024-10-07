using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
using DG.Tweening;
using UnityEngine.Serialization;

public class SpawnController : MonoBehaviour
{
    public float maxSpawnTime = 5f;
    public float minSpawnTime = 1f;
    public float difficultyDuration = 5f;
    public float scaleDecreaseRate = 1f;
    public GameObject[] spawnPointList;
    public AnimalController[] animalList;

    private float spawnTime;
    private float scaleRate = 1f;

    private void Start()
    {
        spawnTime = maxSpawnTime;
        StartCoroutine(spawnCoroutine());
    }

    private void Update()
    {
        DOTween.To(() => spawnTime, x => spawnTime = x, minSpawnTime, difficultyDuration)
            .SetEase(Ease.OutSine);
        // .OnUpdate(() => Debug.Log("spawnTime" + spawnTime));

        scaleRate = Mathf.Lerp(scaleRate, 0f, Time.deltaTime * (scaleDecreaseRate / 1000f));
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
        instance.gameObject.transform.localScale = new Vector3(scaleRate, scaleRate, scaleRate);
        
        var randPoint2 = Random.Range(0, newPoints.Count);

        instance.targetPosition = newPointsList[randPoint2].transform.position;
    }
}
