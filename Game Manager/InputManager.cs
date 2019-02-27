using UnityEngine;
using System.Collections;

namespace GameScene
{
	namespace Manager
	{
		public class InputManager : MonoBehaviour
		{
			private bool mouse;
			private bool keyboard;
			[HideInInspector] public float sensitivity;

			void OnApplicationFocus (bool focus)
			{
				if (GetMouse ()) {
					Cursor.lockState = CursorLockMode.Locked;
					Cursor.visible = false;
				}
			}

			void Start ()
			{

				sensitivity = PlayerPrefs.GetFloat ("sensitivity");
			}

			public void SetMouse (bool toggle)
			{
				mouse = toggle;
				sensitivity = PlayerPrefs.GetFloat ("sensitivity");
			}

			public void SetKeyboard (bool toggle)
			{
				keyboard = toggle;
			}

			public bool GetMouse ()
			{
				return mouse;
			}

			public bool GetKeyboard ()
			{
				return keyboard;
			}
		}
	}
}