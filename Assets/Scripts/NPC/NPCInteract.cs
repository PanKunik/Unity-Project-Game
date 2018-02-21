using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCInteract : Interactable
{

    GameObject HUDCanvas;
    public GameObject mouseItem;

    protected PlayerManager playerManager;
    // Use this for initialization
    public void Start()
    {
        playerManager = PlayerManager.instance;
        HUDCanvas = GameObject.Find("HUDCanvas");
        mouseItem = HUDCanvas.transform.GetChild(2).gameObject;

    }

    // Update is called once per frame
    new public void Update()
    {
        base.Update();
        if (GetIsFocused())
        {
            FaceTarget();

        }

        if (mouseItem.activeSelf == true)
          mouseItem.transform.position = Input.mousePosition;
    }
    public override void Interact()
    {
        base.Interact();
        print("interact");

    }
    void FaceTarget()
    {
        Vector3 direction = (playerManager.player.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
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
