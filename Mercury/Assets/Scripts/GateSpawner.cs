using UnityEngine;

public class GateSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _spawnPrefab;

    [SerializeField]
    private float _spawnRateSeconds = 1.0f;

    private float _spawnTimer = 0.0f;

    [SerializeField]
    private float _numPerSpawn = 1.0f;

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
        for (int i = 0; i < _numPerSpawn; i++)
        {
            GameObject spawn = ObjectPool.Instance.GetPooledObject(_spawnPrefab);
            // TODO: Add random position range from spawner transform.

            spawn.transform.position = new Vector3(Random.Range(_spawnPoint.bounds.min.x, _spawnPoint.bounds.max.x),
                                                                _spawnPoint.bounds.center.y,
                                                   Random.Range(_spawnPoint.bounds.min.z, _spawnPoint.bounds.max.z));
            spawn.transform.rotation = Quaternion.identity;

            Gate gateComponent = spawn.GetComponent<Gate>();
            if (gateComponent != null)
            {
                gateComponent.OnSpawn();
            }
            else
            {
                Debug.LogError("GateSpawner: Spawned object does not have Gate component.");
            }
        }
    }

}
