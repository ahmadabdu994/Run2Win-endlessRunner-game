using UnityEngine;


public class CoinsController : MonoBehaviour
{
    public AudioSource audioSource;
    public int rotateSpeed = 2;
    void Update()
    {
        transform.Rotate(0, rotateSpeed, 0, Space.World);
    }
    
    void OnTriggerEnter(Collider other)
    {
        audioSource.Play();
        PlayerController.coinCount += 1;
        this.gameObject.SetActive(false);
    }
}
