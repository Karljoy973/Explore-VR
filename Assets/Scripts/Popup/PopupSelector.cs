using System;
using UnityEngine;

public class PopupSelector : InteractableObject {

    [SerializeField] public Texture2D PopupTexture2D;
    
    public override void OnInteract() {
        if (DateTime.Now - PopupDisplayer.instance.LastPopupUpdate >= TimeSpan.FromSeconds(0.25f)) {
            DataStore.instance.SelectedPopup = this;
            PopupDisplayer.instance.UpdatePopup();
        }
    }
    
}
