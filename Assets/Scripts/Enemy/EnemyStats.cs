using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : CharacterStats {
    Slider HPSlider;

    Animator anim;

    public int multiplier = 1;
    public int level = 1;
    int experience;

    public GameObject ExpPrefab;
    public GameObject CBTprefabs;
    public GameObject HealthSlider;

    public SpawnEnemy SpawnPoint;
    public GameObject EnemyName;
    public GameObject EnemyMulti;

    PlayerStats playerStats;

    EnemyController EnemyController;
    CharacterCombat EnemyCombat;
    EnemyInteract EnemyInteract;

    CapsuleCollider Collider;



    protected override void Awake()
    {
        Collider = gameObject.GetComponent<CapsuleCollider>();
        HPSlider = transform.Find("EnemyCanvas").Find("HealthSlider").GetComponent<Slider>();

        experience = (int)(5 * Mathf.Log(level+1, 1.1F) * multiplier);
        currentHealth = maxHealth = (int)((100 + 10 * level * Mathf.Sqrt(level)) * multiplier);

        EnemyName.GetComponent<Text>().text = gameObject.name + " (" + level + ")";
        EnemyMulti.GetComponent<Text>().text = "x" + multiplier;

        damage.SetValue((int)((Mathf.Log(level+6,1.3F) * level / 2) *multiplier));


        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();

        // HealthSlider = transform.Find("HealthSlider").gameObject;

        anim = gameObject.GetComponent<Animator>();

        EnemyCombat = gameObject.GetComponent<CharacterCombat>();
        EnemyController = gameObject.GetComponent<EnemyController>();
        EnemyInteract = gameObject.GetComponent<EnemyInteract>();
    }

    public override void TakeDamage(int amount)
    {
        base.TakeDamage(amount);

        StopCoroutine("DeactivateForTime");
        EnemyController.isActive = true;

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

    }

    IEnumerator DeactivateEnemy(float time)
    {
        float timer = time;
        while( timer >= 0 )
        {
            timer -= Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }

        SpawnPoint.StartCoroutine("SpawnDelay", SpawnPoint.spawnTimer);
        gameObject.SetActive(false);
        GameObject.Find("Player").GetComponent<PlayerController>().RemoveFocus();

        transform.localPosition = new Vector3(0, 0);

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
        EnemyController.isActive = false;

        gameObject.SetActive(true);
        EnemyMulti.SetActive(true);
        EnemyName.SetActive(true);

        currentHealth = maxHealth = (int)((100 + 10 * level * Mathf.Sqrt(level)) * multiplier);
        float normalizedHealth = (currentHealth / (float)maxHealth);
        HPSlider.value = normalizedHealth;

        HealthSlider.SetActive(true);

        Collider.enabled = true;
        EnemyCombat.enabled = true;
        EnemyController.enabled = true;
        EnemyInteract.enabled = true;

        StartCoroutine("DeactivateForTime", 5f);
    }
}
