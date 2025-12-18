using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 3.0f;

    private Rigidbody _rbRef;

    [SerializeField]
    private int _hitPoints = 1;

    [SerializeField]
    private int _maxHitPoints = 1;

    void Start()
    {
        _rbRef = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        Spawn();
    }

    void Update()
    {
        _rbRef.MovePosition(transform.position + new Vector3(0.0f, 0.0f, -_moveSpeed * Time.deltaTime));
    }

    void Spawn()
    {
        _hitPoints = _maxHitPoints;
    }

    void Death()
    {
        ObjectPool.Instance.ReturnObjectToPool(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag.Equals("Despawn"))
        {
            ObjectPool.Instance.ReturnObjectToPool(gameObject);
        }
        else if (collision.collider.tag.Equals("Bullet"))
        {
            _hitPoints -= 1;

            if (_hitPoints <= 0)
            {
                Death();
            }
        }
        else if (collision.collider.tag.Equals("Player"))
        {
            ObjectPool.Instance.ReturnObjectToPool(gameObject);
        }
    }
}
