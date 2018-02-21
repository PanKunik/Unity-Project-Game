using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RespawnPointInteract : Interactable {

    PlayerStats playerStats;
    Animator anim;
    public GameObject textPointSaved;

    GameObject HUDCanvas;
    public GameObject mouseItem;

    void Start () {
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        anim = gameObject.GetComponent<Animator>();

        HUDCanvas = GameObject.Find("HUDCanvas");
        mouseItem = HUDCanvas.transform.GetChild(2).gameObject;
    }

    // Update is called once per frame
    protected override void Update () {
        base.Update();

        if (mouseItem.activeSelf == true)
        {
            mouseItem.transform.position = Input.mousePosition + new Vector3(40,40,40);
        }
    }

    public override void Interact()
    {
        base.Interact();
        playerStats.RespawnPoint = gameObject.transform.position;
        anim.SetTrigger("Interact");
        InitCBT("Respawn point saved!", textPointSaved);
    }

    void InitCBT(string text, GameObject Prefab)
    {
        GameObject temp = Instantiate(Prefab) as GameObject;
        RectTransform tempRect = temp.GetComponent<RectTransform>();
        temp.transform.SetParent(GameObject.Find("RespawnPointCanvas").transform);
        tempRect.transform.localPosition = Prefab.transform.localPosition;
        tempRect.transform.localScale = Prefab.transform.localScale;
        tempRect.transform.localRotation = Prefab.transform.localRotation;

        temp.GetComponent<Text>().text = text;
    }

    public void DestroyMe()
    {
        Destroy(gameObject);
    }

    private void OnMouseOver()
    {
        if (mouseItem && (mouseItem.activeSelf == false) && PauseMenu.IsGamePaused == false)
        {
            mouseItem.SetActive(true);
            // mouseItem.transform.position = Input.mousePosition;
            // mouseItem.GetComponent<TextMeshPro>().text = item.name + count;
        }
    }

    private void OnMouseExit()
    {
        if (mouseItem && mouseItem.activeSelf)
            mouseItem.SetActive(false);
    }
}
