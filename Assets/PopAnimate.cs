using UnityEngine;

public class PopAnimate : MonoBehaviour
{
    public void PlayAnimation()
    {
        Animator animator = GetComponent<Animator>();

        animator.SetBool("Play", true);
    }
}
