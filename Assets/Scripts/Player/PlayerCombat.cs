using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : CharacterCombat {

    Animator anim;
    public float defaultSpeedAttack { get; set; }
    public float attackSpeedAnimation = 1;
	// Use this for initialization
	protected override void Start () {
        base.Start();
        anim = GetComponent<Animator>();
        anim.SetFloat("attackSpeed", attackSpeedAnimation);
        defaultSpeedAttack = attackSpeed;
	}

    // Update is called once per frame
    protected override void Update () {
        base.Update();
	}

    public override void Attack(CharacterStats targetStats)
    {
        if (attackCooldown <= 0)
        {
            // targetStats.TakeDamage(myStats.damage.GetValue());
            anim.SetTrigger("Attack");
            anim.SetFloat("Attack Index", (int)Random.Range(0f, 2f));

            StartCoroutine(DoDamage(targetStats, attackDelay));  // do damage when animation hits enemy

            attackCooldown = 1f / attackSpeed;
        }
    }
}
