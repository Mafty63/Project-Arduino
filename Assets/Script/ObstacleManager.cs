using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [SerializeField] private ObjectPool pipePool;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float topY = 3f;
    [SerializeField] private float bottomY = -3f;
    [SerializeField] private float moveSpeed = 2f;

    private float timer;
    private bool spawnPaused = false;

    private void Start()
    {
        timer = spawnInterval;
    }

    private void Update()
    {
        if (!spawnPaused)
        {
            timer += Time.deltaTime;
            if (timer >= spawnInterval)
            {
                SpawnObstacle();
                timer = 0f;
            }
        }
    }

    private void SpawnObstacle()
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

    public void SpawnStopped(bool cond) => spawnPaused = cond;


}
