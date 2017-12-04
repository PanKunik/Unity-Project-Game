using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerStats : CharacterStats {

    Image healthBar;
    Image expBar;
    Text levelTxt;

    Animator anim;

    PlayerMovement playerMov;
    PlayerController playerControl;
    PlayerStats playerStats;

    public GameObject HPRegenPrefab;
    public GameObject PlayerDamagePrefab;

    private int level;
    private int nextLevelExperience;
    public int Experience { get; set; }

    public float combatCooldown = 0f;
    protected float timeToRegen = 1f;

    void Update()
    {
        if( Experience >= nextLevelExperience )
            LevelUp();

        expBar.fillAmount = (Experience / (float)nextLevelExperience);

        CheckCombat();
        timeToRegen -= Time.deltaTime;
    }

    protected override void Awake()
    {
        base.Awake();
        anim = GetComponent<Animator>();
        Experience = 0;
        level = 1;
        nextLevelExperience = (int)((level / 2) + Mathf.Log10(2 * level + 1) * 1000 * level);

        CalculateMaxHelath();

        healthBar = GameObject.Find("FillHealth").GetComponent<Image>();
        expBar = GameObject.Find("FillXP").GetComponent<Image>();
        levelTxt = GameObject.Find("Level").GetComponent<Text>();
        levelTxt.text = level.ToString();

        playerMov = GetComponent<PlayerMovement>();
        playerControl = GetComponent<PlayerController>();
        playerStats = GetComponent<PlayerStats>();
    }

    void LevelUp()
    {
        level++;
        nextLevelExperience = (int)((level / 2) + Mathf.Log10(2 * level + 1) * 1000 * level);
        levelTxt.text = level.ToString();

        CalculateMaxHelath();
        RefreshHealthBar();
    }

    private void CheckCombat()
    {
        if (combatCooldown > 0)
        {
            combatCooldown -= Time.deltaTime;
        }
        else
        {
            RegInTimeHP();
        }
    }

    private void RegInTimeHP()
    {
        int HPreg = (level * 3 - 1);
        if (maxHealth > currentHealth && timeToRegen < 0)
        {
            if( maxHealth < currentHealth + HPreg )
            {
                HPreg = maxHealth - currentHealth;
                currentHealth = maxHealth;
            }
            else
            {
                currentHealth += HPreg;
            }
            timeToRegen = 3f;
            InitCBT("+" + HPreg.ToString() + " HP", HPRegenPrefab);
            RefreshHealthBar();
        }
    }

    void InitCBT(string text, GameObject Prefab)
    {
        GameObject temp = Instantiate(Prefab) as GameObject;
        RectTransform tempRect = temp.GetComponent<RectTransform>();
        temp.transform.SetParent(GameObject.Find("HUD").transform);
        tempRect.transform.localPosition = Prefab.transform.localPosition;
        tempRect.transform.localScale = Prefab.transform.localScale;
        tempRect.transform.localRotation = Prefab.transform.localRotation;

        temp.GetComponent<Text>().text = text;
    }

    public void CalculateMaxHelath()
    {
        currentHealth = maxHealth = (int)((150 + 20 * level * Mathf.Sqrt(level)));
    }

    public void RefreshHealthBar()
    {
         healthBar.fillAmount = (currentHealth / (float)maxHealth);
    }

    public override void TakeDamage(int damage)
    {
		if (currentHealth >= 0) {
			base.TakeDamage (damage);
			RefreshHealthBar ();
			InitCBT ("-" + damage.ToString () + " HP", PlayerDamagePrefab);
			combatCooldown = 5f;
		}
    }

    public override void Die()
    {
        base.Die();
        anim.SetTrigger("Die");
        playerMov.enabled = false;
        playerControl.enabled = false;
        playerStats.enabled = false;
    }
}
