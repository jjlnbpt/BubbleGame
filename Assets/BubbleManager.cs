using UnityEngine;

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

    // Increments the pop counter by 1 for every bubble popped
    public void IncrementPopCount()
    {
        m_popCount += 1;
        m_timeSinceLastPop = 0.0f;
        m_combo += 1;
    }

    private void Update()
    {
        m_timeSinceLastPop += Time.deltaTime;
        if (m_timeSinceLastPop > comboTime)
        {
            m_combo = 0;
        }

        AudioManager.instance.background.setParameterByName("MusicIntensity", m_combo);

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
