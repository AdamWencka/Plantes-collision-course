using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormMove : MonoBehaviour
{
    [Header("Path Patrol Parameters")]
    [SerializeField] private float minXPos;
    [SerializeField] private float maxXPos;
    [SerializeField] private float minYPos;
    [SerializeField] private float maxYPos;

    [SerializeField] private float speed;

    
    [Header("Particle Effects")]
    [SerializeField] private GameObject deathEffect;

    private GameMaster gm;

    Vector2 targetPosition;

    [Header("SFX")]
    [SerializeField] private AudioClip explosionSound;

    private float degrees;
    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        targetPosition = GetRandomPosition();
    }
    private void Update()
    {
        degrees += 0.5f;
        transform.rotation = Quaternion.Euler(Vector3.forward * degrees);
        

        if((Vector2)transform.position != targetPosition)
        {
            
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
        else
        {
            Destroy(gameObject);
        }
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
    private Vector2 GetRandomPosition()
    {
        float randomXPos = Random.Range(minXPos, maxXPos);
        float randomYPos = Random.Range(minYPos, maxYPos);

        return new Vector2(randomXPos, randomYPos);
    }
    private void DestroyStorm()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, targetPosition);
    }

}
