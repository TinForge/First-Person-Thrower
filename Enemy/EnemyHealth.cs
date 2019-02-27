using UnityEngine;
using System.Collections;
using GameScene.Manager;

public class EnemyHealth : MonoBehaviour,IDamageable
{
	[HideInInspector]private EnemyState main;
	[HideInInspector]private Renderer render;
	[HideInInspector]public bool alive;
	[HideInInspector]private int hp;
	[HideInInspector]private int maxHp;
	[HideInInspector]private Color color;

	void Awake ()
	{
		main = GetComponent<EnemyState> ();
		render = GetComponent<Renderer> ();
		alive = true;
	}

	void Start ()
	{
		color = GetComponent<Renderer> ().material.color;
		maxHp = main.stats.maxHealth;
		hp = maxHp;
	}

	public void Damage (int value)
	{
		if (!alive)
			return;
		
		hp += value;

		if (value > 0)
			OnHealthGain ();
		if (value < 0)
			OnHealthLoss (value);

		if (hp <= 0)
			Dead ();
		else if (hp > maxHp)
			hp = maxHp;



	}

	private void OnHealthGain ()
	{

	}

	private void OnHealthLoss (int value)
	{
		main.anim.SetTrigger ("Jump");
		StartCoroutine ("Flash");
		MasterManager.popup.SpawnDamageText (value * -1, transform.position);
	}

	private IEnumerator Flash ()
	{
		float t = 0;
		float rate = 5;
		SkinnedMeshRenderer smr = GetComponentInChildren<SkinnedMeshRenderer> ();
		while (t < 1) {
			t += Time.deltaTime * rate;
			if (t < 0.5f) {
				smr.material.color = Color.Lerp (color, Color.white, t * 2);
				yield return null;
			} else if (t > 0.5f) {
				smr.material.color = Color.Lerp (Color.white, color, (t - 0.5f) * 2);
				yield return null;
			}
		}
		main.hitmarkerAudio.Play ();
		render.material.color = color;
	}

	private void Dead ()
	{
		if (!alive)
			return;		

		alive = false;
		hp = 0;

		Rigidbody rb = GetComponent<Rigidbody> ();
		rb.constraints = RigidbodyConstraints.None;
		rb.mass = 50;
		rb.drag = 0.1f;


		main.anim.SetTrigger ("Dead");
		main.attack.Remove ();
		GetComponent<UnityEngine.AI.NavMeshAgent> ().enabled = false;
		StartCoroutine (FadeToBlack ());
		main.deadAudio.Play ();
		GetComponent<ParticleSystem> ().Play ();
		MasterManager.enemies.RemoveEnemy (gameObject);
		Invoke ("Goodbye", 6);
	}

	private IEnumerator FadeToBlack ()
	{
		float t = 0;
		float rate = 0.5f;
		yield return new WaitForSeconds (0.6f);
		while (t < 2) {
			t += Time.deltaTime * rate;
			GetComponentInChildren<SkinnedMeshRenderer> ().material.color = Color.Lerp (color, Color.black, t / 2);
			yield return null;
		}
	}

	private void Goodbye ()
	{
		Destroy (gameObject);
	}

}
