using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent (typeof (GunController))]
public class Player : LivingEntity {

	GunController gunController;
	LivingEntity enemyEntity;
	GameController controller;

	protected override void Start () {
		base.Start ();
		gunController.EquipGun (0);
	
	}

	void Awake() {
		controller = FindObjectOfType<GameController> ();
		gunController = GetComponent<GunController> ();
		health = startingHealth;
	}


		
	public override void Die ()
	{
		base.Die ();
		if (this.tag == "Player" && controller.isDone == false){
			GamePlayUI.instance.GameOver ();
			controller.isDone = true;
			FindObjectOfType<EnemyController> ().playerDeath = true;
			AudioManager.instance.PlaySound ("Lose", transform.position);
		} else if (this.tag == "Enemy" && controller.isDone == false){
			GamePlayUI.instance.PlayeWin ();
			controller.isDone = true;
			AudioManager.instance.PlaySound ("Win", transform.position);
		}

	}
		
}
