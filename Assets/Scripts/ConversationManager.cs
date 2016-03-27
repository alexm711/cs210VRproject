using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;

public class ConversationManager : MonoBehaviour {

    bool conversationPlayed = false;
    bool respondedInTime = false;
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKey(KeyCode.A) && !conversationPlayed)
        {
            DialogueManager.Instance.StartConversation("Opening");
            conversationPlayed = true;
        }
	}

    public void startTimeoutForResponse()
    {
        Debug.Log("Called timeout response");
        StartCoroutine(playTimeoutConversation());
    }

    IEnumerator playTimeoutConversation()
    {
        float time = Time.time;
        while (Time.time - time < 10f)
        {
            if(Input.GetKey(KeyCode.A))
            {
                DialogueManager.Instance.StartConversation("EllieRespondsToAlice");
                respondedInTime = true;
            }
            yield return new WaitForEndOfFrame();
        }

        if(!respondedInTime)
        {
            DialogueManager.Instance.StartConversation("AliceAnnoyed");
        }
    }

}
