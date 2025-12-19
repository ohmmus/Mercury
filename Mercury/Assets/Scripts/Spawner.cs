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
    private Collider _spawnPoint;

    void Start()
    {
    }

    void Update()
    {
        _spawnTimer -= Time.deltaTime;

        if (_spawnTimer <= 0.0f)
        {
            Spawn();
            _spawnTimer = _spawnRateSeconds;
        }
    }

    void Spawn()
    {
        for (int i = 0; i < _numEnemiesPerSpawn; i++)
        {
            GameObject enemy = ObjectPool.Instance.GetPooledObject(_enemyPrefab);
            // TODO: Add random position range from spawner transform.
            Rigidbody test = enemy.GetComponent<Rigidbody>();

            enemy.transform.position = new Vector3(Random.Range(_spawnPoint.bounds.min.x, _spawnPoint.bounds.max.x),
                                                                _spawnPoint.bounds.center.y,
                                                   Random.Range(_spawnPoint.bounds.min.z, _spawnPoint.bounds.max.z));
            enemy.transform.rotation = Quaternion.identity;
        }
    }

}
