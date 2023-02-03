using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    public Camera targetCamera;
    public Transform handVis;

    public GameObject openHand;
    public GameObject closedHand;
    public GameObject closedHandWithRoot;

    private void Start()
    {
        if (targetCamera == null)
        {
            targetCamera = Camera.main;
        }
    }

    void Update()
    {
        PlayerHand();
        //Mouse Input [left = 0, right = 1, middle = 2]
        MouseInput(Input.GetMouseButton(0));
    }

    private void PlayerHand()
    {
        Ray inputRay = targetCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(inputRay, out RaycastHit hitInfo))
        {
            if (!handVis.gameObject.activeInHierarchy)
            {
                handVis.gameObject.SetActive(true);
            }
            Vector3 contactPoint = hitInfo.point;
            handVis.position = contactPoint;
        }
        else if (handVis.gameObject.activeInHierarchy)
        {
            handVis.gameObject.SetActive(false);
        }
    }

    private void MouseInput(bool mousePress)
    {
        if (handVis.gameObject.activeInHierarchy)
        {
            openHand.SetActive(!mousePress);

            closedHand.SetActive(mousePress);
        }
    }
}
