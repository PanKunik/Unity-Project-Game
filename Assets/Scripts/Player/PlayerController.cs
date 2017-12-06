using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerController : MonoBehaviour {

    public Interactable focus;

    float camRayLength = 100f;
    public LayerMask floorMask;
    GameObject inventory;
    PlayerMovement playerMovement;

	// Use this for initialization
	void Start () {
        playerMovement = GetComponent<PlayerMovement>();
        inventory = GameObject.FindGameObjectWithTag("Inventory");
		inventory.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.I))
        {
            inventory.SetActive(!inventory.activeSelf);

        } 

        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (Input.GetMouseButton(0))
        {

            Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitRay;

            if (Physics.Raycast(camRay, out hitRay, camRayLength, floorMask))
            {
                playerMovement.MoveToPoint(hitRay.point);

                RemoveFocus();
                //Debug.Log("We hit: " + hitRay.point);
            }

        }

        if (Input.GetMouseButtonDown(1))
        {
            Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitRay;

            if (Physics.Raycast(camRay, out hitRay, camRayLength))
            {
                Interactable interactable = hitRay.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    SetFocus(interactable);
                }
            }
        }

        
    }

    void SetFocus(Interactable newFocus)
    {
        if( newFocus != focus )
        {
            if( focus != null )
                focus.OnDefocused();

            focus = newFocus;
            playerMovement.FollowTarget(newFocus);
        }

        newFocus.OnFocused(transform);

    }

    void RemoveFocus()
    {
        if (focus != null)
            focus.OnDefocused();

        focus = null;
        playerMovement.StopFollowingTarget();
    }
}
