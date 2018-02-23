using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerController : MonoBehaviour
{

    public Interactable focus;

    float camRayLength = 100f;
    public LayerMask floorMask;
    public LayerMask enemyMask;
    GameObject PlayerInventory;
    PlayerMovement playerMovement;

    public GameObject Particles;
    public GameObject PointerPrefab;

    // Use this for initialization
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        PlayerInventory = GameObject.FindGameObjectWithTag("Inventory");
        PlayerInventory.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.I) && PauseMenu.IsGamePaused == false)
        {
            PlayerInventory.SetActive(!PlayerInventory.activeSelf);
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
                DestinationPoint(hitRay.point);
                //Debug.Log("We hit: " + hitRay.point);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitRay;

            if (Physics.Raycast(camRay, out hitRay, camRayLength, enemyMask))
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
        if (newFocus != focus)
        {
            if (focus != null)
                focus.OnDefocused();

            focus = newFocus;
            playerMovement.FollowTarget(newFocus);

            // Zniszcz pointer
            GameObject Pointer = GameObject.Find("Pointer(Clone)");
            Destroy(Pointer);
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

    void RemoveFocus()
    {
        if (focus != null)
            focus.OnDefocused();

        focus = null;
        DestroyParticle();
        playerMovement.StopFollowingTarget();
    }

    public void RemoveFocusOnDeath()
    {
        if (focus != null)
            focus.OnDefocused();

        focus = null;
        DestroyParticle();
        playerMovement.StopFollowingTargetOnDeath();
    }

    void DestinationPoint(Vector3 DestinationPoint)
    {
        GameObject Pointer = GameObject.Find("Pointer(Clone)");
        if (Pointer != null)
        {
            ChangePointerPosition(Pointer, DestinationPoint);
            StartCoroutine(DestroyPointer(Pointer, DestinationPoint));
        }
        else
        {
            SpawnPointer(PointerPrefab, DestinationPoint);
        }
    }

    void SpawnPointer(GameObject Prefab, Vector3 Point)
    {
        GameObject temp = Instantiate(Prefab) as GameObject;
        temp.transform.localPosition = Point;
        temp.transform.localRotation = Prefab.transform.localRotation;
        temp.transform.localScale = Prefab.transform.localScale;
    }

    void ChangePointerPosition(GameObject Pointer, Vector3 Point)
    {
        Pointer.transform.localPosition = Point;
    }

    IEnumerator DestroyPointer(GameObject Pointer, Vector3 DestinationPoint)
    {
        while (Vector3.Distance(DestinationPoint, transform.localPosition) > 0.2f)
            yield return new WaitForSeconds(0.1f);

        Destroy(Pointer);

        yield return null;
    }
}
