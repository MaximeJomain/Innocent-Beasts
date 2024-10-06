using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalController : MonoBehaviour
{
    public Vector3 targetPosition = Vector3.zero;
    public float speed = 1f;
    
    private void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

        if (transform.position == targetPosition) Destroy(gameObject);
    }
}
