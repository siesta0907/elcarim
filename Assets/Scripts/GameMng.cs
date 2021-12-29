using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMng : MonoBehaviour {

	public GameObject[] m_Blocks;
	public GameObject m_prefabPlayer1;
	public GameObject m_prefabPlayer2;
	public GameObject MessagePanel;
	public Text m_MessageText;

	int[] m_dat = new int[36];
	Block[] m_block = new Block[36];
	Player[] m_player = new Player[2];

	// base flow
	int m_PlayerCount;
	int m_RoundNumber;
	float m_StartDelay = 3f;
	float m_EndDelay = 3f;

	WaitForSeconds m_StartWait;
	WaitForSeconds m_EndWait;

	void Start() {
		m_StartWait = new WaitForSeconds (m_StartDelay);
		m_EndWait = new WaitForSeconds (m_EndDelay);

		MakeBoardBlock ();
		SpawnAllPlayers ();

		StartCoroutine (GameLoop ());
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			SceneManager.LoadScene ("Main");
		}
	}

	// ====================================================================================
	IEnumerator GameLoop () {
		yield return StartCoroutine (RoundStarting ());
		yield return StartCoroutine (RoundPlaying ());
		yield return StartCoroutine (RoundEnding ());

		StartCoroutine (GameLoop ());
	}

	IEnumerator RoundStarting () 	{
		// Round Init
		m_PlayerCount = 2;

		// message show
		MessagePanel.SetActive(true);
		m_RoundNumber++;
		m_MessageText.text = "ROUND " + m_RoundNumber;

		yield return m_StartWait;
	}

	IEnumerator RoundPlaying () {
		MessagePanel.SetActive (false);


		while (m_PlayerCount != 1) 		{
			yield return null;
		}
	}

	IEnumerator RoundEnding () {
		// message show
		MessagePanel.SetActive(true);
		m_MessageText.text = "END";

		yield return m_EndWait;
	}
	// ====================================================================================
	public void setAttack(int pi) {
		if (m_player [1 - pi].isThere (m_player [pi].px, m_player [pi].py, 0)) {// attack right
			m_player [pi].set_PlayerView (1);
			m_player [pi].anim.SetTrigger("attackR");

		} else if (m_player [1 - pi].isThere (m_player [pi].px, m_player [pi].py, 1)) {// attack down
			m_player [pi].anim.SetTrigger("attackD");

		} else if (m_player [1 - pi].isThere (m_player [pi].px, m_player [pi].py, 2)) {// attack left
			m_player [pi].set_PlayerView (-1);
			m_player [pi].anim.SetTrigger("attackL");

		} else if (m_player [1 - pi].isThere (m_player [pi].px, m_player [pi].py, 3)) {// attack up
			m_player [pi].anim.SetTrigger("attackU");

		}

		Invoke ("setRestoreIdle", 1f);
	}

	public void setMoveR(int pi) {
		m_player [pi].set_PlayerNewPosition (0);
		m_player [pi].set_PlayerView (1);
		setPlayerState (pi);
	}

	public void setMoveD(int pi) {
		m_player [pi].set_PlayerNewPosition (1);
		setPlayerState (pi);
	}

	public void setMoveL(int pi) {
		m_player [pi].set_PlayerNewPosition (2);
		m_player [pi].set_PlayerView (-1);
		setPlayerState (pi);
	}

	public void setMoveU(int pi) {
		m_player [pi].set_PlayerNewPosition (3);
		setPlayerState (pi);
	}

	// ====================================================================================
	void setRestoreIdle() {
		m_player [0].anim.SetTrigger ("setIdle");
		m_player [1].anim.SetTrigger ("setIdle");
	}

	void setPlayerState(int pi) {
		int nx = m_player [pi].getNx ();
		int ny = m_player [pi].getNy ();
		int idx = ny * 6 + nx;

		m_block [idx].setState ((pi == 0) ? 2 : 3);
	}

	void MakeBoardBlock() {
		float sx = -3.25f;
		float sy = 3.25f;

		mix ();

		for (int i = 0; i < m_dat.Length; i++) {
			m_block [i] = Instantiate (m_Blocks [m_dat [i]], new Vector3 (sx + 1.3f * (i % 6), sy - 1.3f * (i / 6), 0f), Quaternion.identity).GetComponent<Block> ();
		}
	}

	void SpawnAllPlayers() {
		m_player [0] = Instantiate (m_prefabPlayer1).GetComponent<Player> ();
		m_player [1] = Instantiate (m_prefabPlayer2).GetComponent<Player> ();
	}

	// ====================================================================================
	void mix() {
		for (int i = 0; i < m_dat.Length; i++)
			m_dat [i] = i;

		int idx = 0;
		int mx = m_dat.Length;
		while (mx > 0) {
			idx = Random.Range (0, mx);
			swap (ref m_dat [idx], ref m_dat [mx - 1]);
			mx--;
		}
	}

	void swap(ref int a, ref int b) {
		int t = a;
		a = b;
		b = t;
	}
}
