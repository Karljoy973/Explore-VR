using UnityEngine;
using UnityEngine.Serialization;

public class MenuSelector : InteractableObject {
    
    [SerializeField] [FormerlySerializedAs("materials")] private Material[] Materials;
    
    private Renderer _renderer;
    
    private void Start() {
        _renderer = GetComponent<Renderer>();
        _renderer.enabled = true;
    }

    public override void OnInteract() {
        if (Materials.Length == 0)
            return;

        if (Input.GetMouseButtonDown(0)) {
            DataStore.instance.LevelIndex = ++DataStore.instance.LevelIndex % Materials.Length;
            _renderer.sharedMaterial = Materials[DataStore.instance.LevelIndex];
        }
    }
    
}
