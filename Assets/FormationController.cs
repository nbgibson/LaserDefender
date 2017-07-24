using UnityEngine;
using System.Collections;

public class FormationController : MonoBehaviour
{

    public GameObject enemyPrefab;
    public float width = 10f;
    public float height = 5f;
    public float speed = 15f;

    private bool movingRight = true;
    private float xmax;
    private float xmin;

    // Use this for initialization
    void Start()
    {
        SpawnEnemies();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
    }

    // Update is called once per frame
    void Update()
    {
        if (movingRight)
        {
            //Move right
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        else
        {
            //Move left
            transform.position += Vector3.left * speed * Time.deltaTime;
        }

        float rightEdgeOfFormation = transform.position.x + (0.5f * width);
        float leftEdgeOfFormation = transform.position.x - (0.5f * width);

        if (leftEdgeOfFormation < xmin)
        {
            movingRight = true;
        }
        else if (rightEdgeOfFormation > xmax)
        {
            movingRight = false;
        }

        if (AllMembersDead())
        {
            Debug.Log("Enemy formation destroyed.");
            SpawnEnemies();
        }
    }

    bool AllMembersDead()
    {
        foreach(Transform childPositionGameObject in transform)
        {
            if (childPositionGameObject.childCount > 0)
            {
                return false;
            }
        }
        return true;
    }

    private void SpawnEnemies()
    {
        foreach (Transform child in transform)
        {
            GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = child;

            float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
            Vector3 leftBoundry = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distanceToCamera));
            Vector3 rightBoundry = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distanceToCamera));

            xmax = rightBoundry.x;
            xmin = leftBoundry.x;
        }
    }
}
