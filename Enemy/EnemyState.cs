using UnityEngine;
using System.Collections;
using GameScene.Manager;

public class EnemyState : MonoBehaviour
{
	[Header ("Scripts")]
	[HideInInspector]public EnemyStats stats = new EnemyStats ();
	[HideInInspector]public EnemyMovement move;
	[HideInInspector]public EnemyVision vision;
	[HideInInspector]public EnemyAttack attack;
	[HideInInspector]public EnemyHealth health;
	[HideInInspector]public EnemyStun stun;

	[Header ("States")]
	[HideInInspector]public IEnemyState currentState;
	[Space]
	[HideInInspector]public IdleState idleState;
	[HideInInspector]public SearchState searchState;
	[HideInInspector]public AttackState attackState;
	[HideInInspector]public FleeState fleeState;

	[Header ("Components")]
	[HideInInspector]public UnityEngine.AI.NavMeshAgent nav;
	[HideInInspector]public Animator anim;
	[HideInInspector]public Rigidbody rb;
	public SphereCollider detectionCollider;
	public AudioSource hitmarkerAudio;
	public AudioSource deadAudio;

	[Header ("Read Only")]
	[HideInInspector]public bool dumb;
	[HideInInspector]public Vector3 destination;
	[HideInInspector]public Transform target;


	void Awake ()
	{
		idleState = new IdleState (this);
		searchState = new SearchState (this);
		attackState = new AttackState (this);
		fleeState = new FleeState (this);

		move = new EnemyMovement (this);
		vision = new EnemyVision (this);
		attack = gameObject.AddComponent <EnemyAttack> ();
		health = gameObject.AddComponent <EnemyHealth> ();
		stun = gameObject.AddComponent<EnemyStun> ();

		nav = GetComponent<UnityEngine.AI.NavMeshAgent> ();
		anim = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody> ();

		target = MasterManager.player.transform;
		stats.Initialize (MasterManager.enemies.wave, Random.Range (-0.5f, 0.5f));
		nav.speed = stats.speed;
		transform.localScale = new Vector3 (stats.scaleModifier, stats.scaleModifier, stats.scaleModifier);

		//-----------DIRTY
		SkinnedMeshRenderer smr = GetComponentInChildren<SkinnedMeshRenderer> ();
		smr.material.color = new Color (Random.Range (0, 255) / 255f, Random.Range (0, 255) / 255f, Random.Range (0, 255) / 255f);
		GetComponent<ParticleSystemRenderer> ().material = smr.material;
		//-----------DIRTY
	}

	void OnEnable ()
	{
		currentState = searchState;

		StartCoroutine ("StateMachine");
		MasterManager.death.onPlayerDelegate += Revert;
	}

	void OnDisable ()
	{
		StopCoroutine ("StateMachine");
		MasterManager.death.onPlayerDelegate -= Revert;
	}

	private IEnumerator StateMachine ()
	{
		while (health.alive) {
			if (!dumb)
				currentState.UpdateState ();
			yield return null;
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if (health.alive)
			currentState.OnTriggerEnter (other);
	}

	private void Revert ()
	{
		currentState = searchState;
	}

}
