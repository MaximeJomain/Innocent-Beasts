using JetBrains.Annotations;
using System.Collections;
using UnityEngine;

public class AnimalController : MonoBehaviour
{
    public Vector3 targetPosition = Vector3.zero;
    public float moveSpeed = 1f;
    [HideInInspector] public Animator animator;
    public Sprite interactionSprite;
    public float spriteSpeed = 1f;
    [CanBeNull]
    public ParticleSystem particle;
    public bool isEnemy;
    public float attackRate = 1.5f;
    public int attackDamage = 2, healValue = 8, clickDamage = 4;
    public AudioClip bounceAudio;
    public AudioClip[] interactionAudioList;

    private SpriteRenderer spriteRenderer;
    private Sprite baseSprite;
    private bool canInteract = true;

    private GameManager gameManager;
    public AudioSource bounceAS, interactAS;

    public void PlayInteraction()
    {
        // animator.SetTrigger("interaction");
        if (canInteract) StartCoroutine(InteractionCoroutine());
    }

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        baseSprite = spriteRenderer.sprite;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Start()
    {
        StartCoroutine(BounceCoroutine());
        if (isEnemy) StartCoroutine(EnemyCoroutine());
    }

    private IEnumerator BounceCoroutine()
    {
        bounceAS.PlayOneShot(bounceAudio);
        yield return new WaitForSeconds(1f);
        StartCoroutine(BounceCoroutine());
        yield return null;
    }

    private void Update()
    {
        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

        if (transform.position == targetPosition) Destroy(gameObject);
    }

    private IEnumerator InteractionCoroutine()
    {
        canInteract = false;
        spriteRenderer.sprite = interactionSprite;
        interactAS.PlayOneShot(interactionAudioList[Random.Range(0, interactionAudioList.Length)], .5f);
        if (isEnemy)
        {
            gameManager.ChangeHealth(-clickDamage);
        }
        else
        {
            gameManager.ChangeHealth(healValue);
        }

        if (particle)
        {
            particle.Play();
        }
        yield return new WaitForSeconds(spriteSpeed);
        spriteRenderer.sprite = baseSprite;
        
        yield return new WaitForSeconds(0.5f);
        canInteract = true;
        yield return null;
    }
    
    private IEnumerator EnemyCoroutine()
    {
        yield return new WaitForSeconds(attackRate);
        spriteRenderer.sprite = interactionSprite;
        interactAS.PlayOneShot(interactionAudioList[Random.Range(0, interactionAudioList.Length)], .5f);
        gameManager.ChangeHealth(-attackDamage);
        yield return new WaitForSeconds(0.5f);
        spriteRenderer.sprite = baseSprite;
        StartCoroutine(EnemyCoroutine());
        yield return null;
    }
}
