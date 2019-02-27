using UnityEngine;
using System.Collections;

namespace GameScene
{
	namespace Manager
	{
		public class DeathManager : MonoBehaviour
		{

			public Transform deathCam;

			[Header ("Read Only")]
			private int respawnTime;
			private Transform player;

			public delegate void OnPlayerDelegate ();

			public event OnPlayerDelegate onPlayerDelegate;

			void Awake ()
			{
			}

			public void OnDead (Transform target, int time)
			{
				player = target;
				respawnTime = time;

				if (onPlayerDelegate != null)
					onPlayerDelegate ();

				StartCoroutine (DeathCam ());
			}

			private IEnumerator DeathCam ()
			{
				float t = 0;
				float i = 0;

				player.gameObject.SetActive (false);
				deathCam.gameObject.SetActive (true);
				MasterManager.canvas.FadePanel (true, respawnTime - 1);

				deathCam.position = player.GetComponent<GameScene.MainPlayer.Player> ().camera.transform.position;
				Quaternion oldRot = player.GetComponent<GameScene.MainPlayer.Player> ().camera.transform.rotation;
				Quaternion newRot = Quaternion.Euler (new Vector3 (90, oldRot.eulerAngles.y, 0));

				while (t <= respawnTime) {
					t += Time.deltaTime;
					deathCam.position += Vector3.up * t * Time.deltaTime;
					if (t <= 1) {
						i = Mathf.Sin (t * Mathf.PI / 2);
						deathCam.rotation = Quaternion.Lerp (oldRot, newRot, i);
					} else
						deathCam.Rotate (Vector3.forward * (t - 1) * Time.deltaTime);
			
					yield return null;
				}
				Respawn ();
			}

			private void Respawn ()
			{
				MasterManager.enemies.Reset (0);
				MasterManager.canvas.FadePanel (false, 1);
				deathCam.gameObject.SetActive (false);
				player.gameObject.SetActive (true);
				player.position = Vector3.zero + Vector3.up * 5;

			}


		}
	}
}