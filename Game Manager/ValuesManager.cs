using UnityEngine;
using System.Collections;

namespace GameScene
{
	namespace Manager
	{
		public class ValuesManager : MonoBehaviour
		{
			private MasterManager main;

			public int level;
			public int enemyLevel;

		

			void Awake ()
			{
				main = GetComponentInParent<MasterManager> ();
			}

		}
	}
}
