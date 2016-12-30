using UnityEngine;
using System.Collections;

public class GunController : MonoBehaviour {

	public Transform weaponHold;
	public Gun[] allGuns;
	Gun equippedGun;
	public int currentGunIndex;
	bool isSleeping;
	public ParticleSystem fireBreath;
	public GameObject breathHolder;

	void Start() {
		currentGunIndex = 0;
	}

	public void FireBreath(){
		ParticleSystem spawnedFireBreath = Instantiate (fireBreath, breathHolder.transform.position, breathHolder.transform.rotation) as ParticleSystem;
		if (gameObject.tag == "Player"){
			GameObject.FindGameObjectWithTag ("Enemy").GetComponent<LivingEntity> ().Burn (3,.5f);
		} else if (gameObject.tag == "Enemy"){
			GameObject.FindGameObjectWithTag ("Player").GetComponent<LivingEntity> ().Burn (3,.5f);
		}
	}

	public void EquipGun(Gun gunToEquip) {
		if (equippedGun != null) {
			Destroy(equippedGun.gameObject);
		}
		equippedGun = Instantiate (gunToEquip, weaponHold.position,weaponHold.rotation) as Gun;
		equippedGun.transform.parent = weaponHold;
	}

	public void EquipGun(int weaponIndex) {
		EquipGun (allGuns [weaponIndex]);
	}

	public void Sleep(float timeToSleep){
		StartCoroutine (SleepCoroutine(timeToSleep));
	}

	IEnumerator SleepCoroutine(float timeToSleep){
		isSleeping = true;
		yield return new WaitForSeconds (timeToSleep);
		isSleeping = false;
	}

	public void Shoot() {
		if (!isSleeping) {
			equippedGun.Shoot ();
			AudioManager.instance.PlaySound ("Shoot", transform.position);
		} else {
			Debug.Log ("You cannot shoot while sleeping!");
		}
	}

	public void ShootShadowBall() {
		if (!isSleeping) {
			equippedGun.ShootShadowBall();
			AudioManager.instance.PlaySound ("Shoot", transform.position);
		} else {
			Debug.Log ("You cannot shoot while sleeping!");
		}
	}


}