using UnityEngine;

public class BallScript : MonoBehaviour
{
    public Vector3 startPosition;

    [SerializeField] private AudioSource winSound;
    [SerializeField] private Rigidbody rb;

    void OnCollisionEnter(Collision other){
        if(other.gameObject.CompareTag("target"))
        {
            winSound.Play();
            Destroy(other.gameObject);
        }
        if (!other.gameObject.CompareTag("floor"))
        {
            transform.position = startPosition;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        } 
    }
}
