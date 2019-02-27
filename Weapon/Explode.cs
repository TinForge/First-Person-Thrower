using UnityEngine;

public class Explode : MonoBehaviour
{
	[Header ("Components")]
	private WeaponObject wo;

	[Header ("Read Only")]
	private bool touch = false;
	private int musk;

	void Awake ()
	{
		wo = GetComponent<Weapon> ().wo;
		musk = LayerMask.GetMask ("Enemy", "Player");
	}

	public void OnCollisionEnter ()
	{
		if (!touch) {
			touch = true;
			Detonate ();
			SelfDestruct ();
		}
	}

	private void Detonate ()
	{
		Collider[] colliders = Physics.OverlapSphere (transform.position, wo.explodeSize, musk);

		foreach (Collider col in colliders) {
			if (!col.isTrigger && col.transform.tag == "Enemy" || col.transform.tag == "Player") {

				if (col.attachedRigidbody != null)
					col.attachedRigidbody.AddExplosionForce (wo.explodeForce, transform.position, wo.explodeSize, 3, ForceMode.Acceleration);

				if (col.transform.root.GetComponent<IDamageable> () != null) {
					col.transform.root.GetComponent<IDamageable> ().Damage (Hit ());
				}

				if (col.transform.root.GetComponent<IStunnable> () != null) {
					col.transform.root.GetComponent<IStunnable> ().Stunned ();
				}
			}
		}
	}

	private int Hit ()
	{
		int hit = (int)(wo.damage * Random.Range (1 - wo.range, 1 + wo.range));
		return hit * -1;
	}

	private void SelfDestruct ()
	{
		GetComponent<Weapon> ().Disable ();
	}





	//account for grenade -delay
	//account for mine - timer
	//account for semtex - delay
}
