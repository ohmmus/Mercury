using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 10.0f;

    private Rigidbody _rbRef;

    void Start()
    {
        _rbRef = GetComponent<Rigidbody>();
    }

    void Update()
    {
        _rbRef.MovePosition(transform.position + new Vector3(0.0f, 0.0f, _moveSpeed * Time.deltaTime));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag.Equals("Despawn"))
        {
            ObjectPool.Instance.ReturnObjectToPool(gameObject);
        }
    }
}
