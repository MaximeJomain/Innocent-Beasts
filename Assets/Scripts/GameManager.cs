using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject cursor;
    
    private Camera cam;
    private Animator cursorAnimator;

    private void Awake()
    {
        cursorAnimator = cursor.GetComponent<Animator>();
    }

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        cursor.transform.position = mousePosition;
        
        if (Input.GetMouseButtonDown(0))
        {
            cursorAnimator.SetTrigger("click");
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null)
            {
                Debug.Log(hit.collider.name);
                var animal = hit.collider.GetComponent<AnimalController>();
                if (animal)
                {
                    Debug.Log("ANIMAL");
                    animal.PlayInteraction();
                }
            }
        }
    }
}
