﻿using UnityEngine;
using System.Collections;

// TODO die kugeln kollidieren miteinander :(
public class Bullet : MonoBehaviour {
    public float speed;
    public int hitValue;
    public bool isPlayerBullet;

	// Use this for initialization
	void Start () {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        GameObject target = other.gameObject;
        if (!isPlayerBullet && (target.tag == "Base Tree"))
        {
            target.GetComponent<BaseTree>().hit(gameObject);
            Destroy(gameObject);
        }
        else if (!isPlayerBullet && (target.tag == "Player"))
        {
            //target.GetComponent<Player>().hit(gameObject);
            Destroy(gameObject);
        }
        else if (isPlayerBullet && (target.tag == "Enemy"))
        {
            target.GetComponent<Enemy>().hit(gameObject);
            Destroy(gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        GameObject target = other.gameObject;
        if ((target.name == "Game Area"))
        {
            Destroy(gameObject);
        }
    }
}