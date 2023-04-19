using UnityEngine;

public class MenuLevel : InteractableObject {
    
    [SerializeField] public Vector3 TeleportPosition;
    
    public override void OnInteract() {
        Menu.instance.SelectedLevel = this;
    }
    
}
