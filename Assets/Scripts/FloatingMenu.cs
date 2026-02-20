using UnityEngine;
using TMPro;

public class FloatingMenu : MonoBehaviour
{
    private TextMeshPro _tms;
    void Start()
    {
        _tms = GetComponentInChildren<TextMeshPro>();
    }

    void Update()
    {
        Vector3 directionToCamera = Camera.main.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(-directionToCamera);

        _tms.text = $"Local: {transform.localPosition} \nGlobal: {transform.position}";
    }
}
