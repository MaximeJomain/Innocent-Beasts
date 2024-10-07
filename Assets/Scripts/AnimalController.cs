using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AnimalController : MonoBehaviour
{
    public Vector3 targetPosition = Vector3.zero;
    [FormerlySerializedAs("speed")]
    public float moveSpeed = 1f;
    [HideInInspector] public Animator animator;
    public Sprite interactionSprite;
    public float spriteSpeed = 1f;

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
        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

        if (transform.position == targetPosition) Destroy(gameObject);
    }

    private IEnumerator InteractionCoroutine()
    {
        spriteRenderer.sprite = interactionSprite;
        yield return new WaitForSeconds(spriteSpeed);
        spriteRenderer.sprite = baseSprite;
        yield return null;
    }
}
