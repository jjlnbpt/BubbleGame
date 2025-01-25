using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

public class SetColor : MonoBehaviour
{
    private SpriteRenderer m_renderer;
    [SerializeReference] List<Color> colors;

    private void Start()
    {
        m_renderer = GetComponent<SpriteRenderer>();
    }

    public void UpdateColor(int index)
    {
        m_renderer.color = colors[index];
    }
}
