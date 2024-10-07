using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int baseHealth = 100;
    public GameObject cursor;
    public Slider healthSlider;
    private int health;
    
    private Camera cam;
    private Animator cursorAnimator;

    public void ChangeHealth(int value)
    {
        health += value;
        healthSlider.value = health;
    }

    private void Awake()
    {
        cursorAnimator = cursor.GetComponent<Animator>();
    }

    private void Start()
    {
        cam = Camera.main;
        healthSlider.value = baseHealth;
        health = baseHealth;
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
                var animal = hit.collider.GetComponent<AnimalController>();
                if (animal)
                {
                    animal.PlayInteraction();
                }
            }
        }
    }
}
