using UnityEngine;

public class ShootOtherPlanet : MonoBehaviour
{
    /// <summary>
    /// For a level where planets shoot each other
    /// </summary>

    [Header("Fire point")]
    [SerializeField] private Transform shotPos;

    [Header("Projectile Prefab")]
    [SerializeField] private GameObject projectile;
     private GameObject[] planets;


    private GameObject target;
    private GameObject targetedPlanet;
    private float timeBtwShot = 0.5f;
    private float startTimeBtwShot;

    [Header("Difficulty Parameters")]
    [SerializeField] private float secondsToMaxDifficulty;
    [SerializeField] private float slowestRateOfFire;
    [SerializeField] private float fastestRateOfFire;

    [Header("SFX")]
    [SerializeField] private AudioClip laserSound;
   

    private bool hasShoot;

    
    private void Start()
    {
        planets = GameObject.FindGameObjectsWithTag("Planet");
        TargetRandomPlanet();
    }
    private void Update()
    {

        if (targetedPlanet == null)
        {
            GetComponent<ShootOtherPlanet>().enabled = false;
            return;
        }

        if (hasShoot)
        {
            TargetRandomPlanet();
            hasShoot = false;
        }
        if (targetedPlanet == null) return;
        Vector2 direction = (Vector2)targetedPlanet.transform.position - (Vector2)transform.position;
       float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
       Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
       transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 10f * Time.deltaTime);
        
         

        if (timeBtwShot <= 0)
        {
            SoundManager.instance.PlaySound(laserSound);
            GameObject firedBullet = Instantiate(projectile, shotPos.position, shotPos.rotation);
            firedBullet.GetComponent<Projectile>().target = (Vector2)target.transform.position;

            hasShoot = true;
            timeBtwShot = startTimeBtwShot;
        }
        else
        {
            startTimeBtwShot = Mathf.Lerp(slowestRateOfFire, fastestRateOfFire, GetDifficultyPrecent());
            timeBtwShot -= Time.deltaTime;
        }
    }

    private void TargetRandomPlanet()
    {
        int random = Random.Range(0, planets.Length);
        while (planets[random] == gameObject) random = Random.Range(0, planets.Length);
        
        target = planets[random];
        targetedPlanet = planets[random];
    }

    private float GetDifficultyPrecent()
    {
        return Mathf.Clamp01(Time.timeSinceLevelLoad / secondsToMaxDifficulty);
    }
}
