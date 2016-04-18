using UnityEngine;
using PixelCrushers.DialogueSystem;

public class UnityUIDialogueUIExtensions : UnityUIDialogueUI
{

    public override void ShowResponses(Subtitle subtitle, Response[] responses, float timeout)
    {
        // (Any extra processing before showing responses goes here.)
        base.ShowResponses(subtitle, responses, timeout);

        //for (int i = 0; i < dialogue.responseMenu.instantiatedButtons.Count; i++)
        //{
        //    var button = dialogue.responseMenu.instantiatedButtons[i];
        //    var nav = button.GetComponent<UnityEngine.UI.Button>().navigation;
        //    nav.selectOnDown = dialogue.responseMenu.instantiatedButtons[(i + 1) % dialogue.responseMenu.instantiatedButtons.Count].GetComponent<UnityEngine.UI.Button>();
        //    nav.selectOnUp = dialogue.responseMenu.instantiatedButtons[(i-1) % dialogue.responseMenu.instantiatedButtons.Count].GetComponent<UnityEngine.UI.Button>();
        //}

        Debug.Log("Hi");
    }


}