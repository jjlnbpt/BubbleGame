using UnityEngine;
using UnityEngine.Events;

public class OnClick : MonoBehaviour
{
    [Tooltip("Radius of bubble used for checking if the player clicks on a bubble")]
    public float radius = 1.0f;
    public float growthRate = 1.0f;

    [SerializeField] private float pop_radius = 4.0f;
    public float MaxSendOffForce;

    private Vector3 originalScale;

    private Camera m_camera;
    private BubbleManager m_bubbleManeger;

    public bool active = true;
    public bool hover = false;

    private bool register = false;

    [Header("Events")]
    [Space]
    [Tooltip("A simple pop event that provides no arguments")]
    public UnityEvent onPopSimple;
    [Tooltip("A simple pop event that provides the radius of the bubble popped")]
    public UnityEvent<float> onPop;
    [Tooltip("Is the mouse cursor hovering over the bubble")]
    public UnityEvent onHover;
    [Tooltip("Is the mouse cursor stops hovering over the bubble")]
    public UnityEvent onHoverExit;
    [Tooltip("Spawn event that provides no arguments")]
    public UnityEvent onSpawnSimple;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        m_camera = FindObjectOfType<Camera>();
        m_bubbleManeger = FindObjectOfType<BubbleManager>();

        originalScale = transform.localScale;

    }

    // Update is called once per frame
    void Update()
    {
        if (!register)
        {
            m_bubbleManeger.RegisterBubble(this);
            register = true;
        }

        // Double the scale so it renders with correct radius
        transform.localScale = originalScale * radius * 2.0f;

        if (active)
        {
            var pos = transform.position;
            pos.z = 0.0f;
            var worldPos = m_camera.ScreenToWorldPoint(Input.mousePosition);
            worldPos.z = 0.0f;

            CheckForPop(pos, worldPos);
            CheckForGrow(pos, worldPos);
            DetectHover(pos, worldPos);
        }
        else
        {
            hover = false;
        }

    }

    /// <summary>
    /// Check to see if the conditions to pop the bubble are met
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="worldPos"></param>
    private void CheckForPop(Vector3 pos, Vector3 worldPos)
    {
        bool sizeExceeded = radius > pop_radius;

        if ((Input.GetMouseButtonDown(0) && Vector3.Distance(worldPos, pos) <= radius) || sizeExceeded)
        {

            Pop(sizeExceeded);
        }
    }

    public void Pop(bool sizeExceeded)
    {
        AudioManager.instance.CreatePopInstance(m_bubbleManeger.GetCurrentCombo());

        onPopSimple.Invoke();
        onPop.Invoke(radius);

        if (sizeExceeded)
        {
            m_bubbleManeger.IncrementPopCount((int)pop_radius + 1);
        }
        else
        {
            m_bubbleManeger.IncrementPopCount((int)radius);
        }

        active = false;
    }

    /// <summary>
    /// Check to see if the conditions to pop the bubble are met
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="worldPos"></param>
    private void CheckForGrow(Vector3 pos, Vector3 worldPos)
    {
        if (Input.GetMouseButton(1) && Vector3.Distance(worldPos, pos) <= radius)
        {
            radius += growthRate * Time.deltaTime;
        } else if (Input.GetMouseButtonUp(1) && Vector3.Distance(worldPos, pos) <= radius)
        {
            Vector2 SendOffVector = new Vector2(Random.Range(-MaxSendOffForce, MaxSendOffForce), Random.Range(-MaxSendOffForce, MaxSendOffForce));
            rb.AddForce(SendOffVector, ForceMode2D.Impulse);
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
            hover = true;

        }
        else
        {
            onHoverExit.Invoke();
            hover = false;
        }
    }
}
