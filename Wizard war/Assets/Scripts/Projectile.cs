using UnityEngine;
using System.Collections;

public class Projectile : LivingEntity {

	public LayerMask collisionMask;
	float speed = 2;
	float damage = 1;
	public ParticleSystem weaponParticalDeathEffect, burnFire, sleepPartical;

	float lifetime = 3;

	public string originalPerson;

	public bool isBurn, isSleep, isBloodSucking;

	public bool isShadowBall;

	LivingEntity originalPersonEntity;
	void Start() {
		Destroy (gameObject, lifetime);
		if (GameObject.FindGameObjectWithTag (originalPerson).GetComponent<LivingEntity>() != null ){
			originalPersonEntity = GameObject.FindGameObjectWithTag (originalPerson).GetComponent<LivingEntity>();
		}

		Collider[] initialCollisions = Physics.OverlapSphere (transform.position, .1f, collisionMask);
		if (initialCollisions.Length > 0) {
			OnHitObject(initialCollisions[0]);
		}
	}

	public void SetSpeed(float newSpeed) {
		speed = newSpeed;
	}

	public void SetDamage(float newDamage) {
		damage = newDamage;
	}
	
	void Update () {
		float moveDistance = speed * Time.deltaTime;
		CheckCollisions (moveDistance);
		transform.Translate (Vector3.forward * moveDistance);
	}


	void CheckCollisions(float moveDistance) {
		Ray ray = new Ray (transform.position, transform.forward);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, moveDistance, collisionMask, QueryTriggerInteraction.Collide)) {
			OnHitObject(hit.collider);
		}
	}


	public void InstantiateParticalEffect (){
		Instantiate (weaponParticalDeathEffect, transform.position, Quaternion.identity);	
	}

	void OnHitObject(Collider c) {
		LivingEntity damageableObject = c.GetComponent<LivingEntity> ();
		GunController opponentGunController = c.GetComponent<GunController> ();
		Instantiate (weaponParticalDeathEffect, transform.position, Quaternion.identity);	
		if (damageableObject != null) {
			if (Random.Range (0f, 10f) <= 2f) {
				if (isBurn) {
					int randomIndex = (int)Random.Range (4, 8.5f);
					damageableObject.Burn (randomIndex, .25f);
					if (damageableObject.tag == "Enemy" || damageableObject.tag == "Player") {
						ParticleSystem spawenedBurnFire = Instantiate (burnFire, damageableObject.transform.position, damageableObject.transform.rotation) as ParticleSystem;
						spawenedBurnFire.GetComponent<BurnFire> ().SetLifeTime (randomIndex);
						spawenedBurnFire.transform.parent = damageableObject.transform;
					}
				} else if (isBloodSucking) {
					if (damageableObject.tag == "Enemy" || damageableObject.tag == "Player") {
						originalPersonEntity.AddHealth (damage * 0.25f);
					}

				} else if (isSleep) {
					if (damageableObject.tag == "Enemy" || damageableObject.tag == "Player") {
						float randomIndex = Random.Range (2, 5f);
						opponentGunController.Sleep (randomIndex);
						ParticleSystem spawenedSleepPowder = Instantiate (sleepPartical, damageableObject.transform.position, damageableObject.transform.rotation) as ParticleSystem;
						spawenedSleepPowder.GetComponent<BurnFire> ().SetLifeTime (randomIndex);
						spawenedSleepPowder.transform.parent = damageableObject.transform;
					}
				}
			}
			damageableObject.TakeDamage (damage);
			Projectile otherProjectile = c.GetComponent<Projectile> ();
			if (otherProjectile != null) {
				otherProjectile.InstantiateParticalEffect ();
			}
			if (!isShadowBall){
				GameObject.Destroy (gameObject);
			}

		}
	}
}
