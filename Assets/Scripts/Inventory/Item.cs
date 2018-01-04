
using UnityEngine;

//[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public GameObject item;
    public Sprite icon = null;
    public bool isDefaultItem = false;
    public int itemID = 0;
    
    //public int weaponID = 0;
    //public int damage = 10;
    //public float speedAttack = 3;

    public virtual void Use()
    {
        Debug.Log("Using " + this + "\n" + itemID);
    }

}
