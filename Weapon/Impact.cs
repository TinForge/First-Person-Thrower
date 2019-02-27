using UnityEngine;

public class Impact : MonoBehaviour
{
	[Header ("Components")]
	private Weapon main;
	private WeaponObject wo;
	private Rigidbody rb;

	[Header ("Read Only")]
	private bool touch = false;

	void Start ()
	{
		main = GetComponent<Weapon> ();
		wo = main.wo;
		rb = GetComponent<Rigidbody> ();
	}

	public void OnCollisionEnter ()
	{
		if (!touch) {
			main.effects.PlayImpact ();
			SetRigidbody ();
			SetCollision ();
			SelfDestruct ();
		}
	}

	#region INNERWORKS

	private void SetRigidbody ()
	{
		rb.drag = wo.drag;
		rb.angularDrag = wo.angularDrag;
	}

	private void SetCollision ()
	{
		if (main.owner != null) {
			foreach (Collider col in transform.GetComponentsInChildren<Collider> ())
				Physics.IgnoreCollision (col, main.owner.GetComponentInChildren<Collider> (), false); //replace with change layer?
		}
	}

	private void SelfDestruct ()
	{
		main.Invoke ("Disable", Random.Range (5, 10));
		Destroy (this);
	}

	#endregion
}
