using UnityEngine;

public class PopAnimate : MonoBehaviour
{
    public void PlayAnimation()
    {
        var animator = GetComponent<Animator>();

        animator.SetBool("Play", true);
    }
}
