using UnityEngine;

public class InteractableObject : MonoBehaviour {

    public virtual bool CanInteract() => true;
    public virtual void OnInteract() {
    }

}
