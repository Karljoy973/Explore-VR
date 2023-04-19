using UnityEngine;

public class MenuTeleporter : InteractableObject {

    [SerializeField] private CharacterController Player;
    
    public override void OnInteract() {
        if (Menu.instance.SelectedLevel)
            Player.transform.position = Menu.instance.SelectedLevel.TeleportPosition;
    }
    
}
