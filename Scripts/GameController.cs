using UnityEngine;
using System.Collections;
using UnityChan;

public class GameController : MonoBehaviour {

	public GameObject unityChan;
	public GameObject ballPrefab;
	GameObject score;

	/** 落下するBallオブジェクトを受け止める側のBaseオブジェクトを格納する配列 */
	public GameObject[] BaseAry;
	/** 落下するボールを生成する高さ */
	private const float createBallHeight = 15.0f;
	/** ユニティちゃんを笑顔にするためのスコア加算数 */
	private const int faceSmileStep = 5;
	/** スコア加算数によりユニティちゃんを笑顔にするためのカウンタ */
	private int faceSmileCounter = 1;
	private float fallSpeed = 0.2f;
	private BallController ballController;
	private ScoreController scoreController;
	private FaceUpdate faceUpdate;
	private float timer = 0.0f;
	private float createBallInterval = 1.5f;
	
	// Use this for initialization
	void Start () {
		// BallController取得
		ballController = ballPrefab.GetComponent<BallController> ();
		score = GameObject.Find( "Score" );
		// ScoreController取得
		scoreController = score.GetComponent<ScoreController> ();

		faceUpdate = unityChan.GetComponent<FaceUpdate>();
	

	}

	// Update is called once per frame
	void Update () {
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
				baseController.isValidTapped();
//				if(baseController.isValidTapped()){
//					// スコアを加算
//					scoreController.addScore();
////					// 笑顔に変更
////					faceUpdate.OnCallChangeFace("smile@sd_hmd");
//				} else {
////					// 悲しい顔に変更
////					faceUpdate.OnCallChangeFace("sad@sd_hmd");
//				}
			}
		}

		// Scoreが一定数加算される毎に笑顔にする
		if(scoreController.getScore() == faceSmileCounter * faceSmileStep){
			// 笑顔に変更
			faceUpdate.OnCallChangeFace("smile@sd_hmd");
			faceSmileCounter++;
		}
	}

	public void tapSuccess(){
		// スコアを加算
		scoreController.addScore ();
		// コンボ成功を設定
		scoreController.setCombo (true);
	}

	public void tapFail(){
		// ライフを減算
		scoreController.subLife ();
		// コンボ失敗を設定
		scoreController.setCombo (false);
	}

	/**
	 * ボールの落下位置を生成します
	 */
	private Vector3 getFallPosition(GameObject targetBase){

		// ボールを落下させるターゲットのBaseオブジェクトのX座標とZ座標、生成位置の高さを設定
		return new Vector3 (targetBase.transform.position.x,
		                   createBallHeight,
		                   targetBase.transform.position.z);
	}

	/**
	 * Ballのプレハブを生成します
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
