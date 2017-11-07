using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : CharacterStats {
    Slider img;

    protected override void Awake()
    {
        base.Awake();
        img = transform.Find("EnemyCanvas").Find("HealthSlider").GetComponent<Slider>();
    }

    public override void TakeDamage(int amount)
    {
        base.TakeDamage(amount);

        img.value = currentHealth;
    }

    public override void Die()
    {
        base.Die();

        Destroy(gameObject);
    }
}
