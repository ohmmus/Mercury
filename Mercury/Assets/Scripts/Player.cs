using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject _playerPrefab = null;

    Transform _transformRef;

    [SerializeField]
    private float _moveSpeed = 5.0f;

    [SerializeField]
    private float _secondsPerShot = 1.0f;

    [SerializeField]
    private GameObject _bulletPrefab;

    private float _shotTimer = 0.0f;

    private Stack<Player> _SpawnedPlayers = new Stack<Player>();

    public int PlayerCount
    {
        get { return _SpawnedPlayers.Count; }
    }

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
      
    }

    void Death()
    {
        SceneManager.LoadScene("GameOver");
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

    void SetNumberOfPlayers(int numPlayers)
    {
        while (PlayerCount != numPlayers)
        {
            if (numPlayers < PlayerCount)
            {
                RemoveAPlayer();
            }
            else
            {
                AddAPlayer();
            }
        }
    }

    void AddAPlayer()
    {
        GameObject newPlayer = ObjectPool.Instance.GetPooledObject(_playerPrefab);
        _SpawnedPlayers.Push(newPlayer.GetComponent<Player>()); 
    }

    void RemoveAPlayer(bool allowDeath = false)
    {
        Player killedPlayer = null;

        if (allowDeath && PlayerCount == 0)
        {
            Death();
        }

        if (_SpawnedPlayers.TryPop(out killedPlayer))
        {
            ObjectPool.Instance.ReturnObjectToPool(killedPlayer.gameObject);
        }
    }

    public void ApplyStatus(Gate.StatusEffect statusEffect)
    {

        if (statusEffect == null)
        {
            Debug.LogError("Player: ApplyStatus called with null statusEffect.");
        }
        switch(statusEffect.statusOperation)
        {
            case Gate.Operation.Add:      SetNumberOfPlayers(PlayerCount + statusEffect.count); break;
            case Gate.Operation.Subtract: SetNumberOfPlayers(PlayerCount - statusEffect.count);  break;
            case Gate.Operation.Multiply: SetNumberOfPlayers(PlayerCount * statusEffect.count);  break;
            case Gate.Operation.Divide:   SetNumberOfPlayers(PlayerCount / statusEffect.count);  break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag.Equals("Enemy"))
        {
            RemoveAPlayer(allowDeath: true);
        }
    }
}
