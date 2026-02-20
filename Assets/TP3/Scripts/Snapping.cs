using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Snapping : MonoBehaviour
{
    [SerializeField] private Transform personalSnap;
    [SerializeField] private Snapping otherObject;
    public float snapSpace;
    private bool _isAssembled;

    private LineRenderer lineRenderer;
    
    void Start()
    {
        // Add a LineRenderer component
        lineRenderer = gameObject.AddComponent<LineRenderer>();

        // Set the material
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));

        // Set the color
        lineRenderer.startColor = Color.white;
        lineRenderer.endColor = Color.white;

        // Set the width
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;

        // Set the number of vertices
        lineRenderer.positionCount = 2;

        // Set the positions of the vertices
        lineRenderer.SetPosition(0, personalSnap.position);
        lineRenderer.SetPosition(1, otherObject.transform.position);

        lineRenderer.enabled = false;

        var _grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        _grabInteractable.selectExited.AddListener(OnReleased);
    }

    // Update is called once per frame
    void Update()
    {
        if(!_isAssembled){
            if(Vector3.Distance(personalSnap.position, otherObject.transform.position) < snapSpace){
                lineRenderer.SetPosition(0, personalSnap.position);
                lineRenderer.SetPosition(1, otherObject.transform.position);
                lineRenderer.enabled = true;
            }
            else lineRenderer.enabled = false;
        }
    }

    void OnReleased(SelectExitEventArgs args)
    {
        if(!_isAssembled){
            if(Vector3.Distance(personalSnap.position, otherObject.transform.position) < snapSpace){
                GameObject assembled = new GameObject("New object");
                assembled.transform.position = otherObject.transform.position;
                assembled.transform.rotation = otherObject.transform.rotation;
                transform.SetParent(assembled.transform);
                otherObject.transform.SetParent(assembled.transform);

                transform.rotation = Quaternion.identity;

                Vector3 offset = personalSnap.position - transform.position;
                transform.position = otherObject.personalSnap.position - offset;
            }
        }
    }
}
