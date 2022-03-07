using UnityEngine;

public class MovePlanetsTowardsEachOther : MonoBehaviour
{
    /// <summary>
    ///  For a level where planets are pulling towards each other
    /// </summary>
    [SerializeField]private Transform otherPlanet;

    private float speed;


    [Header("Difficulty Parameters")]
    [SerializeField] private float secondsToMaxDifficulty;
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;

    void Update()
    {
        if (otherPlanet == null) return;
        speed = Mathf.Lerp(minSpeed, maxSpeed, GetDifficultyPrecent());
        transform.position = Vector2.MoveTowards(transform.position, otherPlanet.position, speed * Time.deltaTime);
    }
    private void OnDrawGizmos()
    {
        if (otherPlanet == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, otherPlanet.position);
    }
    private float GetDifficultyPrecent()
    {
        return Mathf.Clamp01(Time.timeSinceLevelLoad / secondsToMaxDifficulty);
    }
}
