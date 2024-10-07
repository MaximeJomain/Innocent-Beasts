using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public TMP_Text timerText;
    
    private Camera cam;
    private Animator cursorAnimator;
    private SpawnController spawnController;
    private float elapsedTime;

    public void ChangeHealth(int value)
    {
        health += value;
        health = Math.Clamp(health, 0, baseHealth);
        healthSlider.value = health;
    }

    private void Awake()
    {
        cursorAnimator = cursor.GetComponent<Animator>();
        spawnController = GameObject.Find("SpawnPoints").GetComponent<SpawnController>();
    }

    private void Start()
    {
        Time.timeScale = 1f;
        Cursor.visible = false;

        elapsedTime = 0f;
        
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

        elapsedTime += Time.deltaTime;
        
        // Handle timer
        int minutes = Mathf.FloorToInt(elapsedTime / 60F);
        int seconds = Mathf.FloorToInt(elapsedTime - minutes * 60);
        string formattedTime = string.Format("{0:00}:{1:00}", minutes, seconds);
        timerText.text = formattedTime;
        
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
