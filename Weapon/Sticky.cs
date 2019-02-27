using UnityEngine;

public class Sticky : MonoBehaviour
{
	[Header ("Read Only")]
	private bool touch = false;

	public void OnCollisionEnter (Collision collision)
	{
		if (!touch) {
			touch = true;
			SetRigidbody (collision);
			transform.position = collision.contacts [0].point + collision.contacts [0].normal * -collision.contacts [0].separation;
			SelfDestruct ();
		}
	}

	private void SetRigidbody (Collision collision)
	{
		Rigidbody rb = GetComponent<Rigidbody> ();
		if (collision.rigidbody) {
			
			transform.SetParent (collision.transform);
			Destroy (rb);
		} else
			rb.constraints = RigidbodyConstraints.FreezeAll;
	}

	private void SelfDestruct ()
	{
		Destroy (this);
	}

}
