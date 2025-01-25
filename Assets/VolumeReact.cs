using System.Linq;
using UnityEngine;

public class VolumeReact : MonoBehaviour
{
    [SerializeField] float alpha = 0.9f;
    private Vector3 m_originalPosition;
    float dy = 0;

    // Start is called before the first frame update
    void Start()
    {
        m_originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        dy = dy * (alpha) + AudioManager.instance.samples.AsEnumerable<float>().Average() * (1 - alpha);
        transform.position = m_originalPosition + new Vector3(0.0f, dy * 2000, 0.0f);
    }
}
