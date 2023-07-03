using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private Animator _anim;

    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    public void Idle()
    {
        _anim.Play("Idle");
    }

    public void Move()
    {
        _anim.Play("Walking");
    }

    public void Attack()
    {
        _anim.Play("Slashing");
    }

    public void Death()
    {
        _anim.Play("Dying");
    }
}
