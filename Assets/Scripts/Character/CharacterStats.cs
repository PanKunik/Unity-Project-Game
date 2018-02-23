using UnityEngine;

public class CharacterStats : MonoBehaviour {

    public int maxHealth;
    public int currentHealth { get; protected set; }

    public Stat minDamage;
    public Stat maxDamage;
    public Stat armor;

    protected virtual void Awake()
    {
        // currentHealth = maxHealth;
    }

    void Update()
    {
        
    }

    /*protected void InitCBT(string text, GameObject Prefab)
    {
        GameObject temp = Instantiate(Prefab) as GameObject;
        RectTransform tempRect = temp.GetComponent<RectTransform>();
        temp.transform.SetParent(GameObject.Find("HUD").transform);
        tempRect.transform.localPosition = Prefab.transform.localPosition;
        tempRect.transform.localScale = Prefab.transform.localScale;
        tempRect.transform.localRotation = Prefab.transform.localRotation;

        temp.GetComponent<Text>().text = text;
    }*/

    public virtual void TakeDamage( int minDamage, int maxDamage )
    {
        int damage = Random.Range(minDamage, maxDamage+1);
        damage -= armor.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        currentHealth -= damage;
        // Debug.Log(transform.name + " takes " + damage + " damage.");

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        // Debug.Log(transform.name + " died.");
    }
}
