using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;


public class Gate : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro _statusText;

    public enum Operation
    {
        Add,
        Subtract,
        Multiply,
        Divide
    }

    public class StatusEffect
    {
        public Operation statusOperation; 
        public int count;
    }

    [SerializeField]
    private StatusEffect _statusEffect = null;
    public StatusEffect statusEffect
    { get { return _statusEffect; } }

    [SerializeField]
    private float _moveSpeed = 3.0f;

    private Rigidbody _rbRef;

    void Start()
    {
        _rbRef = GetComponent<Rigidbody>();
        OnSpawn();
    }

    void Update()
    {
        _rbRef.MovePosition(transform.position + new Vector3(0.0f, 0.0f, -_moveSpeed * Time.deltaTime));
    }

    public void OnSpawn()
    {
        if (_statusEffect == null)
        {
            _statusEffect = new StatusEffect();    
        }
            
        _statusEffect.statusOperation = (Operation)Random.Range(0, 3); // check enum range. 
        _statusEffect.count = Random.Range(1, 4);

        UpdateText();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag.Equals("Bullet"))
        {
            if (_statusEffect.statusOperation == Operation.Subtract)
            {
                _statusEffect.count -= 1;
                if (_statusEffect.count == 0)
                {
                    _statusEffect.statusOperation = Operation.Add;
                }
            }
            else if (_statusEffect.statusOperation == Operation.Add)
            {
                _statusEffect.count += 1;
            }
            else if (_statusEffect.statusOperation == Operation.Multiply)
            {
                _statusEffect.count *= 1; 
            }

            UpdateText();
        }
    }

    void UpdateText()
    {
        // Set text 
        StringBuilder sb = new StringBuilder();
        
        switch(_statusEffect.statusOperation)
        {
            case Operation.Add:
                sb.Append("+");
                break;
            case Operation.Subtract:
                sb.Append("-");
                break;
            case Operation.Multiply:
                sb.Append("x");
                break;
            case Operation.Divide:
                sb.Append("÷");
                break;
        }

        sb.Append(_statusEffect.count.ToString());
        _statusText.text = sb.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                player.ApplyStatus(statusEffect);
            }

            // Return the gate object to the pool
            ObjectPool.Instance.ReturnObjectToPool(gameObject);
        }
    }
}
