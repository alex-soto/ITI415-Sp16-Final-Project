using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour {

	private int _score;
	private bool gameOver;
	private bool restartGame = false;
	public int score {
		get {
			return _score;
		}
	}
	private Text uiText;

	void Start(){
		_score = 0;
		gameOver = false;
		restartGame = false;
		uiText = GameObject.Find ("UIText").GetComponent<Text>();
	}

	void FixedUpdate(){
		if (!gameOver) {
			uiText.text = "Score: " + score;
		} else {
			GameObject player = GameObject.Find("Player");
			if (player != null){
				player.SetActive(false);
			}
			if (Input.GetAxis("Fire1") > 0){
				restartGame = true;
			}
		}

		if (restartGame) {
			Application.LoadLevel(Application.loadedLevelName);
		}
	}

	public void IncreaseScore(){
		_score++;
	}

	public void GameOver(bool gameWon){
		if (gameWon) {
			gameOver = true;
			uiText.text = "You won! Score: " + score;
		} else {
			gameOver = true;
			uiText.text = "You lost. Score: " + score;
		}
	}

}
