using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerController : MonoBehaviour {

    public Interactable focus;

    float camRayLength = 100f;
    public LayerMask floorMask;
    GameObject inventory;
    PlayerMovement playerMovement;

    public GameObject Particles;

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

        if (newFocus.tag == "Enemy")
            CreateParticle(Particles, newFocus);
        newFocus.OnFocused(transform);
    }

    void CreateParticle(GameObject Prefab, Interactable Target)
    {
        GameObject Particles = GameObject.Find("Target particles(Clone)");
        GameObject temp;

        if (Particles == null)
        { 
            temp = Instantiate(Prefab) as GameObject;
            temp.transform.SetParent(Target.transform);
            temp.transform.localPosition = Prefab.transform.localPosition;
            temp.transform.localScale = Prefab.transform.localScale;
            temp.transform.localRotation = Prefab.transform.localRotation;
        }
        else
        {
            temp = Particles;
            Destroy(temp);
            temp = Instantiate(Prefab) as GameObject;
            temp.transform.SetParent(Target.transform);
            temp.transform.localPosition = Prefab.transform.localPosition;
            temp.transform.localScale = Prefab.transform.localScale;
            temp.transform.localRotation = Prefab.transform.localRotation;
        }

    }

    void DestroyParticle()
    {
        GameObject Particles = GameObject.Find("Target particles(Clone)");
        if (Particles != null)
            Destroy(Particles);
    }


    public void RemoveFocus()
    {
        if (focus != null)
            focus.OnDefocused();

        focus = null;
        DestroyParticle();
        playerMovement.StopFollowingTarget();
    }
}
