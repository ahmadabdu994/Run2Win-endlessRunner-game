using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {

        if (other.collider.tag == "Player")
        {
            if (!other.collider.GetComponent<PlayerController>().isDead)
            {
                other.collider.GetComponent<PlayerController>().killPlayer();
            }
        }

    }
}
