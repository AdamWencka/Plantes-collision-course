using UnityEngine;

public class CannonTower : MonoBehaviour
{
    [Header("Firepoint")]
    [SerializeField] private Transform firepoint;

    [Header("Speed of Shell")]
    [SerializeField] private float speed;

    [Header("Shell Prefab")]
    [SerializeField] private GameObject cannonShell;

    [Header("SFX")]
    [SerializeField] private AudioClip bulletFireSound;

    [Header("Particle Effects")]
    [SerializeField] private GameObject smokeEffect;

    private Vector2 lookDirection;
    private float lookAngle;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            Vector2 lookDirection = Camera.main.ScreenToWorldPoint(touch.position) - transform.position;
            lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, lookAngle - 90f);

            if(touch.phase == TouchPhase.Began)
            {
                FireShell();
            }
            
        }
    }

    private void FireShell()
    {
        Instantiate(smokeEffect, firepoint.position, Quaternion.identity);
        SoundManager.instance.PlaySound(bulletFireSound);
        GameObject firedShell = Instantiate(cannonShell, firepoint.position, firepoint.rotation);
        firedShell.GetComponent<Shell>().target = lookDirection;
        firedShell.GetComponent<Rigidbody2D>().velocity = firepoint.up * speed;
    }
}
