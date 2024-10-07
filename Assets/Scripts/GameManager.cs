using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int baseHealth = 100;
    public int heal = 3, damage = 1;
    public GameObject cursor;
    public Slider healthSlider;
    public GameObject gameOver;
    private int health;
    
    private Camera cam;
    private Animator cursorAnimator;
    private SpawnController spawnController;

    public void ChangeHealth(int value)
    {
        health += value;
        health = Math.Clamp(health, 0, baseHealth);
        healthSlider.value = health;
        Debug.Log("HEALTH " + health);
    }

    private void Awake()
    {
        cursorAnimator = cursor.GetComponent<Animator>();
        spawnController = GameObject.Find("SpawnPoints").GetComponent<SpawnController>();
    }

    private void Start()
    {
        Time.timeScale = 1f;
        
        cam = Camera.main;
        healthSlider.value = baseHealth;
        health = baseHealth;
        gameOver.SetActive(false);

        spawnController.damage = damage;
        spawnController.heal = heal;
    }

    private void Update()
    {
        if (health <= 0) GameOver();
        
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

    private void GameOver()
    {
        gameOver.SetActive(true);
        Time.timeScale = 0f;
    }
}
