using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    public Camera targetCamera;
    public Transform handVis;
    public float handDistance;

    public GameObject openHand;
    public GameObject closedHand;
    public GameObject closedHandWithRoot;

    public RaycastHit hitInfo;
    public LayerMask rootsLayer;

    private void Start()
    {
        //Cursor.visible = false;
        Application.targetFrameRate = 60;
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
        Grab();
    }

    private void PlayerHand()
    {
        Ray inputRay = targetCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(inputRay, out hitInfo))
        {
            Vector3 contactPoint = hitInfo.point;
            handVis.position = contactPoint + (-handDistance * inputRay.direction);
        }
    }

    private void MouseInput(bool mousePress)
    {
        openHand.SetActive(!mousePress);
        closedHand.SetActive(mousePress);
    }

    private void Grab()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Collider[] hits = Physics.OverlapSphere(hitInfo.point, 1, rootsLayer);

        }
    }
}
