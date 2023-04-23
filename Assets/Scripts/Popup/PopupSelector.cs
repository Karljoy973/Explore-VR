using UnityEngine;

public class PopupSelector : InteractableObject {

    [SerializeField] public Material PopupMaterial;

    public override void OnInteract() {
        DataStore.instance.SelectedPopup = DataStore.instance.SelectedPopup != this ? this : null;
        PopupDisplayer.instance.ShowCurrentPopup();
    }
    
}
