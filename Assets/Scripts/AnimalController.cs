using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalController : MonoBehaviour
{
    public Vector3 targetPosition = Vector3.zero;
    public float speed = 1f;
    [HideInInspector] public Animator animator;
    public Sprite interactionSprite;

    private SpriteRenderer spriteRenderer;
    private Sprite baseSprite;

    public void PlayInteraction()
    {
        // animator.SetTrigger("interaction");
        StartCoroutine(InteractionCoroutine());
    }

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        baseSprite = spriteRenderer.sprite;
    }

    private void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

        if (transform.position == targetPosition) Destroy(gameObject);
    }

    private IEnumerator InteractionCoroutine()
    {
        spriteRenderer.sprite = interactionSprite;
        yield return new WaitForSeconds(1f);
        spriteRenderer.sprite = baseSprite;
        yield return null;
    }
}
