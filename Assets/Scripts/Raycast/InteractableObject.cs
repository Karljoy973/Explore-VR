using UnityEngine;

public class InteractableObject : MonoBehaviour {

    public virtual bool CanInteract() => !DataStore.instance.PictureMode && DataStore.instance.SelectedPopup == null;
    public virtual void OnInteract() {
    }

}
