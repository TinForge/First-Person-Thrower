using UnityEngine;
using System.Collections;

namespace GameScene
{
	namespace Manager
	{
		public class MasterManager : MonoBehaviour
		{
			[Header ("References")]
			public GameObject playerPrefab;
			public static GameScene.MainPlayer.Player player;
			[Space]
			public static WeaponManager weapons;
			public static EnemyManager enemies;
			public static PopupManager popup;
			public static DeathManager death;
			public static AudioManager audio;
			public static InputManager input;
			public static CanvasManager canvas;
			public static SettingsManager settings;

			void Awake ()
			{
				if (playerPrefab == null)
					Debug.LogError ("Woops! Require player prefab.");

				weapons = GetComponent<WeaponManager> ();
				enemies = GetComponent<EnemyManager> ();
				popup = GetComponent<PopupManager> ();
				death = GetComponent<DeathManager> ();
				audio = GetComponent<AudioManager> ();
				input = GetComponent<InputManager> ();
				canvas = GetComponentInChildren<CanvasManager> ();
				settings = GetComponentInChildren<SettingsManager> ();
			}

			void Start ()
			{
				ActivatePlayer ();
			}

			private void ActivatePlayer ()
			{
				playerPrefab = Instantiate (playerPrefab);
				player = playerPrefab.GetComponent<GameScene.MainPlayer.Player> ();
				player.enabled = true;
				Debug.Log ("Player Enabled");

				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
				input.SetMouse (true);
				input.SetKeyboard (true);
			}
		}
	}
}