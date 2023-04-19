using UnityEngine;

public class MenuTeleporter : InteractableObject {

    [SerializeField] private CharacterController Player;
    
    public override void OnInteract() {
        if (Menu.instance.SelectedLevel) {
            Player.transform.SetPositionAndRotation(
                Menu.instance.SelectedLevel.TeleportPosition,
                Quaternion.Euler(Menu.instance.SelectedLevel.TeleportRotation)
            );
        }
    }
    
}
