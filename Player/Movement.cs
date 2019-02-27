using UnityEngine;
using System.Collections;

namespace GameScene
{
	namespace MainPlayer
	{
		public class Movement : MonoBehaviour //Clever player movement that interpolates across network nicely
		{
			[Header ("Components")]
			private Player main;
			[Header ("Read Only")]
			private bool grounded;

			private float x;
			private bool y;
			private float z;

			void Awake ()
			{
				main = GetComponent<Player> ();
			}

			void Update ()
			{
				Vectors ();
			}

			void FixedUpdate ()
			{
				if (main.input.GetKeyboard () && main.Alive ()) {
					Move ();
					if (y)
						Jump ();
				}
			}

			private void Vectors ()
			{
				x = Input.GetAxis ("Horizontal");
				y = Input.GetKeyDown (KeyCode.Space);
				z = Input.GetAxis ("Vertical");
			}

			private void Move ()
			{
				Vector3 targetVelocity = new Vector3 (x, 0, z);
				targetVelocity = transform.TransformDirection (targetVelocity);
				targetVelocity *= main.stats.speed;

				Vector3 velocityChange = (targetVelocity - main.rb.velocity);
				velocityChange.x = Mathf.Clamp (velocityChange.x, -main.stats.maxVelocityChange, main.stats.maxVelocityChange);
				velocityChange.z = Mathf.Clamp (velocityChange.z, -main.stats.maxVelocityChange, main.stats.maxVelocityChange);
				velocityChange.y = 0;

				main.rb.AddForce (velocityChange, ForceMode.VelocityChange);
			}

			private void Jump ()
			{
				if (Physics.Raycast (transform.position, -transform.up, 1f))
				//	if (Physics.SphereCast (ray, 0.25f, 1.25f))
					grounded = true;
				if (grounded) {
					Vector3 targetVelocity = Vector3.up * (Mathf.Sqrt (main.stats.jump * -Physics.gravity.y * 2));
					Vector3 velocityChange = (targetVelocity - main.rb.velocity);
					main.rb.AddForce (velocityChange, ForceMode.VelocityChange);
					grounded = false;
				}
			}

			void OnCollisionEnter ()
			{
				Ray ray = new Ray (transform.position, -transform.up * 1f);
				if (Physics.SphereCast (ray, 0.5f, 1.25f))
					grounded = true;
			}
		}
	}
}
