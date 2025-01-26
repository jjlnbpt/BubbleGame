using UnityEngine;

public class MoveUp : MonoBehaviour
{
    public float speed = 1.0f;

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.position = new Vector3(transform.position.x, transform.position.y + speed * Time.fixedDeltaTime, 0.0f);
    }
}
