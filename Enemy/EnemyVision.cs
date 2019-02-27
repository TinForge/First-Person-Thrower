using UnityEngine;

public class EnemyVision
{
	private readonly EnemyState main;

	public EnemyVision (EnemyState enemyAI)
	{
		main = enemyAI;
	}

	int layermask = LayerMask.GetMask ("Player");

	public bool LineOfSight (Transform target) //is target in line of sight?
	{
		Debug.DrawRay (main.transform.position, target.position - main.transform.position, Color.green);
		RaycastHit hit;
		if (Physics.Raycast (main.transform.position, target.position - main.transform.position, out hit, layermask) && hit.collider.transform.CompareTag ("Player"))
			return true;
		else
			return false;
	}

	public Transform Target (int length, bool weapon) //is target a player or weapon?
	{
		Debug.DrawRay (main.transform.position, main.transform.forward * length);
		RaycastHit hit;
		if (Physics.Raycast (main.transform.position, main.transform.forward, out hit, length)) {
			if (hit.collider.CompareTag ("Player"))
				return hit.transform;
/*			else if (weapon && hit.collider.CompareTag ("Weapon"))
				return hit.transform;*/
			else
				return null;
		} else
			return null;
	}

	public void LookAt (Transform target) //look at target - y axis only
	{
		Vector3 targetPos = new Vector3 (target.position.x, main.transform.position.y, target.position.z);
		main.transform.LookAt (targetPos);
	}
}
