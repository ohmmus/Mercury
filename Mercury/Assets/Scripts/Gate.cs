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

    public class Powerup
    {
        float shotsPerSecond;
    }

    private StatusEffect _statusEffect = null;
    private Powerup _powerup = null;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void Spawn()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag.Equals("Player"))
        {
            ApplyStatusToPlayer();
        }
    }

    void ApplyStatusToPlayer()
    {

    }
}
