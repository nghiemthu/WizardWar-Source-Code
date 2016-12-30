using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GamePlayUI : MonoBehaviour {

	public Text question;
	GameController controller;
	public RectTransform redBar;
	public RectTransform blueBar;

	public Text skillOneText, skillTwoText, skillThreeText;

	public static GamePlayUI instance;

	LivingEntity playerEntity, enemyEntity;

	public GameObject gameOverPanel;
	public Text GameOverText;
	public Button gameOverButton;
	public Image gameOverImage;
	public Sprite[] chestSprites;

	void Start () {
		gameOverPanel.SetActive (false);
		if (instance == null){
			instance = this;
		}
		if (GameObject.FindGameObjectWithTag ("Player")!= null){
			playerEntity = GameObject.FindGameObjectWithTag ("Player").GetComponent<LivingEntity> ();
		}
		if (GameObject.FindGameObjectWithTag ("Enemy") != null){
			enemyEntity = GameObject.FindGameObjectWithTag ("Enemy").GetComponent<LivingEntity> ();
		}
		controller = FindObjectOfType<GameController> ();
		InitializeVariable ();
	}

	void InitializeVariable(){
		if (PlayerPrefController.instance.GetCharacter() == 0){
			skillOneText.text = "Fire Ball";
			skillTwoText.text = "Fire Breath";
			skillThreeText.text = "Burn";
		} else if (PlayerPrefController.instance.GetCharacter() == 1){
			skillOneText.text = "Ice Ball";
			skillTwoText.text = "Snowfall";
			skillThreeText.text = "Rough Ice";
		} else if (PlayerPrefController.instance.GetCharacter() == 2){
			skillOneText.text = "Electro Ball";
			skillTwoText.text = "Rain";
			skillThreeText.text = "Rain Heal";
		} else if (PlayerPrefController.instance.GetCharacter() == 3){
			skillOneText.text = "Grass Ball";
			skillTwoText.text = "Heal";
			skillThreeText.text = "Sleep";
		} else if (PlayerPrefController.instance.GetCharacter() == 4){
			skillOneText.text = "Bat Ball";
			skillTwoText.text = "Shadow Ball";
			skillThreeText.text = "BloodSucking";
		}
	}
	
	void Update () {
		question.text = controller.question;
		float redPercent = 0;
		if (playerEntity != null) {
			redPercent = playerEntity.health / playerEntity.startingHealth;
		}
		redBar.localScale = new Vector3 (redPercent, 1, 1);

		float bluePercent = 0;
		if (enemyEntity != null) {
			bluePercent = enemyEntity.health / enemyEntity.startingHealth;
		}
		blueBar.localScale = new Vector3 (bluePercent, 1, 1);
	}

	public void GameOver(){
		gameOverPanel.SetActive (true);
		GameOverText.text = "Game Over";
		gameOverImage.gameObject.SetActive (false);
	}

	public void PlayeWin(){
		gameOverPanel.SetActive (true);
		GameOverText.text = "You Won";
		gameOverImage.gameObject.SetActive (true);
		if (PlayerPrefController.instance.GetChestOne() == 0) {
			if (Random.Range (0f, 10f) < 2.5f) {
				gameOverImage.sprite = chestSprites [1];
				PlayerPrefController.instance.SetChestOne (2);
			} else {
				gameOverImage.sprite = chestSprites [0];
				PlayerPrefController.instance.SetChestOne (1);
			}
		} else if (PlayerPrefController.instance.GetChestTwo() == 0) {
			if (Random.Range (0f, 10f) < 2.5f) {
				gameOverImage.sprite = chestSprites [1];
				PlayerPrefController.instance.SetChestTwo (2);
			} else {
				gameOverImage.sprite = chestSprites [0];
				PlayerPrefController.instance.SetChestTwo (1);
			}
		} else if (PlayerPrefController.instance.GetChestThree() == 0) {
			if (Random.Range (0f, 10f) < 2.5f) {
				gameOverImage.sprite = chestSprites [1];
				PlayerPrefController.instance.SetChestThree (2);
			} else {
				gameOverImage.sprite = chestSprites [0];
				PlayerPrefController.instance.SetChestThree (1);
			}
		} else {
			gameOverImage.sprite = chestSprites [3];
		}
	}

	public void BackToMainScene(){
		SceneManager.LoadScene("MainScene");
	}
}
