using UnityEngine;
using System.Collections;

public class DialogueManager : MonoBehaviour {
	public GameObject canvas;
    public GameObject text;

	// Use this for initialization
	void Start () {
        int xpos = Screen.width / 2;
        int ypos = Screen.height / 3;
        int width = Screen.width / 2;
        int height = Screen.height / 8;
        int spacing = height;


        var t = Instantiate (text) as GameObject;
		t.transform.SetParent (canvas.transform);
		t.GetComponent<UnityEngine.UI.Text> ().text = alice1;
        t.transform.position = new Vector3(0, -ypos, 0);

        Debug.Log(t.GetComponent<UnityEngine.UI.Text>().rectTransform.position.ToString());


        startDialogue();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator startDialogue()
    {
        yield return new WaitForSeconds(3f);
        Debug.Log("DialogueManager - got here");
    }


    private string alice1 = "Ellie, come in. Ellie, do you read? Ellie?";

    //If respond timely
    private string ellie1 = "Hi?";
    private string alice2 = "Do you not...";
    private string alice3 = "Tell me, can you move?";
}
