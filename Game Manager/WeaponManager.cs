using UnityEngine;
using System.Collections;

namespace GameScene
{
	namespace Manager
	{
		public class WeaponManager : MonoBehaviour
		{
			public static WeaponManager instance;

			void Awake ()
			{
				instance = this;
			}

			public Transform[] weapon;
			//List
			//0	Axe
			//1	Knife
			//2	Sword
			//3	Shuriken
			//4	Balloon
			//5	Grenade
			//6
			//7

			public bool testing;
			public int id;

			public Transform SpawnWeapon (/*int index, Transform pH*/)
			{
				int index;
				if (testing)
					index = UnityEngine.Random.Range (id, id);//Testing
				else
					index = UnityEngine.Random.Range (0, weapon.Length);
				
				Transform wep = (Transform)Instantiate (weapon [index]/*, pH.position, pH.rotation*/);
				return wep;
			}
			//Incorporate tiers of systems

		}
	}
}