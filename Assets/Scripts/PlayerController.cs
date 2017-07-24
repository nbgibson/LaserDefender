using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed = 15.0f;
    public float padding = 1;
    public GameObject projectile;
    public float projectileSpeed;
    public float firingRate = 0.2f;
    public float health = 250f;

    private float xmin;
    private float xmax;


    void Start()
    {
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        xmin = leftmost.x+ padding;
        xmax = rightmost.x - padding;
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("Fire", 0.000001f, firingRate);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke("Fire");
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //transform.position += new Vector3(-speed * Time.deltaTime, 0f, 0f);

            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            //transform.position += new Vector3(speed * Time.deltaTime, 0f, 0f);
            transform.position += Vector3.right * speed * Time.deltaTime;
        }

        //restrict to gameplay area
        float newX = Mathf.Clamp(transform.position.x, xmin, xmax);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }   

    void Fire()
    {
        Vector3 offset = new Vector3(0f, 1f, 0f);
        GameObject beam = Instantiate(projectile, transform.position + offset, Quaternion.identity) as GameObject;
        beam.rigidbody2D.velocity = new Vector3(0, projectileSpeed, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        Projectile missile = collision.gameObject.GetComponent<Projectile>();
        if (missile)
        {
            Debug.Log("Player collided with missile");
            health -= missile.GetDamage();
            missile.Hit();
            if (health <= 0)
            {
                Debug.Log("Player destroyed");
                Destroy(gameObject);
            }
        }
    }
}

    
