using UnityEngine;
using System.Collections;
using System.Collections.Generic; 
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	private enum States{gameover, fightloop, buildloop, start};
	private States myState;

	[Header("Loop Timers")]
	public float fightLoopTime = 5.0f; 
	public float buildLooptime = 5.0f; 

	[Header("Enemy related Stuff (Lists)")]
	public List<Transform> enemySpawnPoints; 
	public List<GameObject> enemys; 

	[Header("Audio Files")]
	public AudioClip fightLoopAudio; 
	public AudioClip buildLoopAudio; 

	bool isSpawningEnemys = false; 
	bool loopTimerActive = false; 

	void Start () {
		myState = States.fightloop; 
	}
	
	void Update () {
		
		//GAMESTART
		if (myState == States.start) {
			Debug.Log ("Game started"); 
			myState = States.fightloop; 
		}

		//BUILDLOOP
		if (myState == States.buildloop) {
			if (!loopTimerActive) {
				StartCoroutine (LoopTimer (buildLooptime, myState, States.fightloop)); 
				loopTimerActive = true; 
			}		
		}

		//FIGHTLOOP
		if (myState == States.fightloop) {
			if (!isSpawningEnemys) {
				StartCoroutine(SpawnEnemys (enemys[0], enemySpawnPoints[Random.Range(0,enemySpawnPoints.Count)], 1.0f)); 
				isSpawningEnemys = true; 
			}
			if (!loopTimerActive) {
				StartCoroutine (LoopTimer (fightLoopTime, myState, States.buildloop)); 
				loopTimerActive = true; 
			}
		}

		//GAMEOVER
		if (myState == States.gameover) {
			Debug.Log ("Game Over"); 
		}
	}

	IEnumerator SpawnEnemys(GameObject enemy, Transform spawnPosition, float waitTime){
		GameObject myEnemy = (GameObject)Instantiate (enemy, spawnPosition.position, Quaternion.identity); 
		Debug.Log ("Enemy spawned"); 
		yield return new WaitForSeconds (waitTime); 
		isSpawningEnemys = false;
	}

	IEnumerator LoopTimer(float myTime, States initState, States nextState){
		yield return new WaitForSeconds (myTime); 
		if (initState == myState) {
			myState = nextState; 
		}
		Debug.Log ("Next State: " + nextState); 
		loopTimerActive = false; 
	}

	IEnumerator  RestartGame(){
		Debug.Log ("Restart in 2 Seconds.."); 
		yield return new WaitForSeconds (1); 
		SceneManager.LoadScene (0); 

	}

	public void GameOver(){
		myState = States.gameover; 
		Debug.Log ("Gameover"); 
	}
}
