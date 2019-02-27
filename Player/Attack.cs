using UnityEngine;
using System.Collections;
using GameScene.Manager;

namespace GameScene
{
	namespace MainPlayer
	{
		public class Attack : MonoBehaviour//Input Attack and Output Weapon; Attached to player
		{
			[Header ("Scripts")]
			private Player main;

			[Header ("Read Only")]
			[SerializeField] private Transform weapon;
			private Vector3 pos;
			private Quaternion rot;
			private bool onCoolDown;
			private bool melee;

			private int ignoreWeps;

			void Awake ()
			{
				main = GetComponent<Player> ();
				ignoreWeps = ~LayerMask.NameToLayer ("Weapon");
				pos = main.handle.localPosition;
				rot = main.handle.localRotation;
			}

			void OnEnable ()
			{
				GetWeapon ();
			}

			void OnDisable ()
			{
				if (weapon)
					Destroy (weapon.gameObject);
			}

			private void GetWeapon ()
			{
				weapon = WeaponManager.instance.SpawnWeapon ();
				MasterManager.canvas.SetWeaponText (weapon.name);//set name in UI
				ExtensionWeapon.SetInPlace (weapon, main.handle);
				main.handle.localPosition = pos;
				main.handle.localRotation = rot;
			}

			void Update ()
			{
				if (main.input.GetMouse () && main.Alive ()) {
					if (Input.GetButtonDown ("Fire1"))
						Throw ();
					if (Input.GetButtonDown ("Fire2"))
						Melee ();
				}
			}

			public void CoolDown ()
			{
				onCoolDown = false;
				if (!weapon)
					GetWeapon ();
			}

			#region THROW

			private void Throw ()
			{
				if (onCoolDown == false && weapon != null) {
					onCoolDown = true;
					weapon.GetComponent <Weapon> ().Throw (Aim ());
					weapon = null;
					Invoke ("CoolDown", main.stats.coolDownSeconds);
				}
			}

			private Vector3 Aim ()
			{
				Ray ray = main.camera.ViewportPointToRay (new Vector3 (0.5f, 0.5f, 0));
				RaycastHit hit;
				if (Physics.Raycast (ray, out hit, 200/*max distance*/, ignoreWeps))
					return (hit.point);
				else
					return(main.camera.transform.position + main.camera.transform.forward * 5);
			}

			#endregion

			#region MELEE

			private void Melee ()
			{
				if (onCoolDown == false && weapon != null) {
					onCoolDown = true;
					StartCoroutine ("Swing");
					Invoke ("CoolDown", 0.5f);
				}
			}

			void OnCollisionEnter (Collision collision)
			{
				if (melee) {
					if (collision.contacts [0].thisCollider.GetComponentInParent<Weapon> ()) {
						if (collision.transform.root.GetComponent <IDamageable> () != null) {
							collision.transform.root.GetComponent<IDamageable> ().Damage ((-25 - weapon.GetComponent<Weapon> ().wo.damage * MasterManager.enemies.wave));
							melee = false;
						}
					}
				}
			}



			private IEnumerator Swing ()
			{
				float t = 0;
				float duration = 0.5f;
				Vector3 oldPos = main.handle.localPosition;
				Vector3 newPos = main.handle.localPosition + Vector3.forward * 3 + Vector3.right * -1;
				Quaternion oldRot = main.handle.localRotation;
				Quaternion newRot = Quaternion.Euler (new Vector3 (180, -90, -90));
				melee = true;

				while (t < duration) {
					t += Time.deltaTime;
					if (t < duration / 2) {
						main.handle.localPosition = Vector3.Lerp (oldPos, newPos, t / duration);
						main.handle.localRotation = Quaternion.Lerp (oldRot, newRot, t / duration);
						yield return null;
					} else if (t > duration / 2 && t < duration) {
						main.handle.localPosition = Vector3.Lerp (newPos, oldPos, t / duration);
						main.handle.localRotation = Quaternion.Lerp (newRot, oldRot, t / duration);
						yield return null;
					}
				}
				main.handle.localPosition = oldPos;
				main.handle.localRotation = oldRot;
				melee = false;
			}

			#endregion
		}
	}
}
