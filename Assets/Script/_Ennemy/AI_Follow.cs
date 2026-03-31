using Unity.VisualScripting;
using UnityEngine;

public class AI_Follow : MonoBehaviour
{
    public GameObject player;
    public float speed;

    private float distance;





    void Start()
    {
        
    }

    private void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
       // Debug.Log("Collision detected with: " + collision.gameObject.name);
       if (collision.gameObject.name == "_Chara")
        {
            Destroy(collision.gameObject);
        }
        

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction  = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);
    }
}
