using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class trackGoals : MonoBehaviour {

    public int count;
    public Text countText;
    public Text timerText;
    public GameObject TheEnd;
    public Text EndTimer;
    public float timeleft;
    Animator anim;
	// Use this for initialization
	void Start () {
        count = 0;
        SetCountText();
        //Debug.Log("start dwag " + count);

    }

    void Update()
    {
        timeleft -= Time.deltaTime;
        SetTimeText();

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
        //if (count == 1)
        //{
        //    //SceneManager.LoadScene("TheWreckage");
        //    Application.Quit();
        //}
        Transform Child = other.gameObject.transform.parent;
        //Child.parent = new GameObject("shell").transform;
        //Child.position = Vector3.zero;
        //anim = Child.gameObject.GetComponent<Animator>();
        //int triggerhash = Animator.StringToHash("EnterGoal");
        //Child.position = other.gameObject.GetComponent<Rigidbody>().position;
        float[] xcoords = new float[] { -1f, 1f, -2f, 0f, 2f };
        float[] ycoords = new float[] { 0, 0, -1.5f, -1.5f, -1.5f };
        float[] zcoords = new float[] { 0, 0, 2f, 2f, 2f };
        Child.position = new Vector3(xcoords[count-1], ycoords[count - 1], zcoords[count - 1]);
        other.transform.position = Vector3.zero;
        //Child.position.Set(Child.position.x + 15f, Child.position.y - 15f, Child.position.z);

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
        countText.text = "Crew Saved: " + count.ToString() + "/5";

    }

    void SetTimeText()
    {
        if (timeleft < 5)
        {
            int entimeshown = (int)timeleft;
            TheEnd.SetActive(true);
            EndTimer.text = entimeshown.ToString();
        }
        if (timeleft < 30)
        {
            float timeshown = timeleft;
            if ((int) timeshown % 2 == 0)
            {
                timerText.color = Color.yellow;
            }
            else
            {
                timerText.color = Color.red;
            }
            timerText.text = timeshown.ToString();
        }
        else
        {
            int timeshown = (int) timeleft;
            timerText.text = timeshown.ToString();
        }
        if (timeleft < 0)
        {
            Application.Quit();
        }
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
