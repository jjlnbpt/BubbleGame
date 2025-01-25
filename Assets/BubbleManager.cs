using UnityEngine;

/// <summary>
/// A centralized manager for tracking global information about bubbles.
/// </summary>
public class BubbleManager : MonoBehaviour
{
    private int m_popCount = 0;
    private float m_timeSinceLastPop = 0.0f;

    // Increments the pop counter by 1 for every bubble popped
    public void IncrementPopCount()
    {
        m_popCount += 1;
        m_timeSinceLastPop = 0.0f;
    }

    private void Update()
    {
        m_timeSinceLastPop += Time.deltaTime;
    }

    public int GetPopCount()
    {
        return m_popCount;
    }

    public float GetTimeSinceLastPop()
    {
        return m_timeSinceLastPop;
    }
}
