using UnityEngine;
using System.Collections;
using UnityChan;

public class GameController : MonoBehaviour {

	public GameObject unityChan;
	public GameObject ballPrefab;

	/** 落下するBallオブジェクトを受け止める側のBaseオブジェクトを格納する配列 */
	public GameObject[] BaseAry;
	/** 落下するボールを生成する高さ */
	private const float createBallHeight = 15.0f;
	/** ユニティちゃんを笑顔にするためのスコア加算数 */
	private const int faceSmileStep = 5;
	/** スコア加算数によりユニティちゃんを笑顔にするためのカウンタ */
	private int faceSmileCounter = 1;
	/**	Ballオブジェクト落下スピード（難易度設定で可変にする） */
	private float fallSpeed = 0.2f;
	/**	Ballオブジェクト生成間隔用タイマー */
	private float timer = 0.0f;
	/**	Ballオブジェクト生成間隔（難易度設定で可変にする） */
	private float createBallInterval = 1.5f;
	/**	ゲームプレイ可能状態（本来はゲームスタートをしてゲームクリア/ゲームオーバーまでの間true） */
	private bool gameState = true;

	private GameObject score;
	private BallController ballController;
	private ScoreController scoreController;
	private FaceUpdate faceUpdate;
	private Animator anim;
	
	// Use this for initialization
	void Start () {
		// BallController取得
		ballController = ballPrefab.GetComponent<BallController> ();

		// ScoreController取得
		score = GameObject.Find( "Score" );
		scoreController = score.GetComponent<ScoreController> ();

		faceUpdate = unityChan.GetComponent<FaceUpdate>();

		anim = unityChan.GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update () {

		if (!gameState) {
			return;
		}

		// 一定時間経過毎に落下するボールのプレハブを生成する
		timer += Time.deltaTime;
		if(createBallInterval < timer){
			createBallPrefab ();
			timer -= createBallInterval;
		}


		// 画面をタップ（左クリック）された場合
		if (Input.GetMouseButtonDown(0)){
			// タップした先のオブジェクトを取得するためのRay
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit raycastHit;

			// Baseオブジェクトをタップした場合
			if (Physics.Raycast(ray, out raycastHit) && 
			    raycastHit.transform.gameObject.tag.Contains("Base")){
	
				// タップしたBaseオブジェクトを取得し、Baseオブジェクトタップ時の処理を呼び出す
				BaseController baseController = raycastHit.transform.gameObject.GetComponent<BaseController>();
				//	有効なタップだった場合
				baseController.baseTapped();
			}
		}

		// Scoreが一定数加算される毎に笑顔にする
		if(scoreController.getScore() == faceSmileCounter * faceSmileStep){
			// 笑顔に変更
			faceUpdate.OnCallChangeFace("smile@sd_hmd");
			faceSmileCounter++;
		}

		// ゲームクリア時の処理
		if(scoreController.getScore() >= ScoreController.gameClearScore) {
			gameClear();
		}

		// ゲームオーバー時の処理
		if(scoreController.getLife() <= 0) {
			gameOver();
		}
	}

	/**
	 *  タップ成功時の処理
	 */
	public void tapSuccess(){
		// スコアを加算
		scoreController.addScore ();
		// コンボ成功を設定
		scoreController.setCombo (true);
	}

	/**
	 *  タップ失敗時の処理
	 */
	public void tapFail(){
		// ライフを減算
		scoreController.subLife ();
		// コンボ失敗を設定
		scoreController.setCombo (false);
	}

	/**
	 *  ゲームオーバー時の処理
	 */
	private void gameOver(){
		gameState = false;
		// 崩れ落ちるアニメーションを表示
		anim.SetTrigger ("Down");
	}

	/**
	 *  ゲームクリア時の処理
	 */
	private void gameClear(){
		gameState = false;
		// ジャンプしてガッツポーズのアニメーションを表示
		anim.SetTrigger ("Salte");
	}

	/**
	 * ボールの落下位置を生成
	 */
	private Vector3 getFallPosition(GameObject targetBase){

		// ボールを落下させるターゲットのBaseオブジェクトのX座標とZ座標、生成位置の高さを設定
		return new Vector3 (targetBase.transform.position.x,
		                   createBallHeight,
		                   targetBase.transform.position.z);
	}

	/**
	 * Ballのプレハブを生成
	 */
	private void createBallPrefab(){

		// ボールを落とす対象のBaseオブジェクトを取得
		GameObject targetBase = BaseAry[Random.Range (0, BaseAry.Length)];

		// ボールの落下スピード設定
		ballController.setFallSpeed (fallSpeed);
		
		// Ballオブジェクトを生成
		Instantiate(
			ballPrefab,
			getFallPosition(targetBase),
			Quaternion.identity
			);
	}
}
