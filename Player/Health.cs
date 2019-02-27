using UnityEngine;
using System.Collections;
using GameScene.Manager;

namespace GameScene
{
	namespace MainPlayer
	{
		public class Health : MonoBehaviour,IDamageable
		{
			[Header ("Scripts")]
			private Player main;

			[Header ("Read Only")]
			private float hitpoints;

			public float Hitpoints {
				get{ return hitpoints; }
				set{ hitpoints = Mathf.Clamp (value, 0, main.stats.maxHealth); }
			}

			void Awake ()
			{
				main = GetComponent<Player> ();
			}

			void OnEnable ()
			{
				SetAlive (true);
			}

			private void SetAlive (bool state)
			{
				Hitpoints = state == true ? main.stats.maxHealth : 0;
				//transform.SetVisibility (state);
				MasterManager.canvas.ToggleDeadText (!state);
				MasterManager.canvas.SetHealthBar (Conversion ());

				if (!state) {
					MasterManager.death.OnDead (main.transform, main.stats.respawnTime);
					main.Invoke ("Respawn", main.stats.respawnTime);
					this.enabled = false;
				}
			}

			private int Conversion ()
			{
				int i = Mathf.CeilToInt (Hitpoints / main.stats.maxHealth * 100);
				return i;
			}

			public void Damage (int value)
			{				
				Hitpoints += value;

				if (Hitpoints == 0) {
					SetAlive (false);
					return;
				}
				
				if (value > 0)
					OnHealthGain (value);
				else if (value < 0)
					OnHealthLoss (value);

				MasterManager.canvas.SetHealthBar (Conversion ());
			}

			private void OnHealthGain (int value)
			{
			}

			private void OnHealthLoss (int value)
			{
				main.hitSound.Play ();
				main.polaroid.Recoil (value);
				MasterManager.canvas.HurtPanel ();
			}

			void Update ()
			{
				Recharge ();
			}

			private void Recharge ()
			{
				Hitpoints += Time.deltaTime * 2;
				MasterManager.canvas.SetHealthBar (Conversion ());
			}
		}
	}
}
