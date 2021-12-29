using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMng : MonoBehaviour {
	bool bFlag = false;

	void OnMouseDown() {
		if (bFlag)
			return;

		bFlag = true;
		SceneManager.LoadScene ("Game");
	}

	void Update() {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.Quit ();
		}
	}
}
