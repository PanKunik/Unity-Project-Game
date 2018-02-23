
using UnityEngine;

//[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public GameObject item;
    public Sprite icon = null;
    public bool isDefaultItem = false;
    public int itemID = 0;
    public int value = 100;


    public virtual void Use()
    {
        Debug.Log("Using " + this + "\n" + itemID);
    }

}
