using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// A centralized manager for tracking global information about bubbles.
/// </summary>
public class BubbleManager : MonoBehaviour
{
    [Tooltip("The max allowed amount of time between consecutive pops")]
    [SerializeField]
    private float comboTime = 1.0f;

    private int m_popCount = 0;
    private float m_timeSinceLastPop = 0.0f;
    private int m_combo = 0;

    [Tooltip("Spawn event that provides no arguments")]
    public UnityEvent onSpawnSimple;
    [Tooltip("Bubble prefab")]
    [SerializeField] private GameObject bubblePrefab;

    private List<OnClick> bubbles;

    private void Start()
    {
        bubbles = new List<OnClick>();
    }

    public void RegisterBubble(OnClick bubble)
    {
        bubbles.Add(bubble);
    }

    // Increments the pop counter by 1 for every bubble popped
    public void IncrementPopCount(int amount = 1)
    {
        m_popCount += amount;
        m_timeSinceLastPop = 0.0f;
        m_combo += amount;
    }

    private void Update()
    {
        m_timeSinceLastPop += Time.deltaTime;
        if (m_timeSinceLastPop > comboTime)
        {
            m_combo = 0;
        }

        AudioManager.instance.background.setParameterByName("MusicIntensity", m_combo);

        CheckForSpawn();
    }

    /// <summary>
    /// Check to see if the conditions to spawn the bubble are met
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="worldPos"></param>
    private void CheckForSpawn()
    {
        if (Input.GetMouseButtonDown(1))
        {
            var worldPos = FindObjectOfType<Camera>().ScreenToWorldPoint(Input.mousePosition);
            worldPos.z = 0.0f;

            foreach (OnClick bubble in bubbles)
            {
                if (bubble.hover)
                {
                    return;
                }
            }

            Instantiate(bubblePrefab, worldPos, transform.rotation);
            AudioManager.instance.CreateSpawnInstance(m_combo);

            onSpawnSimple.Invoke();
        }
    }

    public int GetPopCount()
    {
        return m_popCount;
    }

    public float GetTimeSinceLastPop()
    {
        return m_timeSinceLastPop;
    }

    public int GetCurrentCombo()
    {
        return m_combo;
    }


}
