using UnityEngine;
using System.Collections;
using GameScene.Manager;

namespace GameScene
{
	namespace MainPlayer
	{
		public class Player : MonoBehaviour //Main script/ref for player controller
		{
			[Header ("Scripts")]			
			[HideInInspector]public Health health;
			[HideInInspector]public Polaroid polaroid;
			[HideInInspector]public Movement movement;
			[HideInInspector]public Energy energy;
			[HideInInspector]public Attack attack;
			[HideInInspector]public InputManager input;
			[HideInInspector]public PlayerStats stats = new PlayerStats ();

			[Header ("Components")]
			[HideInInspector]public new Camera camera;
			[HideInInspector]public Rigidbody rb;
			[HideInInspector]public AudioSource hitSound;

			[Header ("Transform")]
			public Transform handle;
			public Transform noRender;

			void Awake ()
			{
				transform.SetLocalShader ();
				input = MasterManager.input;
				camera = GetComponentInChildren<Camera> ();
				rb = GetComponent<Rigidbody> ();
				hitSound = GetComponent<AudioSource> ();
			}

			void Start ()
			{
				health = gameObject.AddComponent<Health> ();
				polaroid = gameObject.AddComponent<Polaroid> ();
				movement = gameObject.AddComponent<Movement> ();
				energy = gameObject.AddComponent<Energy> ();
				attack = gameObject.AddComponent<Attack> ();
			}

			public bool Alive ()
			{
				if (health.enabled)
					return true;
				else
					return false;
			}

			public void Respawn ()
			{
				health.enabled = true;
			}
		}
	}
}
