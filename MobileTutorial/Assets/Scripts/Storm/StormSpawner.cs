using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormSpawner : MonoBehaviour
{
    [Header("StormBegin Prefab")]
    [SerializeField] private GameObject stormBegin;

    [Header("Storm Prefab")]
    [SerializeField] private GameObject storm;

    [Header("Spawn Space Parameters")]
    [SerializeField] private float minXPos;
    [SerializeField] private float maxXPos;
    [SerializeField] private float minYPos;
    [SerializeField] private float maxYPos;

    [Header("Difficulty Parameters")]
    [SerializeField] private float secondsToMaxDifficulty;
    [SerializeField] private float minRespawnTime;
    [SerializeField] private float maxRespawnTime;

    [Header("SFX")]
    [SerializeField] private AudioClip stormSound;

    [SerializeField] private bool isMoving;
    private float respawnTime;

    private float randomXPos;
    private float randomYPos;
    private GameMaster gm;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        StartCoroutine(StormWave());
    }

    
    void Update()
    {
        if (gm.getHasLost())
        {
            StopAllCoroutines();
        }
        respawnTime = Mathf.Lerp(maxRespawnTime, minRespawnTime, GetDifficultyPrecent());
    }
    private void SpawnStorm()
    {
        if (!isMoving) { 
        SoundManager.instance.PlaySound(stormSound);
        }
        Instantiate(storm, new Vector2(randomXPos, randomYPos), Quaternion.identity);
    }

    private void RandomizePosition()
    {
        randomXPos = Random.Range(minXPos, maxXPos);
        randomYPos = Random.Range(minYPos, maxYPos);
    }
    private void SpawnStormBegin()
    {
        Instantiate(stormBegin, new Vector2(randomXPos, randomYPos), Quaternion.identity);
    }

    IEnumerator StormWave()
    {
        while (true)
        {
            RandomizePosition();
            yield return new WaitForSeconds(respawnTime);
            SpawnStormBegin();
            yield return new WaitForSeconds(1.5f);
            SpawnStorm();
        }
    }

    private float GetDifficultyPrecent()
    {
        return Mathf.Clamp01(Time.timeSinceLevelLoad / secondsToMaxDifficulty);
    }
}
