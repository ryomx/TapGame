using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour {
	
	public float fallSpeed = 1.0f;

	void FixedUpdate ()
	{
		// Ball落下制御
		Vector3 moveDirection = transform.position;
		moveDirection.y -= fallSpeed;

		transform.position = moveDirection;
	}

	/**
	 * Ballオブジェクトの落下スピードを設定
	 */
	public void setFallSpeed(float speed){
		fallSpeed = speed;
	}
}
