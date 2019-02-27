using UnityEngine;
using GameScene.Manager;

public class Damage : MonoBehaviour
{
	[Header ("Components")]
	private Weapon main;

	[Header ("Read Only")]
	private bool active = true;

	void Awake ()
	{
		main = GetComponent<Weapon> ();
	}

	private void OnCollisionEnter (Collision other)
	{
		if (other.transform == main.owner)//if it hits ourselves;
			return;

		if (active) {
			active = false;
			if (other.gameObject.GetComponent<IDamageable> () != null) {
				other.gameObject.GetComponent<IDamageable> ().Damage (Hit ());
			}
			SelfDestruct ();
		}
	}

	private int Hit ()
	{
		int hit = (int)(MasterManager.enemies.wave * main.wo.damage * Random.Range (1 - main.wo.range, 1 + main.wo.range));
		return hit * -1;
	}

	void SelfDestruct ()
	{
		Destroy (this);
	}
}
