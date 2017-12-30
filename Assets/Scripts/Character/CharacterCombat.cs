using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour {

    public float attackSpeed = 0.7f;
    protected float attackCooldown = 0f;

    public float attackDelay = .6f; // .6f - delay time to damge for attack animation

    // public event System.Action OnAttack;

    protected CharacterStats myStats;

    protected virtual void Start()
    {
        myStats = GetComponent<CharacterStats>();
    }

    protected virtual void Update()
    {
        attackCooldown -= Time.deltaTime;
    }

	public virtual void Attack( CharacterStats targetStats )
    {
        if (attackCooldown <= 0)
        {
            StartCoroutine(DoDamage(targetStats, attackDelay)); // do damage when animation hits enemy

            if (gameObject.tag == "Enemy")
            {
                float atak = Random.Range(0, 2);
                Animator anim = gameObject.GetComponent<Animator>();
                anim.SetFloat("Blend", atak);
                anim.SetTrigger("Attack");
            }
            // targetStats.TakeDamage(myStats.damage.GetValue());

            attackCooldown = 1f / attackSpeed;
        }
    }

    protected IEnumerator DoDamage( CharacterStats stats, float delay )
    {
        yield return new WaitForSeconds(delay);

        stats.TakeDamage(myStats.damage.GetValue());
    }
}
