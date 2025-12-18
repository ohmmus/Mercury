using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;

    [SerializeField]
    private float _spawnRateSeconds = 1.0f;

    private float _spawnTimer = 0.0f;

    [SerializeField]
    private float _numEnemiesPerSpawn = 1.0f;

    [SerializeField]
    private Transform _spawnPoint;

    void Start()
    {
    }

    void Update()
    {
        _spawnTimer -= Time.deltaTime;

        if (_spawnTimer <= 0.0f)
        {
            for (int i = 0; i < _numEnemiesPerSpawn; i++)
            {
                GameObject enemy = ObjectPool.Instance.GetObjectFromPool(_enemyPrefab);
                // TODO: Add random position range from spawner transform.
                enemy.transform.position = _spawnPoint.position;
                enemy.transform.rotation = _spawnPoint.rotation;
            }

            _spawnTimer = _spawnRateSeconds;
        }
    }


}
