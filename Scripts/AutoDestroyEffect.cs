using UnityEngine;
using System.Collections;

public class AutoDestroyEffect : MonoBehaviour {

	ParticleSystem particle;

	// Use this for initialization
	void Start () {
		particle = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
		// パーティクルの生成が終了したらGameObjectを削除
		if (!particle.isPlaying) {
			Destroy(gameObject);
		}
	}
}
