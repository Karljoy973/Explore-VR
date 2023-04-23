using UnityEngine;
using UnityEngine.Serialization;

public class MenuSelector : MonoBehaviour {
    
    [SerializeField] [FormerlySerializedAs("materials")] private Material[] Materials;
    [SerializeField] [FormerlySerializedAs("renderer")] private Renderer Renderer;
    
    private void Start() {
        Renderer = GetComponent<Renderer>();
        Renderer.enabled = true;
    }

    private void OnMouseDown() {
        if (Materials.Length == 0)
            return;

        if (Input.GetMouseButtonDown(0)) {
            DataStore.instance.LevelIndex = ++DataStore.instance.LevelIndex % Materials.Length;
            Renderer.sharedMaterial = Materials[DataStore.instance.LevelIndex];
        }
    }
    
}
