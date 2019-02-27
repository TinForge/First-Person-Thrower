using UnityEngine;
using System.Collections;
using GameScene.Manager;

namespace GameScene
{
	namespace MainPlayer
	{
		public class Energy : MonoBehaviour
		{
			[Header ("Components")]
			private Player main;

			[Header ("Read Only")]
			private const int maxEnergy = 100;
			private float energy = maxEnergy;

			private float defaultSpeed;

			void Awake ()
			{
				main = GetComponent<Player> ();
				defaultSpeed = main.stats.speed;
			}

			void Update ()
			{
				Recharge ();
				Multiplier ();
			}

			private void Recharge ()
			{
				energy += Time.deltaTime * 25;
				if (energy > 100)
					energy = 100;
				MasterManager.canvas.SetEnergyBar ((int)energy);
			}

			private void Multiplier ()
			{
				if (Input.GetKey (KeyCode.LeftShift) && energy >= 0) {
					main.stats.speed = defaultSpeed * 2f;
					energy -= 1;
				} else {
					main.stats.speed = defaultSpeed;
				}
			}
		}
	}
}
