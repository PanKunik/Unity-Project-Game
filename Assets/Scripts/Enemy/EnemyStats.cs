using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : CharacterStats {
    Slider img;

    public GameObject CBTprefabs;

    protected override void Awake()
    {
        base.Awake();
        img = transform.Find("EnemyCanvas").Find("HealthSlider").GetComponent<Slider>();
    }

    public override void TakeDamage(int amount)
    {
        base.TakeDamage(amount);
        InitCBT(amount.ToString());
        
        //FloatingTextController.CreateFloatingText(amount, transform);
        img.value = currentHealth;
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
