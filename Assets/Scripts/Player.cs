using UnityEngine;
using System.Collections;

// TODO was passiert, wenn der spieler stirbt?
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


    void FixedUpdate()
    {
        Move();
        Shoot();
    }

    void Move()
    {
        var move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        PlayerRigidbody.position += move * Speed * Time.deltaTime;
        //Debug.Log("Horizontal: " + Input.GetAxis("Horizontal").ToString());
        //Debug.Log("Vertical: " + Input.GetAxis("Vertical").ToString());


    }

    void Shoot()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            Debug.Log("Shoot-Funktion");
            nextFire = Time.time + fireRate;

            Vector3 position;
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit);
            position = hit.point;            
            

            /*
            var position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
            position = Camera.main.ScreenToWorldPoint(position);
            */

            var go = Instantiate(prefab, transform.position, Quaternion.identity) as GameObject;
            go.transform.LookAt(position);
            Debug.Log(position);
            go.GetComponent<Rigidbody>().AddForce(go.transform.forward * 1000);

        }
    }

    public void hit(GameObject bullet)
    {
        Debug.Log("Player Hit");

        Health -= bullet.GetComponent<Bullet>().hitValue;
        if (Health <= 0)
        {
            Debug.Log("Player Dead");
            //die();
        }
    }
}