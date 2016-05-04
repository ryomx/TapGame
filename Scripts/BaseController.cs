using UnityEngine;
using System.Collections;

public class BaseController : MonoBehaviour {

	public GameObject effectPrefab;
	private Vector3 effectRotation = new Vector3(0.0f,0.0f,0.0f);
	// 落下してくるボールと衝突時の色
	private Color collisionColer = new Color(1f, 0.4f, 1f, 0.7f);
	private GameController gameController;
	private bool isTapSuccess = false;

	// Use this for initialization
	void Start () {
//		Controller = gameController.GetComponent<GameController>();
		gameController = GetComponent<GameController>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// 衝突時の処理
	void OnTriggerEnter (Collider other) {
		
		if ("Ball".Equals(other.gameObject.tag)) {

			// プレートの色変更メソッドを呼び出す
			StartCoroutine (changeColer());

			// タップ成功フラグをfalseに初期化
			isTapSuccess = false;

			// ボールオブジェクトを削除
			StartCoroutine (destroyBall(other.gameObject));
			//Destroy(other.gameObject, 0.2f);

		}
	}
	
	/** 
	 * BaseオブジェクトをタップしたときにBallオブジェクトとの衝突判定と衝突時の処理
	*/
	public void isValidTapped(){

//		// タップ成功判定用フラグ
//		bool isTapSuccess = false;

		// Baseオブジェクトのレンダラーを取得
		Renderer renderer = transform.gameObject.GetComponent<Renderer>();
		// Baseオブジェクトの色が変わっている場合（ボールと衝突直後）
		if (collisionColer.Equals(renderer.material.color)){

			// タップ成功時のフラグを立てる
			isTapSuccess = true;

			// タップ成功時のエフェクトを生成
			Instantiate(
				effectPrefab,
				transform.position,
				Quaternion.Euler(effectRotation)
				);

			
//			// BallControllerに持たせているタップ成功フラグを成功にする
//			BallController ballController = other.gameObject.GetComponent<BallController>();
//			ballController.setTapSuccess();
			
//			// タップが成功の場合
//			if (ballController.getTapSuccess()) {
//				gameController.tapSuccess();
//				
//				// タップが失敗の場合
//			} else {
//				gameController.tapFail();
//			}

		}
//		return isTapSuccess;
	}

	/**
	 * コルーチンを利用してBaseオブジェクトの色を変更
	 */
	IEnumerator changeColer(){

		// Baseオブジェクトの色を変更
		Renderer renderer = transform.gameObject.GetComponent<Renderer>();
		Color startColor = renderer.material.color;
		//renderer.material.color = Color.red;
		renderer.material.color = collisionColer;

		// 一定時間待って色をもとに戻す
		yield return new WaitForSeconds(0.2f);
		renderer.material.color = startColor;
	}

	/**
	 * コルーチンを利用してBallオブジェクトを削除
	 * 削除するタイミングのタップ成功フラグの状態によってGameContorllerの処理を呼び出す
	 */
	IEnumerator destroyBall(GameObject deleteObject){
		
		// 一定時間待ってballオブジェクトを削除する
		yield return new WaitForSeconds(0.2f);

		// Ballオブジェクトを削除するまでにタップ成功した場合
		if (isTapSuccess) {
			gameController.tapSuccess();

			// 上記以外
		} else {
			gameController.tapFail();
		}

		Destroy (deleteObject);
	}
}
