using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    public static float handRadiousMult = 1;

    public Camera targetCamera;
    public Transform handVis;
    public float handDistance;

    public GameObject openHand;
    public GameObject closedHand;

    public RaycastHit hitInfo;
    public LayerMask pointerLayer;
    public LayerMask rootsLayer;

    public Vector3 grabPoint;
    public List<IRoot> pickedRoots;
    public string toolAquired;

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
            handVis.position = CalculateHandPosition(inputRay);
        }
    }

    private Vector3 CalculateHandPosition(Ray inputRay)
    {
        Vector3 contactPoint = hitInfo.point;
        Vector3 targetPosition = contactPoint + (-handDistance * inputRay.direction);

        if (pickedRoots.Count > 0)
        {
            //handVis.position = targetPosition;
            float radius = 4; //radius of *black circle*
            Vector3 centerPosition = grabPoint; //center of *black circle*
            float distance = Vector3.Distance(targetPosition, centerPosition); //distance from ~green object~ to *black circle*
            Debug.Log(distance);

            if (distance > radius) //If the distance is less than the radius, it is already within the circle.
            {
                Vector3 fromOriginToObject = targetPosition - centerPosition; //~GreenPosition~ - *BlackCenter*
                fromOriginToObject *= radius / distance; //Multiply by radius //Divide by Distance
                targetPosition = centerPosition + fromOriginToObject; //*BlackCenter* + all that Math
                return targetPosition;
            }
        }
        
        return targetPosition;
    }

    private void MouseInput(bool mousePress)
    {
        if (!mousePress)
        {
            foreach (var root in pickedRoots)
            {
                root.OnRelease();
            }

            pickedRoots.Clear();
        }

        openHand.SetActive(!mousePress);
        closedHand.SetActive(mousePress);
    }

    private void Grab()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Collider[] hits = Physics.OverlapSphere(hitInfo.point, 1 * handRadiousMult, rootsLayer);
            pickedRoots = FilterRoots(hits);

            if (pickedRoots.Count > 0)
            {
                grabPoint = hitInfo.point;
            }
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
                root.OnGrab(handVis);
                root.OnPulledRoot.AddListener(() => pickedRoots.Remove(root));
            }
        }

        return foundRoots;
    }
}
