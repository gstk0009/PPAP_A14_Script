using UnityEngine;

public class AnimSpeedController : MonoBehaviour
{
    private Animator animator;
    public float animSpeed = 2f; // public으로 변경하여 Inspector에서 조정 가능

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        if (animator != null)
        {
            animator.speed = animSpeed;
        }
    }
}
