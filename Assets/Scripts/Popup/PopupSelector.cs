using UnityEngine;

public class PopupSelector : InteractableObject {

    [SerializeField] public Texture2D PopupTexture2D;

    public override void OnInteract() {
        DataStore.instance.SelectedPopup = DataStore.instance.SelectedPopup != this ? this : null;
        PopupDisplayer.instance.ShowCurrentPopup();
    }
    
}
