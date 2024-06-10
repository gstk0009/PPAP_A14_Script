using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetRunForward(bool isRunning)
    {
        _animator.SetBool("RunForward", isRunning);
    }

    public void SetSprint(bool isSprinting)
    {
        _animator.SetBool("Sprint", isSprinting);
    }

    public void TriggerJump()
    {
        _animator.SetTrigger("Jump");
    }

    public void TriggerDie()
    {
        _animator.SetTrigger("Death");
    }

    public void SetRespawnIdle()
    {
        _animator.SetBool("Respawn", true);
    }
}
