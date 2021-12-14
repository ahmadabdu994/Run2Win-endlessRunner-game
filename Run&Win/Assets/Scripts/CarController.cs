using UnityEngine;

public class CarController : MonoBehaviour
{
    public Transform player;
    public int speed = 10;
    public int distance = 100;
    void Start()
    {
        carMove();
    }
    void FixedUpdate()
    {
        carDistance();

    }
    // methods
    public void carMove()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        int r = Random.Range(1, 3);
        if (r == 1)
        {
            transform.position = new Vector3(-3, transform.position.y, transform.position.z);
        }
        else if (r == 2)
        {
            transform.position = new Vector3(0, transform.position.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(3, transform.position.y, transform.position.z);
        }
    }
    public void carDistance()
    {
        if (!GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().isDead)
        {
            if (Vector3.Distance(transform.position, player.position) <= distance)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - speed * Time.deltaTime);
            }
        }
    }
}
