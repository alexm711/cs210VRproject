using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public GameManager GM;
	public MusicManager MM;

    private Slider _musicSlider;
    private Slider _AVSlider;
    private Slider _AASlider;
    private Slider _LASlider;
    private Slider _LVSlider;
    private Slider _difficultySlider;
    private bool showing_controls = false;

    // Use this for initialization
    void Start ()
	{
        //--------------------------------------------------------------------------
        // Game Settings Related Code


        //--------------------------------------------------------------------------
        // Music Settings Related Code
        _musicSlider = GameObject.Find("Music_Slider").GetComponent<Slider>();
        _AVSlider = GameObject.Find("Angular_Velocity_Slider").GetComponent<Slider>();
        _AASlider = GameObject.Find("Angular_Acceleration_Slider").GetComponent<Slider>();
        _LASlider = GameObject.Find("Linear_Acceleration_Slider").GetComponent<Slider>();
        _LVSlider = GameObject.Find("Linear_Velocity_Slider").GetComponent<Slider>();
        _difficultySlider = GameObject.Find("Difficulty_Slider").GetComponent<Slider>();
        _LASlider.interactable = false;
        _AASlider.interactable = false;
        _difficultySlider.interactable = false;
        GameObject.Find("Controller_Panel").GetComponent<CanvasGroup>().alpha = 0f;


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
        MM.ChangeSong();
    }
    public void ShowControls() {
        GameObject.Find("Controller_Panel").GetComponent<CanvasGroup>().alpha = showing_controls ? 1f : 0f;
        showing_controls=  showing_controls ? false : true;

    }

    public void MusicToggle(bool val)
	{
		_musicSlider.interactable = val;
		MM.SetVolume(val ? _musicSlider.value : 0f);
	}
    public void FixedLinearSpeedToggle(bool val)
    {
        PlayerControllerAlex.fixed_linear_speed = val;
        _LASlider.interactable = !val;
        _LVSlider.interactable = val;

    }
    public void FixedAngularSpeedToggle(bool val)
    {
        PlayerControllerAlex.fixed_angular_speed = val;
        _AASlider.interactable = !val;
        _AVSlider.interactable = val;
    }
    public void RollingToggle(bool val)
	{
		PlayerControllerAlex.allowRoll = val;
	}
    public void PitchingToggle(bool val)
    {
        PlayerControllerAlex.allowPitch = val;
    }
    public void SafetyCollisons(bool val)
    {
        PlayerControllerAlex.safety_collisions = val;
    }
    public void InvertYAxis(bool val)
	{
		PlayerControllerAlex.invert_yaxis = val;
	}
    public void LASliderUpdate(float value)
    {
        PlayerControllerAlex.translation_acceleration = value;
    }
    public void LVSliderUpdate(float value)
    {
        PlayerControllerAlex.translation_velocity = value;
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
    public void DifficultySliderUpdate(float value)
    {
        PlayerControllerAlex.translation_acceleration = value;
    }
}
