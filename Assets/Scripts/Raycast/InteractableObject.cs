using UnityEngine;

public class InteractableObject : MonoBehaviour {

    public virtual bool CanInteract() => !DataStore.instance.PictureMode;
    public virtual void OnInteract() {
    }

}
