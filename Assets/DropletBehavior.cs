using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropletBehavior : MonoBehaviour
{

    public float disperseForceX;
    public float disperseForceY;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        float xForce = Random.Range(-disperseForceX, disperseForceX);
        float yForce = Random.Range(-disperseForceY, disperseForceY);

        Vector2 forceVector = new Vector2(xForce, yForce);
        rb.AddForce(forceVector, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bubble"))
        {
            AudioManager.instance.CreateSplashInstance();
            Destroy(gameObject);
        }
    }
}
