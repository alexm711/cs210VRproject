using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class trackGoals : MonoBehaviour {

    public int count;
    public Text countText;
	// Use this for initialization
	void Start () {
        count = 0;
        SetCountText();
        //Debug.Log("start dwag " + count);

    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("counht: " + count);
        human p = other.gameObject.GetComponent<human>();
        if (p == null) return;
        count += 1;
        SetCountText();

        //Debug.Log("counht: " + count);

    }
    void OnTriggerExit(Collider other)
    {
        //Debug.Log("exit counht: " + count);

        human p = other.gameObject.GetComponent<human>();
        if (p == null) return;
        count -= 1;
        SetCountText();

    }
    void SetCountText()
    {
        countText.text = "Crew Saved: " + count.ToString();

    }
    //void Update () {

    //       pickupable p = hit.collider.GetComponent<pickupable>();
    //       currdistance = Vector3.Distance(hit.point, mainCamera.transform.position);
    //       if (currdistance > maxDistance)
    //       {
    //           if (Input.GetButton("Xbox_360_LeftStickClick"))
    //           {
    //               currdistance = distance;
    //           }
    //           else
    //           {
    //               return;
    //           }
    //       }
    //       if (p == null) return;

    //   }
}
