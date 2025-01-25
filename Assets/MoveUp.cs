using UnityEngine;

public class MoveUp : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(transform.position.x, transform.position.y + 0.01f, 0.0f);
    }
}
