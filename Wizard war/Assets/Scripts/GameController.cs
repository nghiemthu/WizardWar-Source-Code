using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	int questionIndex;
	int x, y, z;
	string equation;
	bool answer;
	[HideInInspector]
	public string question;
	public ParticleSystem rain, snow;
	public GameObject weatherHolder;
	public bool isRaining, isSnowing;

	GunController gunController;
	LivingEntity playerEntity;

	public Player[] players;
	public Player[] enemies;

	public Transform playerHolder;
	public Transform enemyHolder;
	public int moveID;

	public event System.Action OnButtonTwo;

	public Button buttonOne, buttonTwo;
	public GameObject chosenSign;

	public bool isDone;
	void Awake(){
		
	}
	void Start(){
		isDone = false;
		Instantiate (players[PlayerPrefController.instance.GetCharacter()], playerHolder.transform.position, playerHolder.transform.rotation);
		int randomIndex = (int)Random.Range (0, enemies.Length - 0.1f);
		Instantiate (enemies[randomIndex], enemyHolder.transform.position, enemyHolder.transform.rotation);
		GenerateQuestion ();
		if (GameObject.FindGameObjectWithTag ("Player") != null){
			gunController = GameObject.FindGameObjectWithTag ("Player").GetComponent<GunController> ();
			playerEntity = GameObject.FindGameObjectWithTag ("Player").GetComponent<LivingEntity> ();
		}
		InitializeSkills ();
		buttonOne.onClick.AddListener (() => ButtonOne());
		buttonTwo.onClick.AddListener (() => ButtonTwo());
		moveID = 1;
		chosenSign.transform.position = buttonOne.transform.position;
	}

	public void InitializeSkills(){
		if (PlayerPrefController.instance.GetCharacter() == 0){
			OnButtonTwo += FireBreath;
		} else if (PlayerPrefController.instance.GetCharacter() == 1){
			OnButtonTwo += Snow;
		} else if (PlayerPrefController.instance.GetCharacter() == 2){
			OnButtonTwo += Rain;
		} else if (PlayerPrefController.instance.GetCharacter() == 3){
			OnButtonTwo += Heal;
		} else if (PlayerPrefController.instance.GetCharacter() == 4){
			OnButtonTwo += ShadowBall;
		}
	}
		
	public void ButtonOne(){
		moveID = 1;
		chosenSign.transform.position = buttonOne.transform.position; 
	}

	public void ButtonTwo(){
		moveID = 2;
		chosenSign.transform.position = buttonTwo.transform.position; 
	}

	public void FireBreath(){
		gunController.FireBreath ();
	}

	public void Heal(){
		playerEntity.Heal (2, 2);
	}

	public void Rain(){
		if  (!isRaining){
			Instantiate (rain, weatherHolder.transform.position, Quaternion.identity);
			StartCoroutine (Raining());
		}
	}
	public void Snow(){
		if (!isSnowing ){
			Instantiate (snow, weatherHolder.transform.position, Quaternion.identity);
			StartCoroutine (Raining());
		}
	}

	public void ShadowBall(){
		gunController.ShootShadowBall ();
		playerEntity.TakeDamage (.5f);
	}

	IEnumerator Raining(){
		isRaining = true;
		yield return new WaitForSeconds (20f);
		isRaining = false;
	}

	IEnumerator Snowing(){
		isSnowing = true;
		yield return new WaitForSeconds (20f);
		isSnowing = false;
	}

	public void ChooseTrue(){
		if (answer == true){
			if (moveID == 1){
				gunController.Shoot ();
			} else if (moveID == 2) {
				OnButtonTwo ();
			}
			AudioManager.instance.PlaySound ("True", transform.position);
			Debug.Log ("Correct");
		} else if (answer == false){
			Debug.Log ("Wrong");
			AudioManager.instance.PlaySound ("False", transform.position);
			playerEntity.TakeDamage (2);
		}
		GenerateQuestion ();
	}

	public void ChooseFalse(){
		if (answer == false){
			AudioManager.instance.PlaySound ("True", transform.position);
			if (moveID == 1){
				gunController.Shoot ();
			} else if (moveID == 2) {
				OnButtonTwo ();
			}
			Debug.Log ("Correct");
		} else if (answer == true){
			AudioManager.instance.PlaySound ("False", transform.position);
			Debug.Log ("Wrong");
			playerEntity.TakeDamage (2);
		}
		GenerateQuestion ();
	}

	void GenerateQuestion(){
		x = (int)Random.Range (1, 9);
		y = (int)Random.Range (1, 9);
		int randomIndex = (int)Random.Range (0, 1.99f);
		if (randomIndex == 0) {
			equation = "-";
			int randomAnswer = (int)Random.Range (0, 1.99f);
			if (randomAnswer == 0) {
				answer = false;
				if (Random.Range (0f, 0.99f) > 0.5f) {
					z = x - y + (int)Random.Range (1, 3.99f);
				} else {
					z = x - y - (int)Random.Range (1, 3.99f);
				}
			} else {
				answer = true;
				z = x - y;
			}
		} else {
			equation = "+";
			int randomAnswer = (int)Random.Range (0, 1.99f);
			if (randomAnswer == 0) {
				answer = false;
				if (Random.Range (0f, 0.99f) > 0.5f) {
					z = x + y + (int)Random.Range (1, 3.99f);
				} else {
					z = x + y - (int)Random.Range (1, 3.99f);
				}
			} else {
				answer = true;
				z = x + y;
			}
		}
		question = x +" "+ equation +" "+ y + " = " + z;
	}
}