using UnityEngine;

public class PopupDisplayer : InteractableObject {

    public static PopupDisplayer instance;
    public void Start() => instance = this;

    [SerializeField] private Renderer PopupRenderer;

    public void ShowCurrentPopup() {
        if (DataStore.instance.SelectedPopup == null) {
            PopupRenderer.enabled = false;
        } else {
            PopupRenderer.material = DataStore.instance.SelectedPopup.PopupMaterial;
            PopupRenderer.enabled = true;
        }
    }
    
}
