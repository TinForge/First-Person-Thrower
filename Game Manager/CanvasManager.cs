using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace GameScene
{
	namespace Manager
	{
		public class CanvasManager : MonoBehaviour//Singelton for all canvas managed functions
		{

			public Text deadText;
			public Text weaponText;
			public Text waveText;
			public Text enemiesText;

			public Image experienceBar;
			public Image healthBar;
			public Image energyBar;
			public Image hurtPanel;
			public Image fadePanel;

			void Awake ()
			{

			}

			public void ToggleDeadText (bool toggle)
			{
				deadText.enabled = toggle;
			}

			public void SetWeaponText (string wep)
			{
				wep = (wep.Split ('(') [0]);
				weaponText.text = wep;
			}

			public void SetWaveText (int count)
			{
				waveText.text = "Wave " + count;
			}

			public void SetEnemiesText (int count)
			{
				enemiesText.text = "Enemies Left: " + count;
			}

			public void SetExperienceBar (int experience)
			{
				experienceBar.rectTransform.sizeDelta = new Vector2 (experience, experienceBar.rectTransform.sizeDelta.y);
			}

			public void SetHealthBar (int health)
			{
				healthBar.rectTransform.sizeDelta = new Vector2 (health, healthBar.rectTransform.sizeDelta.y);
			}

			public void SetEnergyBar (int energy)
			{
				energyBar.rectTransform.sizeDelta = new Vector2 (energy, energyBar.rectTransform.sizeDelta.y);
			}

			public void HurtPanel ()
			{
				StartCoroutine (FlashHurt ());
			}

			private IEnumerator FlashHurt ()
			{
				Color color = hurtPanel.color;
				float t = 0;

				color.a = 0.3f;
				hurtPanel.color = color;
				while (t < 0.1f) {
					t += Time.deltaTime;
					color.a = 0.1f - t;
					hurtPanel.color = color;
					yield return null;
				}
				color.a = 0;
				hurtPanel.color = color;
			}

			public void FadePanel (bool toOpaque, float duration)
			{
				fadePanel.canvasRenderer.SetAlpha (1f);
				if (!toOpaque) {//to transparent
					fadePanel.color = Color.black;
					fadePanel.CrossFadeAlpha (0f, duration, false);
				} else {//to opaque
					Color gay = Color.black;
					gay.a = 0.01f;
					fadePanel.color = gay;
					fadePanel.CrossFadeAlpha (255, duration, false);
				}
			}
		}
	}
}