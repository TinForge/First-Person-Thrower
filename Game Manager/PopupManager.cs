using UnityEngine;
using System.Collections;

namespace GameScene
{
	namespace Manager
	{
		public class PopupManager : MonoBehaviour
		{
			public GameObject damageText;

			void Awake ()
			{
			}

			public void SpawnDamageText (int damage, Vector3 pos)
			{
				GameObject copy = (GameObject)Instantiate (damageText, pos + Vector3.up, Quaternion.identity);
				copy.GetComponent<DamageText> ().Initialize (damage, MasterManager.player.gameObject);
			}
		}
	}
}
