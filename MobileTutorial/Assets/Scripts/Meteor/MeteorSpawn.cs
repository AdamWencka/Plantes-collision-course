using System.Collections;
using UnityEngine;

public class MeteorSpawn : MonoBehaviour
{
    [Header("Meteor Prefab")]
    [SerializeField] private GameObject meteor;

    [Header("Spawn Space Parameters")]
    [SerializeField] private float minXPos;
    [SerializeField] private float maxXPos;
    [SerializeField] private float yPos;

    [Header("Difficulty Parameters")]
    [SerializeField] private float secondsToMaxDifficulty;
    [SerializeField] private float minRespawnTime;
    [SerializeField] private float maxRespawnTime;

    private float respawnTime;

    private GameMaster gm;

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        StartCoroutine(MeteorWave());
    }

    private void Update()
    {
        if (gm.getHasLost())
        { 
            StopAllCoroutines();
        }
        respawnTime= Mathf.Lerp(maxRespawnTime, minRespawnTime, GetDifficultyPrecent());
    }

    private void SpawnMeteor()
    {
        float randomXPos = Random.Range(minXPos, maxXPos);
        Instantiate(meteor, new Vector2(randomXPos, yPos), Quaternion.identity);
    }

    IEnumerator MeteorWave()
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnTime);
            SpawnMeteor();
        }
    }

    private float GetDifficultyPrecent()
    {
        return Mathf.Clamp01(Time.timeSinceLevelLoad / secondsToMaxDifficulty);
    }
}
