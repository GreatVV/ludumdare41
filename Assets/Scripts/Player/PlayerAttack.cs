using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public const string _ATTACK_TRIGGER_1 = "Attack_1";
    public const string _ATTACK_TRIGGER_2 = "Attack_2";
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _animator.SetTrigger(Random.value > 0.5 ? _ATTACK_TRIGGER_1 : _ATTACK_TRIGGER_2);
        }
    }
}
