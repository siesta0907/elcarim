using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

	public Sprite[] sprites;

	SpriteRenderer renderer;
	int state = 0;

	// Use this for initialization
	void Start () {
		renderer = GetComponent<SpriteRenderer> ();
	}

	/*
	void OnMouseDown() {
		state++;
		updateSprite ();
	}
	//*/

	public void setState(int n) {
		state = n;
		updateSprite ();
	}

	void updateSprite() {
		if (state >= sprites.Length)
			state = 0;
		
		renderer.sprite = sprites [state];
	}
}
