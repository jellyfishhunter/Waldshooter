using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public int hp;
    public float range;
    public GameObject dropObject;
    public GameObject bullet;
    public float attackIntervall;

    Transform targetTransform;
    NavMeshAgent agent;
    float timeUntilnextShot = 0;

	// Use this for initialization
	void Start () {
        targetTransform = chooseTarget();
        moveToTarget();
	}

    void moveToTarget()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = targetTransform.position;
        agent.stoppingDistance = range;
        agent.Resume();
    }

    public void initialize()
    {

    }
	
	// Update is called once per frame
	void Update () {
        aimAndShoot();
    }

    // TODO add player and building detection
    Transform chooseTarget()
    {
        return GameObject.FindGameObjectWithTag("Base Tree").transform;
    }

    void aimAndShoot()
    {
        // aim
        face(targetTransform.position);

        // range check
        bool targetInRange = Vector3.Distance(targetTransform.position, transform.position) <= range;

        // reload and shoot
        if (timeUntilnextShot <= 0 && targetInRange)
        {
            shoot();
            timeUntilnextShot = attackIntervall;
            Debug.Log("shoot");
        }
        
        timeUntilnextShot -= Time.deltaTime;
    }

    void shoot ()
    {
        Vector3 bulletSpawnPosition = transform.position + transform.forward;
        GameObject bulletInstance = Instantiate(bullet, bulletSpawnPosition, transform.rotation) as GameObject;
        Bullet bulletScript = bulletInstance.GetComponent<Bullet>();
        bulletScript.isPlayerBullet = false;
    }

    public void hit(GameObject bullet)
    {
        hp -= bullet.GetComponent<Bullet>().hitValue;
        if (hp <= 0)
        {
            die();
        }
    }

    // TODO: enemy counter etc
    void die()
    {
        Destroy(gameObject);
    }

    public void face(Vector3 t)
    {
        Vector3 direction = (new Vector3(t.x, transform.position.y, t.z) - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
        }
    }
}
