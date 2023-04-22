using UnityEngine;
using System.Collections;

public class ChangeMaterialOnClick : MonoBehaviour {
    public Material[] materials; 
    public Renderer renderer; 
    private int i = 1;
    private void Start() {
      renderer = GetComponent<Renderer>();
      renderer.enabled = true;

    }

    private void OnMouseDown() {
        if (materials.Length == 0){
            return; 
        }    
        if (Input.GetMouseButtonDown(0)){
            i+=1;
            if(i==materials.Length+1){
                i  = 1;
            }
        renderer.sharedMaterial = materials[i-1];


        }
    }
}