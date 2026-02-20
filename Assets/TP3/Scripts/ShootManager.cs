using UnityEngine;
using System.Collections;
using UnityEngine.XR;

public class ShootManager : MonoBehaviour
{
    private InputDevice controller;
    private float AngleTolerance = 10f;
    [SerializeField] private GameObject particules;
    private AudioSource sound;
    [SerializeField] private GameObject gameController;
    
    private GameObject[] targets;
    private bool triggerPressed = false;

    void Start()
    {
        targets = GameObject.FindGameObjectsWithTag("target");
        controller = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
    }

    // Update is called once per frame
    void Update()
    {
        if(!controller.isValid)
        {
	        controller=InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
    	    if(!controller.isValid) return;
        }
        bool ancientTriggerPressed = triggerPressed;
        if (controller.TryGetFeatureValue(CommonUsages.triggerButton, out triggerPressed))
        {
            if(!ancientTriggerPressed && triggerPressed){
                for(int i = 0; i<targets.Length; i++)
                {
                    Vector3 controllerPosition;
                    Vector3 Direction = Vector3.zero;
                    Quaternion controllerRotation;
                    Vector3 controllerForward = Vector3.zero;
                    if(controller.TryGetFeatureValue(CommonUsages.devicePosition, out controllerPosition)) Direction = targets[i].transform.position - controllerPosition;
                    if(Vector3.Angle(Direction,gameController.transform.forward) < AngleTolerance)
                    {
                        StartCoroutine(Shoot(i));
                    }
                }
            } 
        }
    }

    private IEnumerator Shoot(int target){
        GameObject instantiatedParticules = Instantiate(particules, targets[target].transform.position, Quaternion.identity);
        if(targets[target].TryGetComponent<AudioSource>(out sound)) sound.Play();
        yield return new WaitForSeconds(3f);
        Destroy(instantiatedParticules);
    }
}
