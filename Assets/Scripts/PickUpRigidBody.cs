using UnityEngine;
using System.Collections;

public class PickUpRigidBody : MonoBehaviour {
    Camera mainCamera;
    bool carrying;
    GameObject carriedObject;
    public float smooth;
    public float distance;
    public float maxDistance;
    float currdistance;

    public float springforce = 50.0f;

    private SpringJoint springJoint;
    public float dampeningRatio = 5.0f;
    public float drag = 10.0f;
    public float angularDrag = 5.0f;


    // Use this for initialization
    void Start () {
        mainCamera = GameObject.FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update () {
        if (!Input.GetButtonDown("Xbox_360_RightStickClick")) return;
        int x = Screen.width / 2;
        int y = Screen.height / 2;
        Ray ray = mainCamera.GetComponent<Camera>().ScreenPointToRay(new Vector3(x, y));
        RaycastHit hit;
        if (!Physics.Raycast(ray, out hit))
            return;
        pickupable p = hit.collider.GetComponent<pickupable>();
        currdistance = Vector3.Distance(hit.point, mainCamera.transform.position);
        if (currdistance > maxDistance)
        {
            if (Input.GetButton("Xbox_360_LeftStickClick"))
            {
                currdistance = distance;
            }else
            {
                return;
            }
        }
        if (p == null) return;
        carrying = true;
        if (!springJoint){
            GameObject obj = new GameObject("Rigidbody dragger");
            Rigidbody body = obj.AddComponent<Rigidbody>();
            springJoint = obj.AddComponent<SpringJoint>();
            body.isKinematic = true;
        }
        carriedObject = p.gameObject;

        springJoint.transform.position = hit.point;
        //springJoint.anchor = Vector3.zero;
        springJoint.connectedAnchor = hit.transform.InverseTransformPoint(hit.point);
        springJoint.damper = dampeningRatio;
        springJoint.spring = springforce;
        //springJoint.frequency = frequency;
        //springJoint.collideConnected = false;
        springJoint.connectedBody = hit.rigidbody;
        StartCoroutine(DragObject());
	}
    IEnumerator DragObject()
    {
        float oldDrag = this.springJoint.connectedBody.drag;
        float oldAngularDrag = this.springJoint.connectedBody.angularDrag;

        springJoint.connectedBody.drag = drag;
        springJoint.connectedBody.angularDrag = angularDrag;

        //
        // The spring joint's position becomes 
        //
        while (Input.GetButton("Xbox_360_RightStickClick"))
        {
            springJoint.transform.position = Vector3.Lerp(springJoint.transform.position, mainCamera.transform.position + mainCamera.transform.forward * currdistance, Time.deltaTime * smooth);
            yield return null;
        }

        //
        // The player released the mouse button, so the spring joint is now
        // detached. The spring joint can be used again later.
        //
        if (springJoint.connectedBody)
        {
            springJoint.connectedBody.drag = oldDrag;
            springJoint.connectedBody.angularDrag = oldAngularDrag;
            springJoint.connectedBody = null;
        }
    }
}
