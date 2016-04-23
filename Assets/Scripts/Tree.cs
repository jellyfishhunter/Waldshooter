using UnityEngine;
using System.Collections;

public class Tree : MonoBehaviour {

	public int hp = 100; 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.A)) {
			GetHit (50); 
		}
	}

	void GetHit(int hitAmount){
		hp -= hitAmount; 
		if (hp <= 0) {
			Debug.Log ("Tree is dead"); 
			GameObject gameManager = GameObject.Find ("Game Manager"); 
			gameManager.SendMessage ("GameOver"); 
			Destroy (this.gameObject); 

		}
	}

    // wird von den Bullets aufgerufen
    public void hit(GameObject bullet)
    {
        hp -= bullet.GetComponent<Bullet>().hitValue;

        if (hp <= 0)
        {
            Debug.Log("Base destroyed");
            GameObject gameManager = GameObject.Find("Game Manager");
            gameManager.SendMessage("GameOver");
        }

    }
}
