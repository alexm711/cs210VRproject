using UnityEngine;
using System.Collections;

public class DialogueManager : MonoBehaviour {

    public GameObject button;
	// Use this for initialization
	void Start () {
        GUI.Label(new Rect(0f, 0f, 500, 1000), alice1);
        //startDialogue();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator startDialogue()
    {
        yield return new WaitForSeconds(3f);
        Debug.Log("Got here");
        
        //var button = Instantiate(button) as GameObject;
    }


    private string alice1 = "Ellie, come in. Ellie, do you read? Ellie?";

    //If respond timely
    private string ellie1 = "Hi?";
    private string alice2 = "Do you not...";
    private string alice3 = "Tell me, can you move?";
}
