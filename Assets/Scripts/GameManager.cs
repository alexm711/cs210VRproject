using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public UIManager PauseUI;
    //public static DialogueManager DialogueUI;
    private GameObject prevSelectedGameObject;
	private GameObject prevSelectedMenuGameObject;
	public bool subtitlesToggle = true;

	// Use this for initialization
	void Start () {
        PauseUI.GetComponentInChildren<Canvas>().enabled = false;
        prevSelectedMenuGameObject = GameObject.Find("Fixed_Angular_Speed_Toggle");
        //ToggleSubtitles();
    }
    void Awake()
    {
        GameObject.Find("SF Unity UI Dialogue UI Response Template/Dialogue_Panel/NPC_Panel").GetComponent<CanvasGroup>().alpha = 0f;
        GameObject.Find("SF Unity UI Dialogue UI Response Template/Dialogue_Panel/PC Subtitle Line").GetComponent<CanvasGroup>().alpha = 0f;
        GameObject.Find("SF Unity UI Dialogue UI Response Template/Dialogue_Panel/NPC Reminder Panel").GetComponent<CanvasGroup>().alpha = 0f;
    }

    // Update is called once per frame
    //	void Update () {
    //	
    //	}
    //	UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(continueButton.gameObject, null);
    //	UnityEngine.EventSystems.EventSystem.curre
    public void ResetLevel(){
        //		SceneManager.UnloadScene ("TheWreckage");
        Application.Quit();

        Scene scene = SceneManager.GetActiveScene ();
//		UnityUISubtitleControls
		PixelCrushers.DialogueSystem.PersistentDataManager.Reset (DatabaseResetOptions.RevertToDefault);
		SceneManager.LoadScene(scene.buildIndex);
		Debug.Log("Doesn't work with dialogue system for some reason");

//		Application.LoadLevel(Application.loadedLevel);
	}
	public void ToggleSubtitles(){
        //		PixelCrushers.DialogueSystem.DialogueManager.UnityUISubtitleControls.HidePanel();

        subtitlesToggle = subtitlesToggle ? false : true;
        Debug.Log("Doesn't work with dialogue system for some reason" +subtitlesToggle );

//		PixelCrushers.DialogueSystem.OverrideDisplaySettings.
//		var
//		toggle =
//		DialogueManager.DisplaySettings.subtitleSettings.showNPCSubtitlesDuringLine = subtitlesToggle;
//		DialogueManager.DisplaySettings.subtitleSettings.showPCSubtitlesDuringLine = subtitlesToggle;
//		DialogueManager.DisplaySettings.subtitleSettings.showNPCSubtitlesDuringLine = subtitlesToggle;
//		GameObject t = GameObject.Find ("SF Unity UI Dialogue UI Response Template/Dialogue_Panel/NPC_Panel");
		GameObject.Find ("SF Unity UI Dialogue UI Response Template/Dialogue_Panel/NPC_Panel").GetComponent<CanvasGroup>().alpha = subtitlesToggle ? 1f : 0f;
        GameObject.Find ("SF Unity UI Dialogue UI Response Template/Dialogue_Panel/PC Subtitle Line").GetComponent<CanvasGroup>().alpha = subtitlesToggle ? 1f : 0f;
        GameObject.Find ("SF Unity UI Dialogue UI Response Template/Dialogue_Panel/NPC Reminder Panel").GetComponent<CanvasGroup>().alpha = subtitlesToggle ? 1f : 0f;


        //		h.alpha = 0.2f;
        //		GameObject.Find ("Dialogue_Panel/Dialogue_Panel_Frame").GetComponent<CanvasGroup>.alpha = 0;
        //		GameObject.Find ("Dialogue_Panel/NPC_Panel").GetComponent<CanvasGroup>.alpha = false;
        //		GameObject.Find ("Dialogue_Panel/PC Subtitle Line").GetComponent<CanvasGroup>.alpha = false;
    }
	public void TogglePauseMenu(){
		if (PauseUI.GetComponentInChildren<Canvas>().enabled)
		{
            prevSelectedMenuGameObject = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
            GameObject.Find("Dialogue Canvas").GetComponent<Canvas>().enabled = true;
            PauseUI.GetComponentInChildren<Canvas>().enabled = false;
            UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(GameObject.Find("Response Button Template(Clone)"), null);

   //         if (prevSelectedGameObject != null) {
			//	UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject (prevSelectedGameObject, null);
			//} else {
			//	UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject (GameObject.Find ("Response Button Template(Clone)"), null);

			//}
			Time.timeScale = 1.0f;
			DialogueTime.Mode = DialogueTime.TimeMode.Gameplay;
		}
		else
		{
			//prevSelectedGameObject = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
            PauseUI.GetComponentInChildren<Canvas>().enabled = true;
            GameObject.Find("Dialogue Canvas").GetComponent<Canvas>().enabled = false;
            UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject (prevSelectedMenuGameObject, null);
			Time.timeScale = 0f;
			DialogueTime.Mode = DialogueTime.TimeMode.Realtime;

		}

		//Debug.Log("GAMEMANAGER:: TimeScale: " + Time.timeScale);
	}
}
