using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GameScene
{
	namespace Manager
	{
		public class EnemyManager : MonoBehaviour
		{
			[Header ("Enemy List")]
			public GameObject enemyPrefab;
			public List<GameObject> enemies = new List<GameObject> ();
			public Transform[] spawners;

			public bool active = true;
			public int wave;

			void Start ()
			{
				Reset (0);
			}

			public void Reset (int value)
			{
				wave = value;
				MasterManager.canvas.SetWaveText (wave);
				MasterManager.canvas.SetEnemiesText (value);
				foreach (GameObject go in enemies)
					Destroy (go);
				enemies.Clear ();

				CancelInvoke ();

				if (value == 0)
					InvokeRepeating ("Wave", 1, 2);
				else
					InvokeRepeating ("Wave", 0, 2);
			}

			void Update ()
			{
				if (Input.GetKeyDown (KeyCode.Equals))
					Reset (wave);
			}

			private void Wave ()
			{
				if (!active)
					return;
				
				if (enemies.Count == 0) {
					wave++;
					MasterManager.canvas.SetWaveText (wave);
					StartCoroutine (AddEnemy ());
				}
			}

			public IEnumerator AddEnemy ()
			{
				for (int i = 0; i < wave * 1; i++) {
					int random = Random.Range (0, spawners.Length);
					GameObject enemy = (GameObject)Instantiate (enemyPrefab, spawners [random].position, Quaternion.identity);
					enemies.Add (enemy);
					MasterManager.canvas.SetEnemiesText (enemies.Count);
					yield return new WaitForSeconds (0.05f);
				}
			}

			public void RemoveEnemy (GameObject enemy)
			{
				enemies.Remove (enemy);
				MasterManager.canvas.SetEnemiesText (enemies.Count);
			}
		}
	}
}