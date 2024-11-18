using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [SerializeField] private ObjectPool pipePool;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float topY = 3f;
    [SerializeField] private float bottomY = -3f;
    [SerializeField] private float moveSpeed = 2f;

    private float timer;
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnObstacle();
            timer = 0f;
        }
    }

    void SpawnObstacle()
    {
        GameObject pipe = pipePool.GetObject();
        if (pipe != null)
        {

            pipe.transform.position = new Vector3(transform.position.x, Random.Range(bottomY, topY), 0);

            Obstacle movement = pipe.GetComponent<Obstacle>();
            if (movement != null)
            {
                movement.SetSpeed(moveSpeed);
            }

        }
    }

}
