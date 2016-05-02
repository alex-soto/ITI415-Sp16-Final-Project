using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour {

	private int _score;
	public int score {
		get {
			return _score;
		}
	}
	private Text uiText;

	void Start(){
		_score = 0;
		uiText = GameObject.Find ("UIText").GetComponent<Text>();
	}

	void FixedUpdate(){
		uiText.text = "Score: " + score;
	}

	public void IncreaseScore(){
		_score++;
	}

}
