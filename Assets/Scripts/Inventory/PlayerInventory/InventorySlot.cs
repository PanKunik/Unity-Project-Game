using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class InventorySlot : MonoBehaviour
{
    Item item { get; set; }
    public Weapon weapon { get; set; }

    public Armor armor { get; set; }
    public Armor gloves { get; set; }
    public Armor helmet { get; set; }
    public Boots boots { get; set; }
    Text text;
    public InventorySlot weaponSlot { get; set; }
    public InventorySlot armorSlot { get; set; }
    public InventorySlot helmetSlot { get; set; }
    public InventorySlot glovesSlot { get; set; }
    public InventorySlot bootsSlot { get; set; }

    public InventoryUI inventoryUI { get; set; }
    WeaponSwitch weaponSwitch;
    PlayerStats playerStats;
    PlayerCombat playerCombat;


    public int slotTypeID;
    public Image icon;
    public bool isSelected = false;
    public Image selected;

    public Item GetItem()
    {

        return item;
    }

    private void Awake()
    {
        inventoryUI = GameObject.Find("Inventory").GetComponent<InventoryUI>();
        weaponSwitch = GameObject.FindGameObjectWithTag("WeaponHolder").GetComponent<WeaponSwitch>();
        text = gameObject.GetComponentInChildren<Text>();
        if (!item)
            if (text)
                text.enabled = false;

        weaponSlot = GameObject.Find("WeaponSlot").GetComponent<InventorySlot>();
        weaponSlot.icon.enabled = false;
        armorSlot = GameObject.Find("ArmorSlot").GetComponent<InventorySlot>();
        armorSlot.icon.enabled = false;
        helmetSlot = GameObject.Find("HelmetSlot").GetComponent<InventorySlot>();
        helmetSlot.icon.enabled = false;
        glovesSlot = GameObject.Find("GlovesSlot").GetComponent<InventorySlot>();
        glovesSlot.icon.enabled = false;
        bootsSlot = GameObject.Find("BootsSlot").GetComponent<InventorySlot>();
        bootsSlot.icon.enabled = false;


        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        playerCombat = GameObject.Find("Player").GetComponent<PlayerCombat>();
        this.icon.enabled = false;

    }

    public void AddItem(Item newItem)
    {
        if (newItem != null)
        {
            item = newItem;

            icon.sprite = item.icon;
            icon.enabled = true;
        }
        else
        {
            item = null;
            icon.enabled = false;
        }
    }

    private void Update()
    {
        selected.enabled = isSelected;
        ShowAmount();
        EquipWeaponAnimation();
        SetWeaponProperties();
        SetArmorProperties();
        SetBootsProperties();
    }

    void SetBootsProperties()
    {
        boots = (Boots)bootsSlot.item;
        if (boots && !playerStats.isUsingBoots)
        {
            playerStats.isUsingBoots = true;
            playerStats.SetBootProperties(boots.armor, boots.movementSpeed);
        }
        else if (!boots && playerStats.isUsingBoots)
        {
            playerStats.isUsingBoots = false;
            playerStats.SetBootProperties(-playerStats.bootsArmor, -playerStats.bootsSpeed);
        }
        else if (boots && playerStats.isUsingBoots)
        {
            playerStats.isUsingBoots = false;
            playerStats.SetBootProperties(-playerStats.bootsArmor, -playerStats.bootsSpeed);
            playerStats.isUsingBoots = true;
            playerStats.SetBootProperties(boots.armor, boots.movementSpeed);
        }
    }

    void SetArmorProperties()
    {
        ChangePropertiesForArmor();

        ChangePropertiesForGloves();

        ChangePropertiesForHelmet();


    }

    private void ChangePropertiesForHelmet()
    {
        helmet = (Armor)helmetSlot.item;
        if (helmet && !playerStats.isUsingHelmet)
        {
            playerStats.isUsingHelmet = true;
            ArmorChangePlayerStats(helmet.armor);
            playerStats.helmetArmorValue = helmet.armor;
        }//add helmet armor
        else if (!helmet && playerStats.isUsingHelmet)
        {
            playerStats.isUsingHelmet = false;
            SetDefaultArmorStats(playerStats.helmetArmorValue);
        }// delete helmet armor
        else if (helmet && playerStats.isUsingHelmet)
        {
            playerStats.isUsingHelmet = false;
            SetDefaultArmorStats(playerStats.helmetArmorValue);
            playerStats.isUsingHelmet = true;
            ArmorChangePlayerStats(helmet.armor);
            playerStats.helmetArmorValue = helmet.armor;
        }//add helmet armor if there is helmet in slot
    }

    private void ChangePropertiesForGloves()
    {
        gloves = (Armor)glovesSlot.item;
        if (gloves && !playerStats.isUsingGloves)
        {
            ArmorChangePlayerStats(gloves.armor);
            playerStats.isUsingGloves = true;
            playerStats.glovesArmorValue = gloves.armor;
        }//add gloves armor
        else if (!gloves && playerStats.isUsingGloves)
        {
            playerStats.isUsingGloves = false;
            SetDefaultArmorStats(playerStats.glovesArmorValue);
        }//delete gloves armor
        else if (gloves && playerStats.isUsingGloves)
        {
            playerStats.isUsingGloves = false;
            SetDefaultArmorStats(playerStats.glovesArmorValue);
            ArmorChangePlayerStats(gloves.armor);
            playerStats.isUsingGloves = true;
            playerStats.glovesArmorValue = gloves.armor;
        }//add gloves armor if there are gloves in slot
    }


    private void ChangePropertiesForArmor()
    {

        armor = (Armor)armorSlot.item;
        if (armor && !playerStats.isUsingArmor)
        {
            playerStats.isUsingArmor = true;
            ArmorChangePlayerStats(armor.armor);
            playerStats.armorValue = armor.armor;
        }// add armor
        else if (!armor && playerStats.isUsingArmor)
        {
            playerStats.isUsingArmor = false;
            SetDefaultArmorStats(playerStats.armorValue);
        }//delete armor
        else if (armor && playerStats.isUsingArmor)
        {
            playerStats.isUsingArmor = false;
            SetDefaultArmorStats(playerStats.armorValue);
            playerStats.isUsingArmor = true;
            ArmorChangePlayerStats(armor.armor);
            playerStats.armorValue = armor.armor;

        }//add armor if there is armor in armor slot
    }

    void SetWeaponProperties()
    {
        weapon = (Weapon)weaponSlot.item;
        if (weapon && !playerStats.isUsingWeapon)
        {
            WeaponChangePlayerStats(weapon.minDamage, weapon.maxDamage, weapon.animationID);
            playerStats.weaponMinDamage = weapon.minDamage;
            playerStats.weaponMaxDamage = weapon.maxDamage;
            playerStats.isUsingWeapon = true;
        }
        else if (!weapon && playerStats.isUsingWeapon)
        {

            playerStats.isUsingWeapon = false;
            SetDefaultWeaponStats(playerStats.weaponMinDamage, playerStats.weaponMaxDamage);
        }
        else if (weapon && playerStats.isUsingWeapon)
        {

            playerStats.isUsingWeapon = false;
            SetDefaultWeaponStats(playerStats.weaponMinDamage, playerStats.weaponMaxDamage);
            playerStats.isUsingWeapon = true;
            WeaponChangePlayerStats(weapon.minDamage, weapon.maxDamage, weapon.animationID);
            playerStats.weaponMinDamage = weapon.minDamage;
            playerStats.weaponMaxDamage = weapon.maxDamage;

        }
    }

    void SetDefaultWeaponStats(int minDamage, int maxDamage)
    {
        playerStats.minDamage.SetValue(playerStats.minDamage.GetValue() - minDamage);
        playerStats.maxDamage.SetValue(playerStats.maxDamage.GetValue() - maxDamage);
        playerCombat.attackSpeed = playerCombat.defaultSpeedAttack;
    }

    void SetDefaultArmorStats(int armor)
    {
        playerStats.armor.SetValue(playerStats.armor.GetValue() - armor);
    }



    void SetDefaultPlayerWeaponStats()
    {
        playerStats.minDamage.SetValue(playerStats.weaponMinDamage);
        playerStats.maxDamage.SetValue(playerStats.weaponMaxDamage);
        playerCombat.attackSpeed = playerCombat.defaultSpeedAttack;

        weaponSwitch.isArmed = false;
    }

    public void EquipWeaponAnimation()
    {

        if (weaponSlot.item)
        {
            weapon = (Weapon)weaponSlot.item;
            weaponSwitch.isArmed = true;
            weaponSwitch.SelectWeapon(weapon.weaponID);
        }
        else
        {
            weaponSwitch.isArmed = false;
            weaponSwitch.DiselectWeapons();
        }
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
    }

    public void OnClicked()
    {
        isSelected = !isSelected;
        for (int i = 0; i < inventoryUI.slots.Length; i++)
        {
            if (bootsSlot.isSelected)                       //changing armor item in inventory, check if selected armorslot
            {
                if (this.item != null)
                {
                    if (this.item.itemID == 4)
                    {
                        Item tempItem = bootsSlot.item;
                        bootsSlot.AddItem(this.item);
                        this.AddItem(tempItem);
                        bootsSlot.isSelected = false;
                        this.isSelected = false;
                        inventoryUI.ChangeInventoryList();
                        inventoryUI.AddBoots(bootsSlot.item);
                        //ChangePlayerProperties();
                    }
                    else
                    {
                        this.isSelected = false;
                        bootsSlot.isSelected = false;
                    }

                }
                else
                {
                    Item temp = bootsSlot.item;
                    bootsSlot.AddItem(this.item);
                    this.AddItem(temp);
                    bootsSlot.isSelected = false;
                    this.isSelected = false;
                    inventoryUI.ChangeInventoryList();
                    inventoryUI.AddBoots(bootsSlot.item);
                    //ChangePlayerProperties();
                }
            }//changing glovesSlot with slot


            if (glovesSlot.isSelected)
            {
                if (this.item != null)
                {
                    if (this.item.itemID == 3)
                    {
                        Item tempItem = glovesSlot.item;
                        glovesSlot.AddItem(this.item);
                        this.AddItem(tempItem);
                        glovesSlot.isSelected = false;
                        this.isSelected = false;
                        inventoryUI.ChangeInventoryList();
                        inventoryUI.AddGloves(glovesSlot.item);
                        //ChangePlayerProperties();
                    }
                    else
                    {
                        this.isSelected = false;
                        glovesSlot.isSelected = false;
                    }

                }
                else
                {
                    Item temp = glovesSlot.item;
                    glovesSlot.AddItem(this.item);
                    this.AddItem(temp);
                    glovesSlot.isSelected = false;
                    this.isSelected = false;
                    inventoryUI.ChangeInventoryList();
                    inventoryUI.AddGloves(glovesSlot.item);
                    //ChangePlayerProperties();
                }
            }//changing glovesSlot with slot


            if (helmetSlot.isSelected)                       //changing armor item in inventory, check if selected armorslot
            {
                if (this.item != null)
                {
                    if (this.item.itemID == 2)
                    {
                        Item tempItem = helmetSlot.item;
                        helmetSlot.AddItem(this.item);
                        this.AddItem(tempItem);
                        helmetSlot.isSelected = false;
                        this.isSelected = false;
                        inventoryUI.ChangeInventoryList();
                        inventoryUI.AddHelmet(helmetSlot.item);
                        //ChangePlayerProperties();
                    }
                    else
                    {
                        this.isSelected = false;
                        helmetSlot.isSelected = false;
                    }

                }
                else
                {
                    Item temp = helmetSlot.item;
                    helmetSlot.AddItem(this.item);
                    this.AddItem(temp);
                    helmetSlot.isSelected = false;
                    this.isSelected = false;
                    inventoryUI.ChangeInventoryList();
                    inventoryUI.AddHelmet(helmetSlot.item);
                    //ChangePlayerProperties();
                }
            }//changing helmetSlot with slot


            if (armorSlot.isSelected)                       //changing armor item in inventory, check if selected armorslot
            {
                if (this.item != null)
                {
                    if (this.item.itemID == 1)
                    {
                        Item tempItem = armorSlot.item;
                        armorSlot.AddItem(this.item);
                        this.AddItem(tempItem);
                        armorSlot.isSelected = false;
                        this.isSelected = false;
                        inventoryUI.ChangeInventoryList();
                        inventoryUI.AddArmor(armorSlot.item);
                        //ChangePlayerProperties();
                    }
                    else
                    {
                        this.isSelected = false;
                        armorSlot.isSelected = false;
                    }

                }
                else
                {
                    Item temp = armorSlot.item;
                    armorSlot.AddItem(this.item);
                    this.AddItem(temp);
                    armorSlot.isSelected = false;
                    this.isSelected = false;
                    inventoryUI.ChangeInventoryList();
                    inventoryUI.AddArmor(armorSlot.item);
                    //ChangePlayerProperties();
                }
                if (helmetSlot.isSelected)                       //changing armor item in inventory, check if selected armorslot
                {
                    if (this.item != null)
                    {
                        if (this.item.itemID == 2)
                        {
                            Item tempItem = helmetSlot.item;
                            helmetSlot.AddItem(this.item);
                            this.AddItem(tempItem);
                            helmetSlot.isSelected = false;
                            this.isSelected = false;
                            inventoryUI.ChangeInventoryList();
                            inventoryUI.AddHelmet(helmetSlot.item);
                            //ChangePlayerProperties();
                        }
                        else
                        {
                            this.isSelected = false;
                            helmetSlot.isSelected = false;
                        }

                    }
                    else
                    {
                        Item temp = helmetSlot.item;
                        helmetSlot.AddItem(this.item);
                        this.AddItem(temp);
                        helmetSlot.isSelected = false;
                        this.isSelected = false;
                        inventoryUI.ChangeInventoryList();
                        inventoryUI.AddHelmet(helmetSlot.item);
                        //ChangePlayerProperties();
                    }
                }//changing helmetSlot with slot
                if (helmetSlot.isSelected)                       //changing armor item in inventory, check if selected armorslot
                {
                    if (this.item != null)
                    {
                        if (this.item.itemID == 2)
                        {
                            Item tempItem = helmetSlot.item;
                            helmetSlot.AddItem(this.item);
                            this.AddItem(tempItem);
                            helmetSlot.isSelected = false;
                            this.isSelected = false;
                            inventoryUI.ChangeInventoryList();
                            inventoryUI.AddHelmet(helmetSlot.item);
                            //ChangePlayerProperties();
                        }
                        else
                        {
                            this.isSelected = false;
                            helmetSlot.isSelected = false;
                        }

                    }
                    else
                    {
                        Item temp = helmetSlot.item;
                        helmetSlot.AddItem(this.item);
                        this.AddItem(temp);
                        helmetSlot.isSelected = false;
                        this.isSelected = false;
                        inventoryUI.ChangeInventoryList();
                        inventoryUI.AddHelmet(helmetSlot.item);
                        //ChangePlayerProperties();
                    }
                }//changing helmetSlot with slot
            }//changing armorSlot with slot


            if (weaponSlot.isSelected)
            {

                if (this.item != null)
                {
                    if (this.item.itemID == 6)
                    {

                        Item temp = weaponSlot.item;
                        weaponSlot.AddItem(this.item);
                        this.AddItem(temp);
                        weaponSlot.isSelected = false;
                        this.isSelected = false;
                        inventoryUI.ChangeInventoryList();
                        inventoryUI.AddWeapon(weaponSlot.item);
                        //ChangePlayerProperties();

                    }
                    else
                    {
                        weaponSlot.isSelected = false;
                        this.isSelected = false;

                    }
                }
                else
                {
                    Item temp = weaponSlot.item;
                    weaponSlot.AddItem(this.item);
                    this.AddItem(temp);
                    weaponSlot.isSelected = false;
                    this.isSelected = false;
                    inventoryUI.ChangeInventoryList();
                    inventoryUI.AddWeapon(weaponSlot.item);
                    //ChangePlayerProperties();
                }
                Debug.Log("1");
            }//changing weaponSlot with slot
            else if (inventoryUI.slots[i].isSelected && inventoryUI.slots[i] != this)
            {
                Item temp = this.item;
                this.AddItem(inventoryUI.slots[i].item);
                inventoryUI.slots[i].AddItem(temp);
                inventoryUI.slots[i].isSelected = false;
                isSelected = false;
                inventoryUI.ChangeInventoryList();
                inventoryUI.AddWeapon(weaponSlot.item);
                //ChangePlayerProperties();
                Debug.Log("2");
            }

        }

    }

    public void OnWeaponSlotClicked()
    {

        isSelected = !isSelected;
        for (int i = 0; i < inventoryUI.slots.Length; i++)
        {
            if (inventoryUI.slots[i].isSelected && inventoryUI.slots[i] != this)
            {
                if (inventoryUI.slots[i].item == null)
                {
                    Item temp = inventoryUI.slots[i].item;
                    inventoryUI.slots[i].AddItem(this.item);
                    this.AddItem(temp);
                    inventoryUI.slots[i].isSelected = false;
                    isSelected = false;
                    inventoryUI.ChangeInventoryList();
                    inventoryUI.AddWeapon(weaponSlot.item);
                    Debug.Log("3");
                }//if there is no item in slot u selected before
                else if (this.item == null)
                {
                    if (inventoryUI.slots[i].item.itemID == 6)
                    {
                        Item temp = this.item;
                        this.AddItem(inventoryUI.slots[i].item);
                        inventoryUI.slots[i].AddItem(temp);
                        inventoryUI.slots[i].isSelected = false;
                        isSelected = false;
                        inventoryUI.ChangeInventoryList();
                        inventoryUI.AddWeapon(weaponSlot.item);



                    } // if selected items can be fitted
                    else
                    {
                        inventoryUI.slots[i].isSelected = false;
                        isSelected = false;

                    } //if selected items in slots cannot be fitted
                    Debug.Log("4");
                }//if there is no item in slot u have just selected
                else
                {
                    if (inventoryUI.slots[i].item.itemID == 6)
                    {
                        Item temp = this.item;
                        this.AddItem(inventoryUI.slots[i].item);
                        inventoryUI.slots[i].AddItem(temp);
                        inventoryUI.slots[i].isSelected = false;
                        isSelected = false;
                        inventoryUI.ChangeInventoryList();
                        inventoryUI.AddWeapon(weaponSlot.item);
                    }
                    else
                    {
                        inventoryUI.slots[i].isSelected = false;
                        isSelected = false;
                    }
                    Debug.Log("5");
                }

            }

        }

    }//all conditions to change weapon items with weapon slot

    public void OnArmorSlotClicked()
    {
        isSelected = !isSelected;
        for (int i = 0; i < inventoryUI.slots.Length; i++)
        {
            if (inventoryUI.slots[i].isSelected && inventoryUI.slots[i] != this)
            {
                if (inventoryUI.slots[i].item == null)
                {
                    Item temp = inventoryUI.slots[i].item;
                    inventoryUI.slots[i].AddItem(this.item);
                    this.AddItem(temp);
                    inventoryUI.slots[i].isSelected = false;
                    isSelected = false;
                    inventoryUI.ChangeInventoryList();
                    inventoryUI.AddArmor(armorSlot.item);
                    Debug.Log("6");
                }//if there is no item u selected before 
                else if (this.item == null)
                {
                    if (inventoryUI.slots[i].item.itemID == 1)
                    {
                        Item temp = this.item;
                        this.AddItem(inventoryUI.slots[i].item);
                        inventoryUI.slots[i].AddItem(temp);
                        inventoryUI.slots[i].isSelected = false;
                        inventoryUI.ChangeInventoryList();
                        inventoryUI.AddArmor(armorSlot.item);
                        isSelected = false;

                    } // if item can be fitted to the slot
                    else
                    {
                        inventoryUI.slots[i].isSelected = false;
                        isSelected = false;

                    } // if item is not fitted that slot
                    Debug.Log("7");
                }//if there is no item on selected slot
                else
                {
                    if (inventoryUI.slots[i].item.itemID == 1)
                    {
                        Item temp = this.item;
                        this.AddItem(inventoryUI.slots[i].item);
                        inventoryUI.slots[i].AddItem(temp);
                        inventoryUI.slots[i].isSelected = false;
                        isSelected = false;
                        inventoryUI.ChangeInventoryList();
                        inventoryUI.AddArmor(armorSlot.item);
                    }//check if slot and item can be fitted (true)
                    else
                    {
                        inventoryUI.slots[i].isSelected = false;
                        isSelected = false;
                    }//(false)
                    Debug.Log("8");
                }////if there is item on selected slot

            }



        }
        //SetArmorProperties();
    } //all conditions to change armor items with armor slot

    public void OnHelmetSlotClicked()
    {
        isSelected = !isSelected;
        for (int i = 0; i < inventoryUI.slots.Length; i++)
        {
            if (inventoryUI.slots[i].isSelected && inventoryUI.slots[i] != this)
            {
                if (inventoryUI.slots[i].item == null)
                {
                    Item temp = inventoryUI.slots[i].item;
                    inventoryUI.slots[i].AddItem(this.item);
                    this.AddItem(temp);
                    inventoryUI.slots[i].isSelected = false;
                    isSelected = false;
                    inventoryUI.ChangeInventoryList();
                    inventoryUI.AddHelmet(helmetSlot.item);
                    Debug.Log("6");
                }//if there is no item u selected before 
                else if (this.item == null)
                {
                    if (inventoryUI.slots[i].item.itemID == 2)
                    {
                        Item temp = this.item;
                        this.AddItem(inventoryUI.slots[i].item);
                        inventoryUI.slots[i].AddItem(temp);
                        inventoryUI.slots[i].isSelected = false;
                        inventoryUI.ChangeInventoryList();
                        inventoryUI.AddHelmet(helmetSlot.item);
                        isSelected = false;

                    } // if item can be fitted to the slot
                    else
                    {
                        inventoryUI.slots[i].isSelected = false;
                        isSelected = false;

                    } // if item is not fitted that slot
                    Debug.Log("7");
                }//if there is no item on selected slot
                else
                {
                    if (inventoryUI.slots[i].item.itemID == 2)
                    {
                        Item temp = this.item;
                        this.AddItem(inventoryUI.slots[i].item);
                        inventoryUI.slots[i].AddItem(temp);
                        inventoryUI.slots[i].isSelected = false;
                        isSelected = false;
                        inventoryUI.ChangeInventoryList();
                        inventoryUI.AddHelmet(helmetSlot.item);
                    }//check if slot and item can be fitted (true)
                    else
                    {
                        inventoryUI.slots[i].isSelected = false;
                        isSelected = false;
                    }//(false)
                    Debug.Log("8");
                }////if there is item on selected slot

            }



        }
    }//all conditions to change helmet items with helmet slot

    public void OnGlovesSlotClicked()
    {
        isSelected = !isSelected;
        for (int i = 0; i < inventoryUI.slots.Length; i++)
        {
            if (inventoryUI.slots[i].isSelected && inventoryUI.slots[i] != this)
            {
                if (inventoryUI.slots[i].item == null)
                {
                    Item temp = inventoryUI.slots[i].item;
                    inventoryUI.slots[i].AddItem(this.item);
                    this.AddItem(temp);
                    inventoryUI.slots[i].isSelected = false;
                    isSelected = false;
                    inventoryUI.ChangeInventoryList();
                    inventoryUI.AddGloves(glovesSlot.item);
                    Debug.Log("6");
                }//if there is no item u selected before 
                else if (this.item == null)
                {
                    if (inventoryUI.slots[i].item.itemID == 3)
                    {
                        Item temp = this.item;
                        this.AddItem(inventoryUI.slots[i].item);
                        inventoryUI.slots[i].AddItem(temp);
                        inventoryUI.slots[i].isSelected = false;
                        inventoryUI.ChangeInventoryList();
                        inventoryUI.AddGloves(glovesSlot.item);
                        isSelected = false;

                    } // if item can be fitted to the slot
                    else
                    {
                        inventoryUI.slots[i].isSelected = false;
                        isSelected = false;

                    } // if item is not fitted that slot
                    Debug.Log("7");
                }//if there is no item on selected slot
                else
                {
                    if (inventoryUI.slots[i].item.itemID == 3)
                    {
                        Item temp = this.item;
                        this.AddItem(inventoryUI.slots[i].item);
                        inventoryUI.slots[i].AddItem(temp);
                        inventoryUI.slots[i].isSelected = false;
                        isSelected = false;
                        inventoryUI.ChangeInventoryList();
                        inventoryUI.AddGloves(glovesSlot.item);
                    }//check if slot and item can be fitted (true)
                    else
                    {
                        inventoryUI.slots[i].isSelected = false;
                        isSelected = false;
                    }//(false)
                    Debug.Log("8");
                }////if there is item on selected slot

            }



        }
    }//all conditions to change helmet items with helmet slot

    public void OnBootsSlotClicked()
    {
        isSelected = !isSelected;
        for (int i = 0; i < inventoryUI.slots.Length; i++)
        {
            if (inventoryUI.slots[i].isSelected && inventoryUI.slots[i] != this)
            {
                if (inventoryUI.slots[i].item == null)
                {
                    Item temp = inventoryUI.slots[i].item;
                    inventoryUI.slots[i].AddItem(this.item);
                    this.AddItem(temp);
                    inventoryUI.slots[i].isSelected = false;
                    isSelected = false;
                    inventoryUI.ChangeInventoryList();
                    inventoryUI.AddBoots(bootsSlot.item);
                    Debug.Log("6");
                }//if there is no item u selected before 
                else if (this.item == null)
                {
                    if (inventoryUI.slots[i].item.itemID == 4)
                    {
                        Item temp = this.item;
                        this.AddItem(inventoryUI.slots[i].item);
                        inventoryUI.slots[i].AddItem(temp);
                        inventoryUI.slots[i].isSelected = false;
                        inventoryUI.ChangeInventoryList();
                        inventoryUI.AddBoots(bootsSlot.item);
                        isSelected = false;

                    } // if item can be fitted to the slot
                    else
                    {
                        inventoryUI.slots[i].isSelected = false;
                        isSelected = false;

                    } // if item is not fitted that slot
                    Debug.Log("7");
                }//if there is no item on selected slot
                else
                {
                    if (inventoryUI.slots[i].item.itemID == 4)
                    {
                        Item temp = this.item;
                        this.AddItem(inventoryUI.slots[i].item);
                        inventoryUI.slots[i].AddItem(temp);
                        inventoryUI.slots[i].isSelected = false;
                        isSelected = false;
                        inventoryUI.ChangeInventoryList();
                        inventoryUI.AddBoots(bootsSlot.item);
                    }//check if slot and item can be fitted (true)
                    else
                    {
                        inventoryUI.slots[i].isSelected = false;
                        isSelected = false;
                    }//(false)
                    Debug.Log("8");
                }////if there is item on selected slot

            }



        }
    }

    public void ShowAmount()
    {
        if (item)
        {
            if (item.itemID == 0)
            {
                LootItem lootItem = (LootItem)item;
                switch (lootItem.lootID)
                {
                    case 0:
                        if (InventoryPlayer.playerInventory.stoneAmount > 1)
                        {
                            text.enabled = true;
                            text.text = InventoryPlayer.playerInventory.stoneAmount.ToString();
                        }
                        else
                        {
                            text.enabled = false;
                        }
                        break;
                    case 1:
                        if (InventoryPlayer.playerInventory.woodAmount > 1)
                        {
                            text.enabled = true;
                            text.text = InventoryPlayer.playerInventory.woodAmount.ToString();
                        }
                        else
                        {
                            text.enabled = false;
                        }
                        break;
                    case 2:
                        if (InventoryPlayer.playerInventory.steelAmount > 1)
                        {
                            text.enabled = true;
                            text.text = InventoryPlayer.playerInventory.steelAmount.ToString();
                        }
                        else
                        {
                            text.enabled = false;
                        }
                        break;
                    case 3:
                        if (InventoryPlayer.playerInventory.coalAmount > 1)
                        {
                            text.enabled = true;
                            text.text = InventoryPlayer.playerInventory.coalAmount.ToString();
                        }
                        else
                        {
                            text.enabled = false;
                        }
                        break;
                    case 4:
                        if (InventoryPlayer.playerInventory.clayAmount > 1)
                        {
                            text.enabled = true;
                            text.text = InventoryPlayer.playerInventory.clayAmount.ToString();
                        }
                        else
                        {
                            text.enabled = false;
                        }
                        break;
                    case 5:
                        if (InventoryPlayer.playerInventory.leatherAmount > 1)
                        {
                            text.enabled = true;
                            text.text = InventoryPlayer.playerInventory.leatherAmount.ToString();
                        }
                        else
                        {
                            text.enabled = false;
                        }
                        break;
                    case 6:
                        if (InventoryPlayer.playerInventory.boneAmount > 1)
                        {
                            text.enabled = true;
                            text.text = InventoryPlayer.playerInventory.boneAmount.ToString();
                        }
                        else
                        {
                            text.enabled = false;
                        }
                        break;
                    case 7:
                        if (InventoryPlayer.playerInventory.glassAmount > 1)
                        {
                            text.enabled = true;
                            text.text = InventoryPlayer.playerInventory.glassAmount.ToString();
                        }
                        else
                        {
                            text.enabled = false;
                        }
                        break;
                    case 8:
                        if (InventoryPlayer.playerInventory.diamondAmount > 1)
                        {
                            text.enabled = true;
                            text.text = InventoryPlayer.playerInventory.diamondAmount.ToString();
                        }
                        else
                        {
                            text.enabled = false;
                        }
                        break;
                    case 9:
                        if (InventoryPlayer.playerInventory.bigPotsAmount > 1)
                        {
                            text.enabled = true;

                        }
                        else
                        {
                            text.enabled = false;
                        }
                        if (text.enabled)
                        {
                            text.text = InventoryPlayer.playerInventory.bigPotsAmount.ToString();
                        }
                        break;
                    case 10:
                        if (InventoryPlayer.playerInventory.middlePotsAmount > 1)
                        {
                            text.enabled = true;
                        }
                        else
                        {
                            text.enabled = false;
                        }
                        if (text.enabled)
                        {
                            text.text = InventoryPlayer.playerInventory.middlePotsAmount.ToString();
                        }
                        break;
                    case 11:
                        if (InventoryPlayer.playerInventory.smallPotsAmount > 1)
                        {
                            text.enabled = true;
                        }
                        else
                        {
                            text.enabled = false;
                        }
                        if (text.enabled)
                        {
                            text.text = InventoryPlayer.playerInventory.smallPotsAmount.ToString();
                        }
                        break;

                }
            }
            else
            {
                if (text)
                    text.enabled = false;
                //text.text = InventoryPlayer.instance.woodAmount.ToString();
            }
        }
        else if (!item && text)
        {
            text.enabled = false;
        }
    }

    public void WeaponChangePlayerStats(int minDamage, int maxDamage, float animationID)
    {

        playerStats.SetWeaponProperties(minDamage, maxDamage, animationID);


        // Debug.Log("SpeedAttacK: " + speedAttack + "\nDamage: " + damage + "\nAniamcja: " + animationID);
    }

    void ArmorChangePlayerStats(int armor)
    {
        playerStats.armor.SetValue(playerStats.armor.GetValue() + armor);
    }

}
