using UnityEngine;

public class Projectile : MonoBehaviour
{
    /// <summary>
    /// For a level where planets shoot each other
    /// </summary>

 

    public Vector2 target;
    [Header("Projectile speed")]
    [SerializeField]private float speed;



    [Header("SFX")]
    [SerializeField] private AudioClip explosionSound;

    [Header("Particle Effects")]
    [SerializeField] private GameObject deathEffect;

    private GameMaster gm;

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();

        Vector2 direction = target- (Vector2)transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;
    }

    private void Update()
    {
        
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, target) < 0.2f)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Planet"))
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            SoundManager.instance.PlaySound(explosionSound);
            gm.GameOver();
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }


}
