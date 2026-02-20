using UnityEngine;

public class BallScript : MonoBehaviour
{
    public Vector3 startPosition;

    [SerializeField] private AudioSource winSound;

    void OnCollisionEnter(Collision other){
        if(other.gameObject.CompareTag("target"))
        {
            winSound.Play();
            Destroy(other.gameObject);
        }
        transform.position = startPosition;
    }
}
