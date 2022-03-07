using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomPathPatrol : MonoBehaviour
{
    [Header("Path Patrol Parameters")]
    [SerializeField] private float minXPos;
    [SerializeField] private float maxXPos;
    [SerializeField] private float minYPos;
    [SerializeField] private float maxYPos;


    private float speed;

    [Header("Difficulty Parameters")]
    [SerializeField] private float secondsToMaxDifficulty;
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;


    Vector2 targetPosition;



    private void Start()
    {
        targetPosition = GetRandomPosition();
    }

    private void Update()
    {
        if ((Vector2)transform.position != targetPosition)
        {
            speed = Mathf.Lerp(minSpeed, maxSpeed, GetDifficultyPrecent());
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
        else
        {
            targetPosition = GetRandomPosition();
        }
    }
    private Vector2 GetRandomPosition()
    {
        float randomXPos = Random.Range(minXPos, maxXPos);
        float randomYPos = Random.Range(minYPos, maxYPos);

        return new Vector2(randomXPos, randomYPos);
    }



    private float GetDifficultyPrecent()
    {
        return Mathf.Clamp01(Time.timeSinceLevelLoad / secondsToMaxDifficulty);
    }
}
