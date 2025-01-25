using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;

public class OnClick : MonoBehaviour
{
    public float radius = 1.0f;
    private Camera m_camera;

    [Header("Events")]
    [Description("A simple pop event that provides no arguments")]
    public UnityEvent onPopSimple;
    [Description("A simple pop event that provides the radius of the bubble popped")]
    public UnityEvent<float> onPop;
    [Description("Is the mouse cursor hovering over the bubble")]
    public UnityEvent onHover;
    [Description("Is the mouse cursor stops hovering over the bubble")]
    public UnityEvent onHoverExit;

    // Start is called before the first frame update
    void Start()
    {
        m_camera = FindObjectOfType<Camera>();

        // Double the scale so it renders with correct radius
        transform.localScale = transform.localScale * radius * 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        var pos = transform.position;
        pos.z = 0.0f;
        var worldPos = m_camera.ScreenToWorldPoint(Input.mousePosition);
        worldPos.z = 0.0f;

        CheckForPop(pos, worldPos);
        DetectHover(pos, worldPos);

    }

    /// <summary>
    /// Check to see if the conditions to pop the bubble are met
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="worldPos"></param>
    private void CheckForPop(Vector3 pos, Vector3 worldPos)
    {
        if (Input.GetMouseButtonDown(0) && Vector3.Distance(worldPos, pos) <= radius)
        {
            onPopSimple.Invoke();
            onPop.Invoke(radius);
            this.gameObject.SetActive(false);

        }
    }

    /// <summary>
    /// Detects when the mouse cursor hovers over a bubble
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="worldPos"></param>
    private void DetectHover(Vector3 pos, Vector3 worldPos)
    {
        if (Vector3.Distance(worldPos, pos) <= radius)
        {
            onHover.Invoke();

        }
        else
        {
            onHoverExit.Invoke();
        }
    }
}
