using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class MainSceneUI : MonoBehaviour {

	public Text goldText;
	public Image chestOneIMG, chestTwoIMG, chestThreeIMG;
	public Sprite[] chestSprites;
	public Image openChestImage, closedChestImage;
	public Sprite[] openChestSprite;
	public GameObject openChestPanel;
	public Button openChestButton, closeChestPanelButton;
	public GameObject rewardPanel;
	public Text xpReward, goldReward;
	public Image characterImage;
	public Sprite[] characterSprites;
	public Image[] chestBg;
	public Text[] openingText;
	public Color chestBGColor = new Color (84/255, 84/255, 84/225, 1);

	void Start () {
		closeChestPanelButton.onClick.AddListener (() => ExitOpenChestPanel());
		chestOneIMG.sprite = chestSprites[PlayerPrefController.instance.GetChestOne()];
		chestTwoIMG.sprite = chestSprites[PlayerPrefController.instance.GetChestTwo()];
		chestThreeIMG.sprite = chestSprites[PlayerPrefController.instance.GetChestThree()];
		openChestPanel.SetActive (false);
		wizardShop.SetActive (false);
	}
	
	void Update () {
		characterImage.sprite = characterSprites[PlayerPrefController.instance.GetCharacter()];
		goldText.text = PlayerPrefController.instance.GetGold () + "";
		if (PlayerPrefController.instance.GetIsCheckingTime () == 1){
			CheckIfEnergyWaitTimeOver ();
		}
	
	}

	public void OpenChestOne(){
		if (PlayerPrefController.instance.GetChestOne() != 0){
			
			if (PlayerPrefController.instance.GetChestOne() == 1){
				SilverChest ();
				openChestButton.onClick.RemoveAllListeners ();
				openChestButton.onClick.AddListener (() => OpenSilverChest(1));
			} else if (PlayerPrefController.instance.GetChestOne() == 2){
				GoldeChest ();
				openChestButton.onClick.RemoveAllListeners ();
				openChestButton.onClick.AddListener (() => OpenGoldChest(1));
			}
			openChestPanel.SetActive (true);
			chestBg [0].color = Color.blue;
			openingText [0].gameObject.SetActive (true);

		}
	}

	public void OpenChestTwo(){
		if (PlayerPrefController.instance.GetChestTwo () != 0) {
			closedChestImage.sprite = openChestSprite [PlayerPrefController.instance.GetChestTwo ()];
			if (PlayerPrefController.instance.GetChestTwo () == 1) {
				SilverChest ();
				openChestButton.onClick.RemoveAllListeners ();
				openChestButton.onClick.AddListener (() => OpenSilverChest(2));
			} else if (PlayerPrefController.instance.GetChestTwo () == 2) {
				GoldeChest ();
				openChestButton.onClick.RemoveAllListeners ();
				openChestButton.onClick.AddListener (() => OpenGoldChest(2));
			}
			openChestPanel.SetActive (true);
			chestBg [0].color = Color.blue;
			openingText [0].gameObject.SetActive (true);
		} 
	}
	public void OpenChestThree(){
		if (PlayerPrefController.instance.GetChestThree() != 0){
			closedChestImage.sprite = openChestSprite [PlayerPrefController.instance.GetChestThree ()];
			if (PlayerPrefController.instance.GetChestThree() == 1){
				SilverChest ();
				openChestButton.onClick.RemoveAllListeners ();
				openChestButton.onClick.AddListener (() => OpenSilverChest(3));
			} else if (PlayerPrefController.instance.GetChestThree() == 2){
				GoldeChest ();
				openChestButton.onClick.RemoveAllListeners ();
				openChestButton.onClick.AddListener (() => OpenGoldChest(3));
			}
			openChestPanel.SetActive (true);
			chestBg [0].color = Color.blue;
			openingText [0].gameObject.SetActive (true);
		}
	}

	void SilverChest(){
		closeChestPanelButton.gameObject.SetActive (false);
		rewardPanel.SetActive (false);
		openChestButton.gameObject.SetActive (true);
		openChestImage.gameObject.SetActive (false);
		closedChestImage.gameObject.SetActive (true);
		closedChestImage.sprite = openChestSprite [0];
		openChestImage.sprite = openChestSprite [1];
		timer.text = "15:00";

	}

	void GoldeChest(){
		closeChestPanelButton.gameObject.SetActive (false);
		rewardPanel.SetActive (false);
		openChestButton.gameObject.SetActive (true);
		openChestImage.gameObject.SetActive (false);
		closedChestImage.gameObject.SetActive (true);
		closedChestImage.sprite = openChestSprite [2];
		openChestImage.sprite = openChestSprite [3];
		timer.text = "30:00";

	}

	void OpenSilverChest(int chestID){
		SetEnergyRefillTimer (1);
		PlayerPrefController.instance.SetOpeningChestID (1);
		PlayerPrefController.instance.SetOpeningChestID (1);
		openChestButton.gameObject.SetActive (false);
		openChestPanel.SetActive (false);
		PlayerPrefController.instance.SetOpeningChestNumber (chestID);

	}

	void OpenGoldChest(int chestID){
		SetEnergyRefillTimer (1);	
		PlayerPrefController.instance.SetOpeningChestID (1);
		PlayerPrefController.instance.SetOpeningChestID (2);
		openChestButton.gameObject.SetActive (false);
		PlayerPrefController.instance.SetOpeningChestNumber (chestID);
		if (PlayerPrefController.instance.GetOpeningChestNumber() == 1){
			PlayerPrefController.instance.SetChestOne (0);
		} else if (PlayerPrefController.instance.GetOpeningChestNumber() == 2){
			PlayerPrefController.instance.SetChestTwo (0);
		} else if (PlayerPrefController.instance.GetOpeningChestNumber() == 3) {
			PlayerPrefController.instance.SetChestThree (0);
		}
	}

	public void ExitOpenChestPanel(){
		openChestPanel.SetActive (false);
		SceneManager.LoadScene ("MainScene");
	}

	// Time Controller
	public Text timer;
	private static DateTime energyRefillTime;

	private static void SetEnergyRefillTimer(double minutes) {
		energyRefillTime = DateTime.Now.AddMinutes(minutes);
	}


	public void CheckIfEnergyWaitTimeOver() {
		if (DateTime.Now < energyRefillTime ) {

			TimeSpan remaining = energyRefillTime - DateTime.Now;
			var timerCountdownText = string.Format("{0:D2}:{1:D2}", remaining.Minutes, remaining.Seconds);
			timer.text = timerCountdownText;

		} else {
			PlayerPrefController.instance.SetIsCheckingTime (0);
			if (PlayerPrefController.instance.GetOpeningChestNumber() == 1){
				PlayerPrefController.instance.SetChestOne (0);
				openingText [0].gameObject.SetActive (false);
				chestBg [0].color = chestBGColor;
			} else if (PlayerPrefController.instance.GetOpeningChestNumber() == 2){
				PlayerPrefController.instance.SetChestTwo (0);
				openingText [1].gameObject.SetActive (false);
				chestBg [1].color = chestBGColor;
			} else if (PlayerPrefController.instance.GetOpeningChestNumber() == 3) {
				PlayerPrefController.instance.SetChestThree (0);
				openingText [2].gameObject.SetActive (false);
				chestBg [2].color = chestBGColor;
			}
			if (PlayerPrefController.instance.GetOpeningChestID () ==1){
				openChestImage.gameObject.SetActive (true);
				closedChestImage.gameObject.SetActive (false);
				int randomGold = UnityEngine.Random.Range (15, 50);
				goldReward.text = "" + randomGold;
				PlayerPrefController.instance.SetGold (PlayerPrefController.instance.GetGold() + randomGold);
				rewardPanel.SetActive (true);
				openChestPanel.SetActive (true);
				closeChestPanelButton.gameObject.SetActive (true);

			} else if (PlayerPrefController.instance.GetOpeningChestID () == 2) {
				openChestImage.gameObject.SetActive (true);
				closedChestImage.gameObject.SetActive (false);
				int randomGold = UnityEngine.Random.Range (50, 100);
				goldReward.text = "" + randomGold;
				PlayerPrefController.instance.SetGold (PlayerPrefController.instance.GetGold() + randomGold);
				rewardPanel.SetActive (true);
				openChestPanel.SetActive (true); 
				closeChestPanelButton.gameObject.SetActive (true);
			}

		}
	}

//Wizard Shop

	public GameObject[] characterPanel; 
	public GameObject wizardShop;
	int characterIndex;
	public Button leftButton, rightButton;
	public Button buySnowWizard, buyWeatherWizard, buyNatureWizard, buyDarkWizard;
	public RectTransform pricePanel;

	public void OpenWizardShop(){
		CheckWizardAvailability ();
		leftButton.onClick.AddListener (() => LeftButton());
		rightButton.onClick.AddListener(() => RightButton());
		wizardShop.SetActive (true);
		for (int i=0; i< characterPanel.Length; i++){
			characterPanel [i].SetActive (false);
		}
		characterIndex = PlayerPrefController.instance.GetCharacter();
		characterPanel [characterIndex].SetActive(true);
		Debug.Log (characterIndex);
		if (characterIndex == 0) {
			leftButton.gameObject.SetActive (false);
		} else if (characterIndex == (characterPanel.Length -1)){
			rightButton.gameObject.SetActive (false);
		}

		buySnowWizard.onClick.AddListener (() => BuySnowWizard());
		buyWeatherWizard.onClick.AddListener (() => BuyWeatherWizard());
		buyNatureWizard.onClick.AddListener (() => BuyNatureWizard());
		buyDarkWizard.onClick.AddListener (() => BuyDarkWizard());
	}

	void CheckWizardAvailability(){
		if (PlayerPrefController.instance.GetSnowWizard () == 1){
			buySnowWizard.gameObject.SetActive (false);
		}
		if (PlayerPrefController.instance.GetWeatherWizard () == 1){
			buyWeatherWizard.gameObject.SetActive (false);
		}
		if (PlayerPrefController.instance.GetNatureWizard () == 1){
			buyNatureWizard.gameObject.SetActive (false);
		}
		if (PlayerPrefController.instance.GetDarkWizard () == 1){
			buyDarkWizard.gameObject.SetActive (false);
		}
	}

	public void LeftButton(){
		characterPanel [characterIndex].SetActive(false);
		characterPanel [characterIndex-1].SetActive(true);
		characterIndex = characterIndex - 1;
		if (characterIndex == 0) {
			leftButton.gameObject.SetActive (false);
		} 
		if (characterIndex != (characterPanel.Length -1)){
			rightButton.gameObject.SetActive (true);
		}
	}

	public void RightButton(){
		characterPanel [characterIndex].SetActive(false);
		characterPanel [characterIndex+1].SetActive(true);
		characterIndex = characterIndex + 1;

		if (characterIndex == (characterPanel.Length -1)){
			rightButton.gameObject.SetActive (false);
		} 
		if (characterIndex != 0) {
			leftButton.gameObject.SetActive (true);
		}
	}

	void BuySnowWizard(){
		if (PlayerPrefController.instance.GetGold () >= 500) {
			PlayerPrefController.instance.SetGold (PlayerPrefController.instance.GetGold () - 500);
			PlayerPrefController.instance.SetSnowWizard (1);
			buySnowWizard.gameObject.SetActive (false);
		} else {
			StartCoroutine (AnimatePanel());
		}
	}

	void BuyWeatherWizard(){
		if (PlayerPrefController.instance.GetGold() >=500){
			PlayerPrefController.instance.SetGold (PlayerPrefController.instance.GetGold() -500);
			PlayerPrefController.instance.SetWeatherWizard (1);
			buyWeatherWizard.gameObject.SetActive (false);
		} else {
			StartCoroutine (AnimatePanel());
		}
	}
	void BuyNatureWizard(){
		if (PlayerPrefController.instance.GetGold() >=500){
			PlayerPrefController.instance.SetGold (PlayerPrefController.instance.GetGold() -500);
			PlayerPrefController.instance.SetNatureWizard (1);
			buyNatureWizard.gameObject.SetActive (false);
		} else {
			StartCoroutine (AnimatePanel());
		}
	}

	void BuyDarkWizard(){
		if (PlayerPrefController.instance.GetGold() >=500){
			PlayerPrefController.instance.SetGold (PlayerPrefController.instance.GetGold() -500);
			PlayerPrefController.instance.SetDarkWizard (1);
			buyDarkWizard.gameObject.SetActive (false);
		} else {
			StartCoroutine (AnimatePanel());
		}
	}

	public void ChooseFireWizard(){
		PlayerPrefController.instance.SetCharacter (0);
		wizardShop.SetActive (false);
		SceneManager.LoadScene ("MainScene");
	}
	public void ChooseSnowWizard(){
		PlayerPrefController.instance.SetCharacter (1);
		wizardShop.SetActive (false);
		SceneManager.LoadScene ("MainScene");
	}
	public void ChooseWeatherWizard(){
		PlayerPrefController.instance.SetCharacter (2);
		wizardShop.SetActive (false);
		SceneManager.LoadScene ("MainScene");
	}
	public void ChooseNatureWizard(){
		PlayerPrefController.instance.SetCharacter (3);
		wizardShop.SetActive (false);
		SceneManager.LoadScene ("MainScene");
	}
	public void ChooseDarkWizard(){
		PlayerPrefController.instance.SetCharacter (4);
		wizardShop.SetActive (false);
		SceneManager.LoadScene ("MainScene");
	}

	IEnumerator AnimatePanel() {

		float delayTime =1.5f;
		float speed = 3f;
		float animatePercent = 0;
		int dir = 1;

		float endDelayTime = Time.time + 1 / speed + delayTime;

		while (animatePercent >= 0) {
			animatePercent += Time.deltaTime * speed * dir;

			if (animatePercent >= 1) {
				animatePercent = 1;
				if (Time.time > endDelayTime) {
					dir = -1;
				}
			}

			pricePanel.anchoredPosition = Vector2.up * Mathf.Lerp (-700, -300, animatePercent);
			yield return null;
		}

	}

	public void LoadGamePlay(){
		SceneManager.LoadScene ("GamePlay");
	}
}