using UnityEngine;

public class Storm : MonoBehaviour
{
    [Header("Particle Effects")]
    [SerializeField] private GameObject deathEffect;

    private GameMaster gm;

    [Header("SFX")]
    [SerializeField] private AudioClip explosionSound;
    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Planet")
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            SoundManager.instance.PlaySound(explosionSound);

            gm.GameOver();
            

            Destroy(collision.gameObject);  
        }
    }
    
    private void DestroyStorm()
    {
        Destroy(gameObject);
    }
}
