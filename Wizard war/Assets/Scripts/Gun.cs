using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {


	public Transform[] projectileSpawn;
	public Projectile projectile;
	public Projectile shadowBall;
	public Projectile hugeProjectile;
	public float msBetweenShots = 100;
	public float muzzleVelocity = 20;
	public float reloadTime = .3f;
	float muzzleDamage =1;
	float powerPerShot = 1f;

	[Header("Effects")]
	public AudioClip shootAudio;

	float nextShotTime;

	void Start() {
	}

	public void SetMuzzleVelocity(float velocity){
		muzzleVelocity = velocity;
	}

	public void SetMuzzleDamage(float damage){
		muzzleDamage = damage;
	}

	void Update(){
	}

	public void Shoot() {

		for (int i =0; i < projectileSpawn.Length; i ++) {
			nextShotTime = Time.time + msBetweenShots / 1000;
			Projectile newProjectile = Instantiate (projectile, projectileSpawn[i].position, projectileSpawn[i].rotation) as Projectile;
			newProjectile.SetSpeed (muzzleVelocity);
			newProjectile.SetDamage (muzzleDamage);
		}
	//		AudioManager.instance.PlaySound (shootAudio, transform.position);
	}

	public void ShootShadowBall() {

		for (int i =0; i < projectileSpawn.Length; i ++) {
			nextShotTime = Time.time + msBetweenShots / 1000;
			Projectile newProjectile = Instantiate (shadowBall, projectileSpawn[i].position, projectileSpawn[i].rotation) as Projectile;
			newProjectile.SetSpeed (muzzleVelocity);
			newProjectile.SetDamage (muzzleDamage);
			newProjectile.isShadowBall = true;
		}
		//		AudioManager.instance.PlaySound (shootAudio, transform.position);
	}
		
		
}
