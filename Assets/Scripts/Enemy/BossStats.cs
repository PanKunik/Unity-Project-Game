using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BossStats : CharacterStats
{
    Slider HPSlider;
    
    Animator anim;

    public int multiplier = 1;
    public int level = 1;
    int experience;

    public GameObject ExpPrefab;
    public GameObject CBTprefabs;
    public GameObject HealthSlider;

    public GameObject TutorialArea;

    public SpawnEnemy SpawnPoint;
    public GameObject EnemyName;
    public GameObject EnemyMulti;

    public LootSpawnItem[] spawnItemClass;
    public LootSpawnItem spawnRipple;

    PlayerStats playerStats;

    EnemyController EnemyController;
    CharacterCombat EnemyCombat;
    EnemyInteract EnemyInteract;

    CapsuleCollider Collider;

    // public GameObject NameCount;



    protected override void Awake()
    {

        Collider = gameObject.GetComponent<CapsuleCollider>();
        HPSlider = transform.Find("EnemyCanvas").Find("HealthSlider").GetComponent<Slider>();

        experience = (int)(5 * Mathf.Log(level + 1, 1.1F) * multiplier);
        currentHealth = maxHealth = (int)((100 + 10 * level * Mathf.Sqrt(level)) * multiplier);

        EnemyName.GetComponent<Text>().text = gameObject.name + " (" + level + ")";
        EnemyMulti.GetComponent<Text>().text = "Boss";

        minDamage.SetValue((int)((Mathf.Log(level + 6, 1.3F) * level / 2)));
        maxDamage.SetValue((int)((Mathf.Log(level + 6, 1.2F) * level / 2)));
        armor.SetValue((int)(minDamage.GetValue() + level * multiplier) / 2);


        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();

        // HealthSlider = transform.Find("HealthSlider").gameObject;

        anim = gameObject.GetComponent<Animator>();

        EnemyCombat = gameObject.GetComponent<CharacterCombat>();
        EnemyController = gameObject.GetComponent<EnemyController>();
        EnemyInteract = gameObject.GetComponent<EnemyInteract>();
    }

    public override void TakeDamage(int minDamage, int maxDamage)
    {
        base.TakeDamage(minDamage, maxDamage);

        StopCoroutine("DeactivateForTime");
        EnemyController.isActive = true;

        int amount = Random.Range(minDamage, maxDamage + 1);

        amount -= armor.GetValue();
        amount = Mathf.Clamp(amount, 0, int.MaxValue);

        InitCBT(amount.ToString(), CBTprefabs);
        float normalizedHealth = (currentHealth / (float)maxHealth);
        HPSlider.value = normalizedHealth;

        playerStats.combatCooldown = 5f;
        FindObjectOfType<AudioManager>().Play("SwordHit");
    }

    void InitCBT(string text, GameObject Prefab)
    {
        GameObject temp = Instantiate(Prefab) as GameObject;
        RectTransform tempRect = temp.GetComponent<RectTransform>();
        temp.transform.SetParent(transform.Find("EnemyCanvas"));
        tempRect.transform.localPosition = Prefab.transform.localPosition;
        tempRect.transform.localScale = Prefab.transform.localScale;
        tempRect.transform.localRotation = Prefab.transform.localRotation;

        temp.GetComponent<Text>().text = text;
    }

    public override void Die()
    {
        base.Die();

        GameObject.Find("Player").GetComponent<PlayerController>().RemoveFocusOnDeath();
        SpawnLoot();

        Collider.enabled = false;

        anim.SetTrigger("Die");

        SpawnPoint.dead = true;

        HealthSlider.SetActive(false);
        EnemyMulti.SetActive(false);
        EnemyName.SetActive(false);

        StartCoroutine("DeactivateEnemy", 3f);

        EnemyCombat.enabled = false;
        EnemyController.enabled = false;
        EnemyInteract.enabled = false;

        playerStats.Experience += experience;
        InitCBT("+" + experience.ToString() + " XP", ExpPrefab);


        DefeatBoss defeatedBoss = gameObject.GetComponent<DefeatBoss>();
        if (defeatedBoss != null)
            defeatedBoss.Defeat();
    }

    IEnumerator DeactivateEnemy(float time)
    {
        float timer = time;
        while (timer >= 0)
        {
            timer -= Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }

        // SpawnPoint.StartCoroutine("SpawnDelay", SpawnPoint.spawnTimer);
        // gameObject.SetActive(false);

        // transform.localPosition = new Vector3(0, 0);

        yield return null;
    }

    IEnumerator DeactivateForTime(float time)
    {
        float timer = time;
        while (timer >= 0)
        {
            timer -= Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        EnemyController.isActive = true;

        yield return null;
    }

    public void Spawn()
    {
        HealthSlider.SetActive(true);

        Collider.enabled = true;
        EnemyCombat.enabled = true;
        EnemyController.enabled = true;
        EnemyInteract.enabled = true;

        gameObject.SetActive(true);
        EnemyMulti.SetActive(true);
        EnemyName.SetActive(true);

        currentHealth = maxHealth = (int)((100 + 10 * level * Mathf.Sqrt(level)) * multiplier);
        float normalizedHealth = (currentHealth / (float)maxHealth);
        HPSlider.value = normalizedHealth;
        EnemyController.isActive = false;

        StartCoroutine("DeactivateForTime", 5f);
    }


    void SpawnLoot()
    {
        // Debug.Log("Spawning: " + itemToSpawn);

        int count;
        foreach (LootSpawnItem item in spawnItemClass)
        {
            float dropChance = Random.Range(0f, 1f);
            if (item.chance >= dropChance)
            {
                count = Random.Range(item.minCount, (item.maxCount + 1));
                for (int i = 0; i < count; i++)
                {

                    Vector3 spawnPosition = new Vector3(this.transform.position.x, this.transform.position.y + 2f, this.transform.position.z);
                    GameObject drop = Instantiate(item.spawnItem, spawnPosition, Quaternion.identity);
                    Rigidbody rb = drop.GetComponent<Rigidbody>();
                    // ItemPickup itemPick = drop.GetComponent<ItemPickup>();
                    // itemPick.nameItem = NameCount;

                    rb.AddForce(transform.forward * Random.Range(100, 200));
                }
            }
        }

        float rippleChance = Random.Range(0f, 1f);

        if (spawnRipple.chance >= rippleChance)
        {
            count = Random.Range(spawnRipple.minCount, (spawnRipple.maxCount + 1));
            ItemPickup amountOfRippleToDrop = spawnRipple.spawnItem.GetComponent<ItemPickup>();
            print(amountOfRippleToDrop);
            amountOfRippleToDrop.amountItemPickup = count;
            // Vector3 spawnRipplePos = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
            /*GameObject dropRipples = */Instantiate(spawnRipple.spawnItem, this.transform.position, Quaternion.identity);
        }


        /*for (int i = 0; i < Random.Range(4, 10); i++)
        {
            item = Random.Range(0, itemToSpawn.Length);
            Vector3 spawnPosition = new Vector3(this.transform.position.x, this.transform.position.y + 2f, this.transform.position.z);
            GameObject drop = Instantiate(itemToSpawn[item], spawnPosition, Quaternion.identity);
            Rigidbody rb = drop.GetComponent<Rigidbody>();

            rb.AddForce(transform.forward * Random.Range(100, 500));
        }*/


    }
}
