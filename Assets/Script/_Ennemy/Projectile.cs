using UnityEngine;

public class Projectile : MonoBehaviour
{
    public bool HasHit { get; private set; }

    public void ResetStatus()
    {
        HasHit = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "_Chara")
        {
            // Détruire la cible si c'est bien _Chara
            Object.Destroy(collision.gameObject);

            HasHit = true;
            Deactivate();
        }
        else
        {
            // optionnel : gérer collisions avec décor
        }
    }

    private void Deactivate() 
    {
        var rb = GetComponent<Rigidbody2D>(); 
        if (rb != null) 
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
        gameObject.SetActive(false);
    }
}