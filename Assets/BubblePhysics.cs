using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubblePhysics : MonoBehaviour
{

    public float MaxSendOffForce;

    private Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnSendOff();
        }
    }

    void OnSendOff()
    {
        Debug.Log("Sending off bubble");
        Vector2 SendOffVector = new Vector2(Random.Range(-MaxSendOffForce, MaxSendOffForce), Random.Range(-MaxSendOffForce, MaxSendOffForce));

        rb.AddForce(SendOffVector, ForceMode2D.Impulse);
    }


}
