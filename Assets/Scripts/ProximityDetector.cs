/*
J'ai désactivé le script de la ligne car il n'était pas complètement fonctionnel et rendait le reste du projet impossible à utiliser. La ligne s'activait correctement mais son emplacement était incorrecte. Si vous voulez voir la ligne, il suffit de décommenter toute les lignes commentées
*/

using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class ProximityDetector : MonoBehaviour
{
    [SerializeField] private GameObject controller;
    public GameObject[] cubes;
    public Material selectedColor;
    public float sizeScale;
    public float sizeChange;
    private List<bool> hasGrown;
    public Material[] originalColor;

    public float angleLimit;
    //private lineRenderer line;
    private float angle;
    private TMP_Text myText;

    void Start(){
        hasGrown = new List<bool>();
        for (int i = 0; i<cubes.Length; i++){
            hasGrown.Add(false);
        }
        //line = gameObject.AddComponent<//lineRenderer>();
        //line.startWidth = 0.2f;
        //line.endWidth = 0.2f;
        //line.positionCount = 2;
        //line.SetPosition(0, controller.transform.position);
        //line.enabled = false;
        GameObject textBox = new GameObject("textBox");
        myText = textBox.AddComponent<TextMeshPro>();
        textBox.transform.position = controller.transform.position + controller.transform.forward*2;
        myText.fontSize = 3;
    }

    void Update()
    {
        Vector3 Direction;
        float minDistance = 100000000;
        int minCube = 0;
        for(int i = 0; i<cubes.Length; i++){
            float CurDist = Vector3.Distance(cubes[i].transform.position,controller.transform.position);
            if(CurDist < sizeChange){
                MeshRenderer mr;
                if(cubes[i].TryGetComponent<MeshRenderer>(out mr)) mr.material = selectedColor;
                Debug.Log("ok");
            }
            else{
                MeshRenderer mr;
                if(cubes[i].TryGetComponent<MeshRenderer>(out mr)) mr.material = originalColor[i];
            }
            if(CurDist < minDistance){
                minDistance = CurDist;
                minCube = i;
            }
        }
        Direction = cubes[minCube].transform.position - controller.transform.position;
        angle = Vector3.Angle(Direction,controller.transform.forward);
        myText.text = "angle: " + angle + "\ndistance: " + minDistance;
        if(!hasGrown[minCube] && angle < angleLimit){
            for(int i = 0; i<cubes.Length; i++){
                if(hasGrown[i]){
                    cubes[i].transform.localScale = cubes[i].transform.localScale*(1/sizeScale);
                    hasGrown[i] = false;
                }
            }
            //line.SetPosition(1, cubes[minCube].transform.position);
            //line.enabled = true;
            cubes[minCube].transform.localScale = cubes[minCube].transform.localScale*sizeScale;
            hasGrown[minCube] = true;
        }
        //else if(angle < angleLimit) line.enabled = true;
        //else line.enabled = false;
    }
}
