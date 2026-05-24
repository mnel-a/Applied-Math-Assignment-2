using UnityEngine;

public class CreatureSpawner : MonoBehaviour
{
    public GameObject quadEnemy;
    public GameObject cubeEnemy;

    public Transform quadSpawnPoint;
    public Transform quadControlPoint;

    public Transform cubeSpawnPoint;
    public Transform cubeControlPoint1;
    public Transform cubeControlPoint2;

    public Transform targetPoint;

    public float spawnInterval = 2f;
    private float timer;


    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnQuadEnemy();
            SpawnCubeEnemy();
            timer = 0f;
        }
    }

    void SpawnQuadEnemy()
    {
        GameObject enemy = Instantiate(quadEnemy, quadSpawnPoint.position, Quaternion.identity);
        QuadraticEnemy movement = enemy.GetComponent<QuadraticEnemy>();

        if (movement != null)
        {
            movement.p1 = quadSpawnPoint;
            movement.p2 = quadControlPoint;
            movement.p3 = targetPoint;
        }   
    }

    void SpawnCubeEnemy()
    {
        GameObject enemy = Instantiate(cubeEnemy, cubeSpawnPoint.position, Quaternion.identity);
        CubicEnemy movement = enemy.GetComponent<CubicEnemy>();

        if (movement != null)
        {
            movement.p1 = cubeSpawnPoint;
            movement.p2 = cubeControlPoint1;
            movement.p3 = cubeControlPoint2;
            movement.p4 = targetPoint;
        }
    }

}
