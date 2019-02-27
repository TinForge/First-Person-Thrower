using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameScene
{
	namespace Manager
	{
		public class SettingsManager : MonoBehaviour
		{
			[SerializeField] private Transform menu;
			[SerializeField] private Transform settings;

			private bool toggle;

			public delegate void OnInputDelegate (bool toggle);

			public event OnInputDelegate onInputDelegate;

			void Awake ()
			{
			}

			void Update ()
			{
				if (Input.GetKeyDown (KeyCode.Escape))
					Back ();
			}

			public void Back ()
			{
				if (settings.gameObject.activeSelf == true) {
					settings.GetComponent<Menu.SettingsUI> ().Return ();
					return;
				}

				toggle = !toggle;
				MasterManager.input.SetMouse (!toggle);
				MasterManager.input.SetKeyboard (!toggle);
				menu.gameObject.SetActive (toggle);
				settings.gameObject.SetActive (false);
				Cursor.visible = toggle;
				if (toggle)
					Cursor.lockState = CursorLockMode.None;
				else
					Cursor.lockState = CursorLockMode.Locked;
			}

			public void Reload ()
			{
				SceneManager.LoadScene (1);
			}

			public void GameOptions ()
			{
				Debug.Log ("To be implemented");
			}

			public void Settings ()
			{
				settings.gameObject.SetActive (true);
			}

			public void ReturnMainMenu ()
			{
				SceneManager.LoadScene (0);
			}

		}
	}
}