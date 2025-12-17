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

    private float _shotTimer = 0.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _transformRef = transform;
        _shotTimer = _secondsPerShot;
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
}
