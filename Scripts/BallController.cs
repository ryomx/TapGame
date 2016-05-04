using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour {

//	public GameObject effectPrefab;
//	public Vector3 effectRotation;
	public float fallSpeed = 1.0f;
//	p	rivate bool isTapSuccess = false;

	void FixedUpdate ()
	{
		Vector3 moveDirection = transform.position;
		// FixedUpdate()内で移動計算をする際、Time.deltaTimeの乗算は不要
		//moveDirection.y -= fallSpeed * Time.deltaTime;
		moveDirection.y -= fallSpeed;

		transform.position = moveDirection;
	}

	public void setFallSpeed(float speed){
		fallSpeed = speed;
	}

//	public void setTapSuccess(){
//		isTapSuccess = true;
//	}
//
//	public bool getTapSuccess(){
//		return this.isTapSuccess;
//	}
}
