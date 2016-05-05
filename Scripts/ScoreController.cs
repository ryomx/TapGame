using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {
	/** Score表示用変数 */
	public Text scoreText;
	/** Life表示用変数 */
	public Text LifeText; 
	/** Combo表示用変数 */
	public Text comboText;
	/** ゲームクリア判定値 */
	public const int gameClearScore = 10;
	/** Score表示用文字列 */
	private const string scoreDefaultVal = "Score:";
	/** Life表示用文字列 */
	private const string lifeDefaultVal = "Life:";
	/** Combo表示用文字列 */
	private const string comboDefaultVal = " Combo";
	private int score = 0;
	private int life = 5;
	private int combo = 0;

	/**
	 * スコアを加算
	 */
	public void addScore(){
		if(score < gameClearScore) {

		}
		score++;
	}

	/**
	 * スコアを取得
	 */
	public int getScore(){
		return this.score;
	}

	/**
	 * ライフを減算
	 */
	public void subLife(){
		if(life > 0) {
			life--;
		} 
	}

	/**
	 * ライフを取得
	 */
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

	/**
	 * コンボを取得
	 */
	public int getCombo(){
		return this.combo;
	}

	/**
	 * スコアなどの表示を更新
	 */
	void LateUpdate (){
		scoreText.text = scoreDefaultVal + score;
		LifeText.text = lifeDefaultVal + life;
		comboText.text = combo + comboDefaultVal;
	}
}
