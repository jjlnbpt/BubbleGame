using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropletBehavior : MonoBehaviour
{

    public float disperseForceX;
    public float disperseForceY;

    public float MinLifetime;
    public float MaxLifetime;

    private Rigidbody2D rb;
    private Collider2D collider;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();

        float xForce = Random.Range(-disperseForceX, disperseForceX);
        float yForce = Random.Range(-disperseForceY, disperseForceY);

        Vector2 forceVector = new Vector2(xForce, yForce);
        rb.AddForce(forceVector, ForceMode2D.Impulse);

        Destroy(gameObject, Random.Range(MinLifetime, MaxLifetime));
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bubble"))
        {
            AudioManager.instance.CreateSplashInstance();
            Destroy(gameObject);
        } else
        {
            Physics2D.IgnoreCollision(collision.collider, collider);
        }
    }
}
