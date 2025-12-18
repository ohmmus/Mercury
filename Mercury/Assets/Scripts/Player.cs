using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Transform _transformRef;

    [SerializeField]
    private float _moveSpeed = 5.0f;

    [SerializeField]
    private float _secondsPerShot = 1.0f;

    [SerializeField]
    private GameObject _bulletPrefab;

    [SerializeField]
    private int _maxHealth = 10;

    private int _currentHealth = 10;

    private float _shotTimer = 0.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _transformRef = transform;
        _shotTimer = _secondsPerShot;
    }

    void OnEnable()
    {
        Spawn();  
    }

    void Spawn()
    {
        _currentHealth = _maxHealth;    
    }

    void Death()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.aKey.isPressed)
        {
            _transformRef.position += new Vector3(-_moveSpeed * Time.deltaTime, 0.0f, 0.0f);
        }
        else if (Keyboard.current.dKey.isPressed)
        {
            _transformRef.position += new Vector3(_moveSpeed * Time.deltaTime, 0.0f, 0.0f);
        }

        // Update Shooting
        _shotTimer -= Time.deltaTime;
        if (_shotTimer <= 0.0f)
        {
            _shotTimer = _secondsPerShot; 
            Shoot();
        }
    }

    void Shoot()
    {
       GameObject bullet = ObjectPool.Instance.GetPooledObject(_bulletPrefab);
       bullet.transform.position = _transformRef.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag.Equals("Enemy"))
        {
            _currentHealth -= 1;

            if (_currentHealth <= 0)
            {
               Death(); 
            }
        }
    }
}
