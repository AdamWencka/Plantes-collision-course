using UnityEngine;

public class Shell : MonoBehaviour
{

    public Vector2 target;

    [Header("SFX")]
    [SerializeField] private AudioClip explosionSound;

    [Header("Particle Effects")]
    [SerializeField] private GameObject deathEffect;

    [Header("Object Life Time")]
    [SerializeField] private float shellLifeTime = 2f;
    
    void Update()
    {
        shellLifeTime -= Time.deltaTime;
        if(shellLifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Meteor"))
        { 
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        SoundManager.instance.PlaySound(explosionSound);
        Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
