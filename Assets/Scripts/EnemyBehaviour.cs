using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {

    public float health = 150f;
    public float projectileSpeed = 10f;
    public GameObject projectile;
    public float shotsPerSeconds = 0.5f;
    public int scoreValue = 150;

    private ScoreKeeper scoreKeeper;

    public void Start()
    {
        scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Projectile missile = collision.gameObject.GetComponent<Projectile>();
        if (missile)
        {
            health -= missile.GetDamage();
            missile.Hit();
            if(health <= 0)
            {
                Destroy(gameObject);
                scoreKeeper.Score(scoreValue);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        float probability = Time.deltaTime * shotsPerSeconds;
        if(Random.value < probability)
        {
            Fire();
        }
	}

    void Fire()
    {
        Vector3 startPosition = transform.position + new Vector3(0f, -1f, 0f);
        GameObject missile = Instantiate(projectile, startPosition, Quaternion.identity) as GameObject;
        missile.rigidbody2D.velocity = new Vector2(0f, -projectileSpeed);
    }
}
