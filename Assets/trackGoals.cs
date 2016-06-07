using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class trackGoals : MonoBehaviour {

    public int count;
    public Text countText;
    Animator anim;
	// Use this for initialization
	void Start () {
        count = 0;
        SetCountText();
        //Debug.Log("start dwag " + count);

    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        human p = other.gameObject.GetComponent<human>();
        if (p == null) return;
        StartCoroutine(bs(other));

        //Debug.Log("counht: " + count);

    }
    IEnumerator bs(Collider other)
    {        //Debug.Log("counht: " + count);
        count += 1;
        SetCountText();
        yield return new WaitForSeconds(2);
        Transform Child = other.gameObject.transform.parent;
        //Child.parent = new GameObject("shell").transform;
        //Child.position = Vector3.zero;
        //anim = Child.gameObject.GetComponent<Animator>();
        //int triggerhash = Animator.StringToHash("EnterGoal");
        Child.position = other.gameObject.GetComponent<Rigidbody>().position;
        Child.position.Set(Child.position.x + 15f, Child.position.y - 15f, Child.position.z);

        Child.gameObject.GetComponent<Animator>().enabled = true;
        //Child.position = other.gameObject.GetComponent<Rigidbody>().position;
        //Child.position.Set(Child.position.x, Child.position.y - 15f, Child.position.z);
        //anim.SetTrigger(triggerhash);
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
