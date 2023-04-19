using UnityEngine;

public class MenuTeleporter : InteractableObject {

    [SerializeField] private CharacterController Player;
    
    public override void OnInteract() {
        if (Menu.instance.SelectedLevel) {
            Player.enabled = false;
            Player.transform.SetPositionAndRotation(
                Menu.instance.SelectedLevel.TeleportPosition,
                Quaternion.Euler(Menu.instance.SelectedLevel.TeleportRotation)
            );
            Player.enabled = true;
        }
    }
    
}
