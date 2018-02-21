using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerStats : CharacterStats
{

    Image healthBar;
    Image expBar;
    Text levelTxt;

    Animator anim;

    PlayerMovement playerMov;
    PlayerController playerControl;
    PlayerStats playerStats;
    PlayerCombat playerCombat;
    public GameObject HPRegenPrefab;
    public GameObject PlayerDamagePrefab;

    public int weaponMinDamage { get; set; }
    public int weaponMaxDamage { get; set; }
    public float weaponAnimationID { get; set; }
    public bool isUsingWeapon { get; set; }

    public int defaultArmor { get; set; }

    public bool isUsingArmor { get; set; }
    public int armorValue { get; set; }

    public bool isUsingGloves { get; set; }
    public int glovesArmorValue { get; set; }

    public bool isUsingHelmet { get; set; }
    public int helmetArmorValue { get; set; }

    public bool isUsingBoots { get; set; }
    public float bootsSpeed { get; set; }
    public int bootsArmor { get; set; }

    public GameObject deathMenu;

    public Vector3 RespawnPoint;

    bool isUsingPotion;
    private int level;
    private int nextLevelExperience;
    public int Experience { get; set; }
    public float combatCooldown = 0f;
    protected float timeToRegen = 1f;

    void Update()
    {
        if (Experience >= nextLevelExperience)
            LevelUp();

        expBar.fillAmount = (Experience / (float)nextLevelExperience);
        CheckCombat();
        timeToRegen -= Time.deltaTime;
    }

    protected override void Awake()
    {
        base.Awake();
        RespawnPoint = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        anim = GetComponent<Animator>();
        Experience = 0;
        level = 1;
        nextLevelExperience = 1;//  (int)((level / 2) + Mathf.Log10(2 * level + 1) * 1000 * level);

        CalculateMaxHelath();

        healthBar = GameObject.Find("FillHealth").GetComponent<Image>();
        expBar = GameObject.Find("FillXP").GetComponent<Image>();
        levelTxt = GameObject.Find("Level").GetComponent<Text>();
        levelTxt.text = level.ToString();

        playerMov = GetComponent<PlayerMovement>();
        playerControl = GetComponent<PlayerController>();
        playerStats = GetComponent<PlayerStats>();
        playerCombat = GetComponent<PlayerCombat>();
        currentHealth = maxHealth;
        RefreshHealthBar();
        weaponMinDamage = minDamage.GetValue();
        weaponMaxDamage = maxDamage.GetValue();
        defaultArmor = armor.GetValue();

        // deathMenu.SetActive(false);
    }

    public int GetLevel()
    {
        return this.level;
    }

    void LevelUp()
    {
        armor.SetValue(armor.GetValue() + 1);
        defaultArmor++;

        
        minDamage.SetValue(minDamage.GetValue() + 1);
        maxDamage.SetValue(maxDamage.GetValue() + ((int)(level * 0.2f)));

        level++;

        Experience -= nextLevelExperience;
        int previous = nextLevelExperience;
        nextLevelExperience = (int)((level / 2) + Mathf.Log10(2 * level + 1) * 1000 * level) - previous;

        levelTxt.text = level.ToString();

        CalculateMaxHelath();
        RefreshHealthBar();
    }

    public void SetWeaponProperties(int minDamage, int maxDamage, float animationID)
    {

        this.minDamage.SetValue(this.minDamage.GetValue() + minDamage);
        this.maxDamage.SetValue(this.maxDamage.GetValue() + maxDamage);

        anim.SetFloat("WeaponID", animationID);
    }

    public void SetBootProperties(int armor, float movementSpeed)
    {
        bootsArmor = armor;
        bootsSpeed = movementSpeed;
        playerMov.SetMovementSpeed(movementSpeed);
        float speedAnim = movementSpeed / 8;
        anim.SetFloat("WalkingSpeed", 1.1f + speedAnim);
        this.armor.SetValue(this.armor.GetValue() + armor);
    }

    private void CheckCombat()
    {
        if (combatCooldown > 0)
        {
            combatCooldown -= Time.deltaTime;
        }
        else
        {
            RegInTimeHP((level * 3 - 1), 3f);
        }
    }

    public bool UseHPPotion(int HPreg, float timeRegen)
    {
        if (currentHealth >= maxHealth || isUsingPotion)
        {
            return false;
        }
        else
        {
            StartCoroutine(RegHpByPotion(HPreg, timeRegen));
            isUsingPotion = true;
            return true;
        }
    }

    public IEnumerator RegHpByPotion(int HPreg, float timeRegen)
    {
        while (timeRegen >= 0)
        {
            if (maxHealth > currentHealth)
            {
                if (maxHealth < currentHealth + HPreg)
                {
                    HPreg = maxHealth - currentHealth;
                    currentHealth = maxHealth;
                }
                else
                {
                    currentHealth += HPreg;
                }
                InitCBT("+" + HPreg.ToString() + " HP", HPRegenPrefab);
                RefreshHealthBar();
            }
            else
            {
                isUsingPotion = false;
                break;
            }

            timeRegen -= 1;
            yield return new WaitForSeconds(1.0f);
        }
        isUsingPotion = false;
    }

    private void RegInTimeHP(int HPreg, float timeRegen)
    {

        if (maxHealth > currentHealth && timeToRegen < 0)
        {
            if (maxHealth < currentHealth + HPreg)
            {
                HPreg = maxHealth - currentHealth;
                currentHealth = maxHealth;
            }
            else
            {
                currentHealth += HPreg;
            }
            timeToRegen = timeRegen;
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

    public override void TakeDamage(int minDamage, int maxDamage)
    {
        if (currentHealth >= 0)
        {
            base.TakeDamage(minDamage, maxDamage);

            int damage = Random.Range(minDamage, maxDamage + 1);

            damage -= armor.GetValue();
            damage = Mathf.Clamp(damage, 0, int.MaxValue);
            RefreshHealthBar();
            InitCBT("-" + damage.ToString() + " HP", PlayerDamagePrefab);
            combatCooldown = 5f;
        }
    }

    public override void Die()
    {
        base.Die();
        NavMeshAgent nav = gameObject.GetComponent<NavMeshAgent>();
        nav.enabled = false;
        anim.SetTrigger("Die");
        playerMov.enabled = false;
        playerControl.enabled = false;
        playerStats.enabled = false;

        StartCoroutine(PauseFade());

        // Show menu (Respawn or Quit)
    }

    void ShowMenu()
    {
        deathMenu.SetActive(!deathMenu.activeSelf);
    }

    protected IEnumerator PauseFade()
    {
        while (Time.timeScale > 0)
        {
            if (Time.timeScale - 0.1f < 0)
                Time.timeScale = 0;
            else
                Time.timeScale -= 0.1f;

            yield return new WaitForSecondsRealtime(0.5f);
        }

        ShowMenu();

        yield return null;
    }

    protected IEnumerator ResumeFade()
    {
        while (Time.timeScale < 1)
        {
            if (Time.timeScale + 0.1f > 1)
                Time.timeScale = 1;
            else
                Time.timeScale += 0.1f;

            yield return new WaitForSecondsRealtime(0.1f);
        }

        yield return null;
    }

    public void Respawn()
    {
        playerMov.enabled = true;
        playerControl.enabled = true;
        playerStats.enabled = true;

        anim.SetTrigger("Respawn");

        currentHealth = maxHealth;
        RefreshHealthBar();

        ShowMenu();

        StartCoroutine(ResumeFade());

        // Take Player to respawn Point
        transform.localPosition = RespawnPoint;
        NavMeshAgent nav = gameObject.GetComponent<NavMeshAgent>();
        nav.enabled = true;

        playerMov.StopFollowingTarget();
        playerMov.MoveToPoint(RespawnPoint);
    }
}
