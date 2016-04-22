using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public int hp;
    public GameObject dropObject;

    Transform targetTransform;
    NavMeshAgent agent;

	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        targetTransform = chooseTarget();
        agent.destination = targetTransform.position;
        agent.Resume();
	}

    public void initialize()
    {

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    // TODO add player and building detection
    Transform chooseTarget()
    {
        return GameObject.FindGameObjectWithTag("Base Tree").transform;
    }

    void shoot ()
    {

    }
}
