﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

    public float Speed = 4f;
    private float move = 0f;
    public float tilt;

    public int Health;
    public int Money;
    public Rigidbody PlayerRigidbody;

    public float fireRate;
    private float nextFire;

    public GameObject prefab;
    public float distance = 10.0f;

    void Start()
    {
        PlayerRigidbody = this.GetComponent<Rigidbody>();
    }

	void LookAtMouse () 
	{
		// Generate a plane that intersects the transform's position with an upwards normal.
		Plane playerPlane = new Plane(Vector3.up, transform.position);

		// Generate a ray from the cursor position
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		// Determine the point where the cursor ray intersects the plane.
		// This will be the point that the object must look towards to be looking at the mouse.
		// Raycasting to a Plane object only gives us a distance, so we'll have to take the distance,
		//   then find the point along that ray that meets that distance.  This will be the point
		//   to look at.
		float hitdist = 0.0f;
		// If the ray is parallel to the plane, Raycast will return false.
		if (playerPlane.Raycast (ray, out hitdist)) 
		{
			// Get the point along the ray that hits the calculated distance.
			Vector3 targetPoint = ray.GetPoint(hitdist);

			// Determine the target rotation.  This is the rotation if the transform looks at the target point.
			Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);

			// Smoothly rotate towards the target point.
			transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 20* Time.deltaTime);
		}
	}



    void FixedUpdate()
    {
        Move();
        Shoot();
		//LookAtMouse (); 
		 
    }

    void Move()
    {
        var move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        PlayerRigidbody.position += move * Speed * Time.deltaTime;
        //Debug.Log("Horizontal: " + Input.GetAxis("Horizontal").ToString());
        //Debug.Log("Vertical: " + Input.GetAxis("Vertical").ToString());

		transform.rotation = Quaternion.LookRotation(move);

    }

    void Shoot()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            Debug.Log("Shoot-Funktion");
            nextFire = Time.time + fireRate;



            var position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
            position = Camera.main.ScreenToWorldPoint(position);
            
			var go = Instantiate(prefab, transform.position, transform.rotation) as GameObject;
           // go.transform.LookAt(position);
            Debug.Log(position);
            go.GetComponent<Rigidbody>().AddForce(go.transform.forward * 1000);

        }
    }
}