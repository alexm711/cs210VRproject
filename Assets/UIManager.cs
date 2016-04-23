using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public GameManager GM;
	public MusicManager MM;

	private Slider _musicSlider;

	// Use this for initialization
	void Start ()
	{
		//--------------------------------------------------------------------------
		// Game Settings Related Code


		//--------------------------------------------------------------------------
		// Music Settings Related Code
		_musicSlider = GameObject.Find("Music_Slider").GetComponent<Slider>();
	}

	// Update is called once per frame
	void Update ()
	{
		ScanForKeyStroke();
	}

	void ScanForKeyStroke()
	{
		if (Input.GetButtonDown("Xbox_360_Start"))     GM.TogglePauseMenu();
	}

	//-----------------------------------------------------------
	// Game Options Function Definitions
//	public void OptionSliderUpdate(float val){ ... }
//	void SetCustomSettings(bool val){ ... }
//	void WriteSettingsToInputText(GameSettings settings){ ... }

	//-----------------------------------------------------------
	// Music Settings Function Definitions
	public void MusicSliderUpdate(float value)
	{
		MM.SetVolume(value);		
	}
	public void MusicChangeSong()
	{
		MM.ChangeSong ();

	}

	public void MusicToggle(bool val)
	{
		_musicSlider.interactable = val;
		MM.SetVolume(val ? _musicSlider.value : 0f);
	}
	public void ClassicModeToggle(bool val)
	{
		PlayerControllerAlex.classic_mode = val;
	}
	public void RollingToggle(bool val)
	{
		PlayerControllerAlex.allowRoll = val;
	}
	public void PitchingToggle(bool val)
	{
		PlayerControllerAlex.allowPitch = val;
	}
	public void InvertYAxis(bool val)
	{
		PlayerControllerAlex.invert_yaxis = val;
	}
	public void LASliderUpdate(float value)
	{
		PlayerControllerAlex.translation_acceleration	= value;	
	}
	public void AASliderUpdate(float value)
	{
		PlayerControllerAlex.rotation_acceleration = value;	
	}
	public void AVSliderUpdate(float value)
	{
		PlayerControllerAlex.rotation_velocity = value;	
	}
	public void BoostSliderUpdate(float value)
	{
		PlayerControllerAlex.boost_multiplier = value;	
	}
	public void SetDialogueVolume(float val){
		AudioSource Ellie_Audio = GameObject.Find ("Ellie").GetComponent<AudioSource> ();
		AudioSource Alice_Audio = GameObject.Find ("Alice").GetComponent<AudioSource> ();
		Ellie_Audio.volume = val;
		Alice_Audio.volume = val;
		Debug.Log("Volume val: " + val);

//		GetComponent<AudioSource>().volume = val;
	}
}
