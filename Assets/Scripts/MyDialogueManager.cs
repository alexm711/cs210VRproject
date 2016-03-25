using UnityEngine;
using System.Collections;

public class MyDialogueManager : MonoBehaviour {
	public GameObject canvas;
    public GameObject text;

    public Color aliceColor = Color.cyan;
    public Color ellieColor = Color.yellow;
    public Color promptColor = Color.gray;

    private GameObject t;
	// Use this for initialization
	void Start () {

        int ypos = Screen.height / 3;

        var t = Instantiate (text) as GameObject;
		t.transform.SetParent (canvas.transform);
		t.GetComponent<UnityEngine.UI.Text> ().text = alice1;
        t.transform.position = new Vector3(0, -ypos, 0);

        StartCoroutine(startDialogue(2f));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator startDialogue(float numSeconds)
    {
        yield return new WaitForSeconds(numSeconds);

        t.GetComponent<UnityEngine.UI.Text>().text = alice1;
        t.GetComponent<UnityEngine.UI.Text>().color = aliceColor;

        float startWaitTime = Time.time;

        bool timeout = true;
        while(Time.time - startWaitTime < 3f)
        {
            if (Input.GetKey(KeyCode.A))
            {
                StartCoroutine(playResponseDialogue());
                timeout = false;
                break;
            }

            yield return new WaitForEndOfFrame();
         }
        if (timeout)
        {
            StartCoroutine(playTimeoutDialogue());
        }
    }


    IEnumerator playResponseDialogue()
    {
        t.GetComponent<UnityEngine.UI.Text>().text = ellie1;
        t.GetComponent<UnityEngine.UI.Text>().color = ellieColor;
        yield return new WaitForSeconds(2f);

        t.GetComponent<UnityEngine.UI.Text>().text = alice2;
        t.GetComponent<UnityEngine.UI.Text>().color = aliceColor;
        yield return new WaitForSeconds(2f);

        t.GetComponent<UnityEngine.UI.Text>().text = alice3;

        while (true)
        {
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
            {
                break;
            }

            yield return new WaitForEndOfFrame();
        }

        t.GetComponent<UnityEngine.UI.Text>().text = ellie2;
        t.GetComponent<UnityEngine.UI.Text>().color = ellieColor;

        yield return new WaitForSeconds(2f);
        t.GetComponent<UnityEngine.UI.Text>().text = alice4;
        t.GetComponent<UnityEngine.UI.Text>().color = aliceColor;


    }

    IEnumerator playTimeoutDialogue()
   {
        yield return new WaitForSeconds(2f);
        t.GetComponent<UnityEngine.UI.Text>().text = alice5;
        t.GetComponent<UnityEngine.UI.Text>().color = aliceColor;

    }

    private string alice1 = "Alice: Ellie, come in. Ellie, do you read? Ellie?";

    //If respond timely
    private string ellie1 = "Ellie: Hi?";
    private string alice2 = "Alice: Do you not...";
    private string alice3 = "Alice: Tell me, can you move?";
    private string ellie2 = "Ellie: I can fly";
    private string alice4 = "Alice: Good for you";

    //Does not respond timely
    private string alice5 = "Ellie, I know you can hear me.";
    //private string alice6 = "Your vitals are right on my screen, and it looks like you're quite alert.";
    
}
