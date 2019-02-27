using UnityEngine;
using System.Collections;
using GameScene.Manager;

public class Effects : MonoBehaviour
{
	[Header ("Sound")]
	[SerializeField]private AudioSource startSound;
	[SerializeField]private AudioSource impactSound;
	[SerializeField]private AudioSource endSound;
	[Header ("Particles")]
	[SerializeField]private ParticleSystem startParticle;
	[SerializeField]private ParticleSystem impactParticle;
	[SerializeField]private ParticleSystem endParticle;

	/*	#region GetComponents

	void Awake ()
	{
		startSound = FindAS ("Starting Sound Source").GetComponent<AudioSource> ();
		impactSound = FindAS ("Impact Sound Source").GetComponent<AudioSource> ();
		endSound = FindAS ("Ending Sound Source").GetComponent<AudioSource> ();

		startParticle = FindPS ("Starting Particle System").GetComponent<ParticleSystem> ();
		impactParticle = FindPS ("Impact Particle System").GetComponent<ParticleSystem> ();
		endParticle = FindPS ("Ending Particle System").GetComponent<ParticleSystem> ();
	}

	private AudioSource FindAS (string tag)
	{
		foreach (AudioSource aud in transform.GetComponentsInChildren<AudioSource> ())
			if (aud.gameObject.CompareTag (tag)) {
				aud.pitch = Random.Range (0.75f, 1.25f);
				return aud;
			}
		Debug.LogError ("No audio source found");
		return null;
	}

	private ParticleSystem FindPS (string tag)
	{
		foreach (ParticleSystem ps in transform.GetComponentsInChildren<ParticleSystem> ())
			if (ps.gameObject.CompareTag (tag))
				return ps;
		Debug.LogError ("No particle system found");
		return null;
	}

	#endregion*/

	void Awake ()
	{
		GameObject container = new GameObject ();
		container.transform.SetParent (transform);
		container.transform.localPosition = Vector3.zero;
		container.name = "Effects";

		WeaponObject wo = GetComponent<Weapon> ().wo;

		startSound = container.AddComponent<AudioSource> ();
		startSound.clip = wo.startSound;
		startSound.outputAudioMixerGroup = MasterManager.audio.effectsGroup;
		startSound.spatialBlend = 1;
		startSound.priority = 64;
		startSound.playOnAwake = false;

		impactSound = container.AddComponent<AudioSource> ();
		impactSound.clip = wo.impactSound;
		impactSound.outputAudioMixerGroup = MasterManager.audio.effectsGroup;
		impactSound.spatialBlend = 1;
		impactSound.priority = 128;
		impactSound.playOnAwake = false;
		impactSound.pitch = Random.Range (wo.pitchOffset - wo.pitchRange, wo.pitchOffset + wo.pitchRange);

		endSound = container.AddComponent<AudioSource> ();
		endSound.clip = wo.endSound;
		endSound.outputAudioMixerGroup = MasterManager.audio.effectsGroup;
		endSound.spatialBlend = 1;
		endSound.priority = 192;
		endSound.playOnAwake = false;

		Transform temp;

		temp = (Transform)Instantiate (wo.startParticle, container.transform);
		temp.localPosition = Vector3.zero;
		startParticle = temp.GetComponent<ParticleSystem> ();
		temp = (Transform)Instantiate (wo.impactParticle, container.transform);
		temp.localPosition = Vector3.zero;
		impactParticle = temp.GetComponent<ParticleSystem> ();
		temp = (Transform)Instantiate (wo.endParticle, container.transform);
		temp.localPosition = Vector3.zero;
		endParticle = temp.GetComponent<ParticleSystem> ();
	}

	public void PlayStart ()
	{
		startSound.Play ();
		//	if (!endParticle.loop)//?
		startParticle.Play ();
	}

	public void PlayImpact ()
	{
		impactSound.Play ();
		impactParticle.Play ();
	}

	public void PlayEnd ()
	{
		endSound.Play ();
		endParticle.Play ();
	}

	public float EndDuration ()
	{
		return endParticle.main.duration;
	}
}
