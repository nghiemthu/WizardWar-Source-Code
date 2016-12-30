using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour {

	public AudioClip mainTheme;

	string sceneName;

	void Start() {
		OnLevelWasLoaded (0);
	}


	void OnLevelWasLoaded(int sceneIndex) {
		string newSceneName = SceneManager.GetActiveScene ().name;
		if (newSceneName != sceneName) {
			sceneName = newSceneName;
			Invoke ("PlayMusic", .2f);
		}
	}

	void PlayMusic() {
		AudioClip clipToPlay = null;

				clipToPlay = mainTheme;
		

		if (clipToPlay != null) {
			AudioManager.instance.PlayMusic (clipToPlay, 0);
			Invoke ("PlayMusic", clipToPlay.length);
		}

	}

}
