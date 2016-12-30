using UnityEngine;
using System.Collections;
using System;

public class PlayerPrefController : MonoBehaviour {

	public static PlayerPrefController instance;
	private const string gold = "gold";
	private const string chestOne = "chestone";
	private const string chestTwo = "chesttwo";
	private const string chestThree = "chestThree";
	private const string character = "character";
	private const string fireWizard = "fireWizard";
	private const string snowWizard = "snowWizard";
	private const string weatherWizard = "weatherWizard";
	private const string natureWizard = "natureWizard";
	private const string darkWizard = "darkWizard";
	private const string timeRemainingToOpenChest = "timeRemainingToOpenChest";
	private const string openingChestNumber = "openingChestNumber";
	private const string isCheckingTime = "isCheckingTime";
	private const string openingChestID = "chestID";

	void Awake (){
		Screen.SetResolution (480, 720, false);
		//PlayerPrefs.DeleteAll ();
		if (instance != null) {
			Destroy (gameObject);
		} else {
			instance = this;
			DontDestroyOnLoad (gameObject);
		}
		isGameStartedFirstTime();
	}

	void isGameStartedFirstTime(){
		if(!PlayerPrefs.HasKey("GamePlayFirstTime1")){
			PlayerPrefs.SetInt (gold,0);
			PlayerPrefs.SetInt (chestOne,0);
			PlayerPrefs.SetInt (chestTwo,0);
			PlayerPrefs.SetInt (chestThree,0);
			PlayerPrefs.SetInt (character,0);
			PlayerPrefs.SetInt (fireWizard,1);
			PlayerPrefs.SetInt (snowWizard,0);
			PlayerPrefs.SetInt (weatherWizard,0);
			PlayerPrefs.SetInt (natureWizard,0);
			PlayerPrefs.SetInt (darkWizard,0);


			PlayerPrefs.SetInt ("GamePlayFirstTime1", 1);
		}
	}


	public void SetGold(int _gold){
		PlayerPrefs.SetInt (gold, _gold);
	}

	public int GetGold(){
		return PlayerPrefs.GetInt (gold);
	}

	public void SetChestOne(int _chestOne){
		PlayerPrefs.SetInt (chestOne, _chestOne);
	}

	public int GetChestOne(){
		return PlayerPrefs.GetInt (chestOne);
	}

	public void SetChestTwo(int _chestTwo){
		PlayerPrefs.SetInt (chestTwo, _chestTwo);
	}

	public int GetChestTwo(){
		return PlayerPrefs.GetInt (chestTwo);
	}

	public void SetChestThree(int _chestThree){
		PlayerPrefs.SetInt (chestThree, _chestThree);
	}

	public int GetChestThree(){
		return PlayerPrefs.GetInt (chestThree);
	}

	public void SetCharacter(int _character){
		PlayerPrefs.SetInt (character, _character);
	}

	public int GetCharacter(){
		return PlayerPrefs.GetInt (character);
	}

	public void SetFireWizard(int _fireWizard){
		PlayerPrefs.SetInt (fireWizard, _fireWizard);
	}

	public int GetFireWizard(){
		return PlayerPrefs.GetInt (fireWizard);
	}

	public void SetSnowWizard(int _snowWizard){
		PlayerPrefs.SetInt (snowWizard, _snowWizard);
	}

	public int GetSnowWizard(){
		return PlayerPrefs.GetInt (snowWizard);
	}

	public void SetWeatherWizard(int _weatherWizard){
		PlayerPrefs.SetInt (weatherWizard, _weatherWizard);
	}

	public int GetWeatherWizard(){
		return PlayerPrefs.GetInt (weatherWizard);
	}

	public void SetNatureWizard(int _natureWizard){
		PlayerPrefs.SetInt (natureWizard, _natureWizard);
	}

	public int GetNatureWizard(){
		return PlayerPrefs.GetInt (natureWizard);
	}

	public void SetDarkWizard(int _darkWizard){
		PlayerPrefs.SetInt (darkWizard, _darkWizard);
	}

	public int GetDarkWizard(){
		return PlayerPrefs.GetInt (darkWizard);
	}

	public void SetTimeRemainingToOpenChest(int _time){
		PlayerPrefs.SetInt (timeRemainingToOpenChest, _time);
	}

	public int GetTimeRemainingToOpenChest(){
		return PlayerPrefs.GetInt (timeRemainingToOpenChest);
	}

	public void SetOpeningChestNumber(int _openingChestNumber){
		PlayerPrefs.SetInt (openingChestNumber, _openingChestNumber);
	}

	public int GetOpeningChestNumber(){
		return PlayerPrefs.GetInt (openingChestNumber);
	}

	public void SetIsCheckingTime(int _isCheckingTime){
		PlayerPrefs.SetInt (isCheckingTime, _isCheckingTime);
	}

	public int GetIsCheckingTime(){
		return PlayerPrefs.GetInt (isCheckingTime);
	}

	public void SetOpeningChestID(int _openingChestID){
		PlayerPrefs.SetInt (openingChestID, _openingChestID);
	}

	public int GetOpeningChestID(){
		return PlayerPrefs.GetInt (openingChestID);
	}

}
