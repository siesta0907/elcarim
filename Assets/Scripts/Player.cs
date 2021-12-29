using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public int px, py;
	public Animator anim;

	int nx, ny;
	float moveCount;

	void Start() {
		init_Player (px, py);
	}

	// Update is called once per frame
	void Update () {
		handle_Position ();
	}

	// ====================================================================================
	public void set_PlayerNewPosition(int dir) {
		if (moveCount >= 0f)
			return;

		if (dir == 0)// right
			nx = px + 1;
		else if (dir == 1)// down
			ny = py + 1;
		else if (dir == 2)// left
			nx = px - 1;
		else// up
			ny = py - 1;

		moveCount = 0f;
		anim.SetBool ("isMove", true);
	}

	public void init_Player(int x, int y) {
		px = nx = x;
		py = ny = y;
		moveCount = -1f;

		float sx = getx (x);
		float sy = gety (y);

		Vector3 pos = new Vector3 (sx, sy, -1f);
		transform.localPosition = pos;
	}

	public void set_PlayerView(int dir) {
		transform.localScale = new Vector3((dir == 1) ? 1f : -1f, 1f, 1f);
	}

	public int getNx() {
		return nx;
	}

	public int getNy() {
		return ny;
	}

	public bool isThere(int x, int y, int dir) {
		if (dir == 0 && ((x + 1 == px) && (y == py)))// right
			return true;
		else if (dir == 1 && ((x == px) && (y + 1 == py)))// down
			return true;
		else if (dir == 2 && ((x - 1 == px) && (y == py)))// left
			return true;
		else if (dir == 3 && ((x == px) && (y - 1 == py)))// up
			return true;

		return false;
	}

	// ====================================================================================
	void handle_Position() {
		if (moveCount < 0f)
			return;

		moveCount += Time.deltaTime;
		if (moveCount >= 1f)
			moveCount = 1f;

		float x = Mathf.Lerp(getx(px), getx(nx), moveCount);
		float y = Mathf.Lerp(gety(py), gety(ny), moveCount);
		transform.localPosition = new Vector3 (x, y, -1f);

		if (moveCount == 1f) {
			px = nx;
			py = ny;
			moveCount = -1f;
			anim.SetBool ("isMove", false);
		}
	}

	// ====================================================================================
	float getx(int x) {
		return (-3.25f + 1.3f * x);
	}

	float gety(int y){
		return (3.25f - 0.35f - 1.3f * y);
	}
}