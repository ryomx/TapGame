using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {

	public Text scoreText; //Score表示用変数
	public Text LifeText; //Life表示用変数
	public Text comboText; //Combo表示用変数
	
	private const string scoreDefaultVal = "Score:";
	private const string lifeDefaultVal = "Life:";
	private const string comboDefaultVal = " Combo";
	private int score = 0;
	private int life = 5;
	private int combo = 0;
	
	public void addScore(){
		score++;
	}
	
	public int getScore(){
		return this.score;
	}
	
	public void subLife(){
		life--;
	}
	
	public int getLife(){
		return this.life;
	}
	
	/**
	 * コンボ数制御用の処理
	 * @param isTapSuccess true:タップ成功、false：タップ失敗
	 */
	public void setCombo(bool isTapSuccess){
		if (isTapSuccess) {
			combo++;
		} else {
			combo = 0;
		}
	}
	
	
	
	public int getCombo(){
		return this.combo;
	}
	
	//	public void setTapSuccess(bool isSuccessTap){
	//		if (isSuccessTap) {
	//			life++;
	//		} else {
	//			life = 0;
	//		}
	//	}
	
	void LateUpdate (){
		scoreText.text = scoreDefaultVal + score;
		LifeText.text = lifeDefaultVal + life;
		comboText.text = combo + comboDefaultVal;
	}
}
