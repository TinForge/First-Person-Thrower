using UnityEngine;

public class EnemyMovement
{
	private readonly EnemyState main;

	public EnemyMovement (EnemyState enemyAI)
	{
		main = enemyAI;
	}

	public void Resume ()
	{
		if (main.dumb || !main.nav.isOnNavMesh)
			return;
		main.nav.Resume ();
	}

	public void MoveTo (Vector3 pos, int offset)
	{
		if (main.dumb || !main.nav.isOnNavMesh)
			return;
		main.nav.SetDestination (pos + new Vector3 (Random.Range (-offset, offset), 0, Random.Range (-offset, offset)));
		main.anim.SetBool ("Moving", true);
		main.nav.Resume ();
	}

	public void Stop ()
	{
		if (main.dumb || !main.nav.isOnNavMesh)
			return;
		main.nav.Stop ();
	}

	public bool Stopped (int remaining)
	{
		if (main.dumb || !main.nav.isOnNavMesh)
			return false;
		
		if (main.nav.remainingDistance < remaining && !main.nav.pathPending) {
			main.anim.SetBool ("Moving", false);
			return true;
		} else
			return false;
	}
		
}
