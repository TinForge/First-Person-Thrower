using UnityEngine;
using System.Collections;
using GameScene.Manager;

namespace GameScene
{
	namespace MainPlayer
	{
		public class Polaroid : MonoBehaviour
		{
			[Header ("Scripts")]
			private Player main;

			void Awake ()
			{
				main = GetComponent<Player> ();
				foreach (Transform t in main.noRender.GetComponentsInChildren<Transform>())
					t.gameObject.layer = 10; //set noRender layer
			}

			void Update ()
			{
				if (main.input.GetMouse () && main.Alive ()) {
					transform.Rotate (0, Input.GetAxis ("Mouse X") * MasterManager.input.sensitivity, 0);
					float x = main.camera.transform.rotation.eulerAngles.x - Input.GetAxis ("Mouse Y") * MasterManager.input.sensitivity / 2;
					if (x > 180 && x <= 270)
						x = 270;
					else if (x < 180 && x >= 90)
						x = 90;
					main.camera.transform.localRotation = Quaternion.Euler (x, 0, 0);
				}
			}

			public void Recoil (float magnitude)
			{
				StartCoroutine (RecoilThread (magnitude / 10));
			}

			private IEnumerator RecoilThread (float magnitude)
			{
				for (int i = 0; i < 10; i++) {
					transform.Rotate (0, magnitude * Random.Range (-0.1f, 0.1f), 0);
					main.camera.transform.Rotate (magnitude * Random.Range (-0.1f, 0.1f), 0, 0);
					yield return null;
				}
			}
		}
	}
}