using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {

	// Use this for initialization
	public GameManager gameManager;
	private GameRules gameRules;
	private int team;
	private Vector2 pos;
	public Vector3 move;
	public int[,] board;
	public int value;
	public List<Board> possibleMoves;


	void Start () {
		GameObject gameManagerObject = GameObject.Find("GameManager");
		gameRules = gameManagerObject.GetComponent<GameRules> ();
		gameManager = gameManagerObject.GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public int[,] LoadBoard(GameObject player){
		if (!gameRules) {
			GameObject gameManagerObject = GameObject.Find ("GameManager");
			gameRules = gameManagerObject.GetComponent<GameRules> ();
			gameManager = gameManagerObject.GetComponent<GameManager> ();
		}
		if (board == null){
			int row = (int)((gameRules.baseLineRight - gameRules.baseLineLeft) / 2);
			int col = (int)(gameRules.farSideLine - gameRules.nearSideLine) / 2;
			board = new int[row, col];
		}
		pos = new Vector2();
		FillBoard (player);
		return board;
	}

	public void FillBoard( GameObject myPlayer) {         //tba

		/* 
		int team = myPlayer.GetComponent<Player> ().team;
		pos.x = (myPlayer.transform.position.x + 39) / 2;
		pos.y = (myPlayer.transform.position.z + 81) / 2;
		if (team==1){
		foreach (GameObject player in gameManager.Players) {
				if (player.GetComponent<Player>().team == 1) {
					int row = (int)(player.transform.GetChild (0).transform.position.x + 39)/2;
					int col = (int) (player.transform.GetChild (0).transform.position.z + 81)/2;
					board [row, col] = 1;
			} else {
					if (player.GetComponent<Player>().team == 2) {
						int row = (int)(player.transform.GetChild (0).transform.position.x + 39)/2;
						int col = (int)(player.transform.GetChild (0).transform.position.z + 81)/2;
						board [row, col] = -1;
					}

				}
			}
			foreach (GameObject ball in gameManager.Balls) {
				if (ball.transform.position.x >= gameRules.halfCourtLine) {
					int row = (int)(ball.transform.position.x + 39)/2;
					int col = (int)(ball.transform.position.z + 81)/2;
					board [row, col] = 2;
				}
			}
		}
		if (team == 2) {
			foreach (GameObject player in gameManager.Players) {
				if (player.GetComponent<Player>().team == 1) {
					int row = (int)(player.transform.GetChild (0).transform.position.x + 39)/2;
					int col = (int)(player.transform.GetChild (0).transform.position.z + 81)/2;
					board [row, col] = -1;
				} else {
					if (player.GetComponent<Player>().team == 2) {
						int row = (int)(player.transform.GetChild (0).transform.position.x + 39)/2;
						int col = (int)(player.transform.GetChild (0).transform.position.z + 81)/2;
						board [row, col] = 1;
					}

				}
			}
			foreach (GameObject ball in gameManager.Balls) {
				if (ball.transform.position.x <= gameRules.halfCourtLine) {
					int row = (int)(ball.transform.position.x + 39)/2;
					int col = (int)(ball.transform.position.z + 81)/2;
					board [row, col] = 2;
				}
			}
		}
		*/
	}

	public void PrintBoard(){
		print ("Board");
		string row;
		string col;
		int i = 0;
		int j = 0;

		for (i = 0; i < board.GetLength (0); i++) {
			row = "";
			for (j = 0; j < board.GetLength (1); j++) {
				row += board [i, j] + " ";
			}
			print (row);
		}
	}

	public Vector3 GetMove(int type){
		if (type == 0) {
			return new Vector3 (Random.Range(-2.0f,2.0f), 0.0f, Random.Range(-2.0f,2.0f));
		}
		if (type == 1) {
			int depth = 0;
			possibleMoves = GenerateMoves (pos,board,depth);
			foreach (Board board in possibleMoves) {
				board.possibleMoves = board.GenerateMoves (board.pos, board.board ,depth +1);
			}
			return new Vector3 (0f, 0f, 0f);
		}
		return new Vector3 (0f, 0f, 0f);
	}

	public List <Board> GenerateMoves(Vector2 pos,int [,] board,int depth){
		

		return new List<Board> ();
	}


}