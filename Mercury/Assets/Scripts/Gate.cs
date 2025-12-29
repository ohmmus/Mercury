using UnityEngine;

public class Gate : MonoBehaviour
{
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

    private StatusEffect _statusEffect = null;
    public StatusEffect statusEffect
    { get { return _statusEffect; } }

    [SerializeField]
    private float _moveSpeed = 3.0f;

    private Rigidbody _rbRef;

    void Start()
    {
        _rbRef = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        if (_statusEffect != null)
        {
            _statusEffect = new StatusEffect
            {
                    
            };
        }    
    }

    void Update()
    {
        _rbRef.MovePosition(transform.position + new Vector3(0.0f, 0.0f, -_moveSpeed * Time.deltaTime));
    }

    void Spawn()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        // TODO: bullets change the status.
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
        }
    }

}
