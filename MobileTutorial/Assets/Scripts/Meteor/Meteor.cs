using UnityEngine;

public class Meteor : MonoBehaviour
{
    [Header("Fall Space Parameters")]
    [SerializeField] private float minXPos;
    [SerializeField] private float maxXPos;
    [SerializeField] private float minYPos;
    [SerializeField] private float maxYPos;

    
    [Header("Meteor Speed")]
    [SerializeField] private float speed;
    [SerializeField] private bool isStatic;

    [Header("Particle Effects")]
    [SerializeField] private GameObject deathEffect;

    [Header("SFX")]
    [SerializeField] private AudioClip explosionSound;

    private Vector2 targetPosition;

    private Rigidbody2D rb;

    private GameMaster gm;

    private void Start()
    {
        targetPosition = GetRandomPosition();
        rb = GetComponent<Rigidbody2D>();
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    void Update()
    {
        if (!isStatic)
        {
            if ((Vector2)transform.position != targetPosition)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            }
        }
    }
    private void FixedUpdate()
    {
        rb.rotation += 1.0f;
    }

    private Vector2 GetRandomPosition()
    {
        float randomXPos = Random.Range(minXPos, maxXPos);
        float randomYPos = Random.Range(minYPos, maxYPos);

        return new Vector2(randomXPos, randomYPos);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Planet")
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            SoundManager.instance.PlaySound(explosionSound);

            gm.GameOver();

            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
