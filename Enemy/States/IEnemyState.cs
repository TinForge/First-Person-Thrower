using UnityEngine;

public interface IEnemyState
{

	void UpdateState ();

	void OnTriggerEnter (Collider other);

	void ToIdleState ();

	void ToSearchState ();

	void ToAttackState ();

	void ToFleeState ();

}
