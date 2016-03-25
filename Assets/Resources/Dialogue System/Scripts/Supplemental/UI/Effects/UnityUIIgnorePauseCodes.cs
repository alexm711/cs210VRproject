using UnityEngine;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace PixelCrushers.DialogueSystem
{
	
	/// <summary>
	/// This component strips RPGMaker-style pause codes from a text component when enabled.
	/// </summary>
	[AddComponentMenu("Dialogue System/UI/Unity UI/Effects/Unity UI Ignore Pause Codes")]
    [DisallowMultipleComponent]
	public class UnityUIIgnorePauseCodes : MonoBehaviour
    {

        private UnityEngine.UI.Text control;

        public void Awake()
        {
			control = GetComponent<UnityEngine.UI.Text>();
		}

		public void OnEnable()
        {
            if (control == null) return;
            control.text = UnityUITypewriterEffect.StripRPGMakerCodes(control.text);
		}
		
	}
	
}
