using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerStats : CharacterStats {

    Animator anim;
    PlayerMovement playerMov;
    PlayerController playerControl;
    CharacterCombat Enemy;

    protected override void Awake()
    {
        base.Awake();
        anim = GetComponent<Animator>();
        
        playerMov = GetComponent<PlayerMovement>();
        playerControl = GetComponent<PlayerController>();
        Enemy = GameObject.FindWithTag("Enemy").GetComponent<CharacterCombat>();
    }

    public override void Die()
    {
        base.Die();
        anim.SetTrigger("Die");
        playerMov.enabled = false;
        playerControl.enabled = false;
        Enemy.enabled = false;
    }
}
