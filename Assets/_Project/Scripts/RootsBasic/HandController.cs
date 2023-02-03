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
    public GameObject withRoot;

    public RaycastHit hitInfo;
    public LayerMask pointerLayer;
    public LayerMask rootsLayer;

    public List<IRoot> pickedRoots;

    private void Start()
    {
        //Cursor.visible = false;
        Application.targetFrameRate = 60;
        if (targetCamera == null)
        {
            targetCamera = Camera.main;
        }
        pickedRoots = new List<IRoot>();
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

        if (Physics.Raycast(inputRay, out hitInfo, Mathf.Infinity, pointerLayer))
        {
            Vector3 contactPoint = hitInfo.point;
            handVis.position = contactPoint + (-handDistance * inputRay.direction);
        }
    }

    private void MouseInput(bool mousePress)
    {
        if (!mousePress)
        {
            foreach (var root in pickedRoots)
            {
                root.onRelease();
            }

            pickedRoots = new List<IRoot>();
        }

        openHand.SetActive(!mousePress);
        closedHand.SetActive(mousePress);

        if (pickedRoots.Count > 0)
        {
            withRoot.SetActive(mousePress);
        }
        else withRoot.SetActive(false);
    }

    private void Grab()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Collider[] hits = Physics.OverlapSphere(hitInfo.point, 1, rootsLayer);
            pickedRoots = FilterRoots(hits);
        }
    }

    private List<IRoot> FilterRoots(Component[] colliders)
    {
        List<IRoot> foundRoots = new List<IRoot>();
        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent(out IRoot root))
            {
                foundRoots.Add(root);
                root.onGrab(handVis);
                Debug.Log("Hit");
            }
        }

        return foundRoots;
    }
}
