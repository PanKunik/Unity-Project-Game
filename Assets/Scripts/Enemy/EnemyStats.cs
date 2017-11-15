using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : CharacterStats {
    Slider img;

    public int multiplier = 1;
    public int level = 1;
    int experience;

    public GameObject CBTprefabs;

    protected override void Awake()
    {
        img = transform.Find("EnemyCanvas").Find("HealthSlider").GetComponent<Slider>();
        experience = (int)(5 * Mathf.Log(level+1, 1.1F) * multiplier);
        currentHealth = maxHealth = (int)((100 + 10 * level * Mathf.Sqrt(level)) * multiplier);
    }

    public override void TakeDamage(int amount)
    {
        base.TakeDamage(amount);
        InitCBT(amount.ToString());
        float normalizedHealth = (currentHealth / (float)maxHealth);
        //FloatingTextController.CreateFloatingText(amount, transform);
        img.value = normalizedHealth;
    }

    void InitCBT(string text)
    {
        GameObject temp = Instantiate(CBTprefabs) as GameObject;
        RectTransform tempRect = temp.GetComponent<RectTransform>();
        temp.transform.SetParent(transform.Find("EnemyCanvas"));
        tempRect.transform.localPosition = CBTprefabs.transform.localPosition;
        tempRect.transform.localScale = CBTprefabs.transform.localScale;
        tempRect.transform.localRotation = CBTprefabs.transform.localRotation;

        temp.GetComponent<Text>().text = text;
    }

    public override void Die()
    {
        base.Die();

        Destroy(gameObject);
    }
}
