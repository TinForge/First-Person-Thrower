using UnityEngine;
using System.Collections;
using GameScene.Manager;

public class EnemyAttack : MonoBehaviour
{
	[HideInInspector]private EnemyState main;
	[HideInInspector]private Transform handle;
	[HideInInspector]public Transform weapon;
	[HideInInspector]private bool onCoolDown;
	[HideInInspector]private float coolDownSeconds;

	void Awake ()
	{
		main = GetComponent<EnemyState> ();
		coolDownSeconds = main.stats.attackSpeed;
		handle = transform.GetChild (0);
	}

	void Start ()
	{
		GetWeapon ();
	}

	private void GetWeapon ()
	{
		if (!main.health.alive)
			return;
		weapon = WeaponManager.instance.SpawnWeapon ();
		ExtensionWeapon.SetInPlace (weapon, handle);
	}

	public void Throw ()
	{
		if (onCoolDown == false && weapon != null) {
			onCoolDown = true;
			if (main.target.tag != "Player")
				return;
			weapon.GetComponent <Weapon> ().Throw (main.target.position + Random.insideUnitSphere * 1);
			weapon = null;
			Invoke ("CoolDown", coolDownSeconds);
		}
	}

	public void CoolDown ()
	{
		onCoolDown = false;
		if (!weapon)
			GetWeapon ();
	}

	public void Remove ()
	{
		if (weapon) {
			Destroy (weapon.gameObject);
		}
	}

	void OnDisable ()
	{
		Remove ();
	}

}
