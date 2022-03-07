
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private bool moveAllowed;
    private CircleCollider2D col;

    [Header("Particle Effects")]
    [SerializeField] private GameObject selectionEffect;
    [SerializeField] private GameObject deathEffect;

    private GameMaster gm;

    [Header("SFX")]
    [SerializeField] private AudioClip selectSound;
    [SerializeField] private AudioClip explosionSound;

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        col = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

            if(touch.phase == TouchPhase.Began)
            {
                Collider2D touchedCollider = Physics2D.OverlapPoint(touchPosition);
                if(col == touchedCollider)
                {
                    Instantiate(selectionEffect, transform.position, Quaternion.identity);
                    SoundManager.instance.PlaySound(selectSound);
                    moveAllowed = true;
                }
            }
            if (touch.phase == TouchPhase.Moved)
            {
                if (moveAllowed)
                {
                    transform.position = new Vector2(touchPosition.x, touchPosition.y);
                }
            }
            if (touch.phase == TouchPhase.Ended)
            {
                moveAllowed = false;
            }
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
            Destroy(gameObject);
        }
    }
}
