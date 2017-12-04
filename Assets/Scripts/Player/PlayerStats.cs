using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerStats : CharacterStats {

    Image healthBar;
    Text levelTxt;
    Image ExpBar;

    Animator anim;
    PlayerMovement playerMov;
    PlayerController playerControl;
    CharacterCombat Enemy;

    private int level;
    private int nextLevelExperience;
    public int Experience { get; set; }

    void Update()
    {
        if( Experience >= nextLevelExperience )
            LevelUp();

        ExpBar.fillAmount = (float)Experience / nextLevelExperience;
    }

    void LevelUp()
    {
        level++;
        nextLevelExperience = (int)((level / 2) + Mathf.Log10(2 * level + 1) * 1000 * level);
        levelTxt.text = level.ToString();
        Debug.Log("LEVEL UP! " + level);
    }

    protected override void Awake()
    {
        base.Awake();
        anim = GetComponent<Animator>();
        Experience = 414;
        Debug.Log(Experience + " exp");
        level = 1;
        nextLevelExperience = (int)((level/2) + Mathf.Log10(2*level+1) * 1000*level);
        healthBar = GameObject.Find("FillHealth").GetComponent<Image>();
        levelTxt = GameObject.Find("LevelTxt").GetComponent<Text>();
        levelTxt.text = level.ToString();
        ExpBar = GameObject.Find("FillExp").GetComponent<Image>();
        ExpBar.fillAmount = (float)Experience / nextLevelExperience;

        playerMov = GetComponent<PlayerMovement>();
        playerControl = GetComponent<PlayerController>();
        Enemy = GameObject.FindWithTag("Enemy").GetComponent<CharacterCombat>();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        healthBar.fillAmount = (currentHealth / (float)maxHealth);
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
