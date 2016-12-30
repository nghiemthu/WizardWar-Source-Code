using UnityEngine;
using System.Collections;

public class LivingEntity : MonoBehaviour, IDamageable {

	public float startingHealth;
	public float health;
	protected bool dead;
	public ParticleSystem healPartical;

	public bool healRain;
	public bool roughIce;

	public event System.Action OnDeath;

	GameController gameController;
	GunController gunController;

	protected virtual void Start() {
		health = startingHealth;
		gameController = FindObjectOfType<GameController> ();
		gunController = GetComponent<GunController> ();
	}

	void Update(){
		if (gameController.isRaining == true && health < startingHealth && healRain){
			health += Time.deltaTime * .1f;
		}
		if (gameController.isSnowing == true && roughIce) {
			gunController.allGuns [0].SetMuzzleDamage (1.5f);
		} else {
			gunController.allGuns [0].SetMuzzleDamage (1f);
		};
	}

	public virtual void TakeDamage(float damage) {
		StartCoroutine (TakeDamageCoroutine(0.5f, damage));
	}

	public void AddHealth(float health){
		StartCoroutine(HealCoroutine(0.5f, health));
		Debug.Log ("added");
	}

	public void Heal(float timeToLast, float health){
		StartCoroutine (HealCoroutine(timeToLast, health));
	}

	public void Burn(float timeToLast, float damage){
		StartCoroutine (BurnCoroutine(timeToLast, damage));
	}

	IEnumerator HealCoroutine(float timeToLast, float health){
		Instantiate (healPartical, transform.position, Quaternion.identity);
		float endTime = Time.time + timeToLast;
		float targetHealth = this.health + health ;
		while (Time.time < endTime && this.health < targetHealth && this.health <startingHealth){
			yield return null;
			this.health += Time.deltaTime*2;
		}
	}

	IEnumerator TakeDamageCoroutine(float timeToLast, float damage){
		float endTime = Time.time + timeToLast;
		float targetHealth = this.health - damage ;
		while (Time.time < endTime && this.health > targetHealth){
			yield return null;
			this.health -= Time.deltaTime*4;
			if (health <= 0 && !dead) {
				Die();
			}
		}
	}

	IEnumerator BurnCoroutine(float timeToLast, float damage){
		float endTime = Time.time + timeToLast;
		while (Time.time < endTime){
			yield return new WaitForSeconds (1f);
			TakeDamage (damage);
		}
	}

	[ContextMenu("Self Destruct")]
	public virtual void Die() {
		dead = true;
		if (OnDeath != null) {
			OnDeath();
		}

		GameObject.Destroy (gameObject);
	}
}
