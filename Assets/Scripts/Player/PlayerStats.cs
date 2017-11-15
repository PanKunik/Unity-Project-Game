using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerStats : CharacterStats {

    Slider healthBar;

    Animator anim;
    PlayerMovement playerMov;
    PlayerController playerControl;
    CharacterCombat Enemy;

    protected override void Awake()
    {
        base.Awake();
        anim = GetComponent<Animator>();

        healthBar = GameObject.Find("HealthBar").GetComponent<Slider>();
        
        playerMov = GetComponent<PlayerMovement>();
        playerControl = GetComponent<PlayerController>();
        Enemy = GameObject.FindWithTag("Enemy").GetComponent<CharacterCombat>();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        healthBar.value = currentHealth;
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
