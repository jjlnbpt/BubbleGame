using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopCascade : MonoBehaviour
{
    [Tooltip("Radius to detect other bubbles, expressed as a multiplier of the radius")]
    public float detectionRadiusScale;


    public void Cascade(float radius)
    {
        float detectionRadius = radius * detectionRadiusScale;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Bubble"))
            {
                // if other bubble is smaller than this bubble and it's within vicinity
                if (collider.transform.localScale.magnitude < transform.localScale.magnitude)
                {
                    StartCoroutine(DelayUntilPop(collider));
                }
            }
        }
    }

    IEnumerator DelayUntilPop(Collider2D collider)
    {
        yield return new WaitForSeconds(0.1f);
        OnClick onClickComponent = collider.gameObject.GetComponent<OnClick>();
        onClickComponent.Pop(false);
    }
}
