using UnityEngine;

public class MenuTeleporter : InteractableObject {

    [SerializeField] private CharacterController Player;
    
    public override void OnInteract() {
        if (DataStore.instance.SelectedLevel) {
            Player.enabled = false;
            Player.transform.SetPositionAndRotation(
                DataStore.instance.SelectedLevel.TeleportPosition,
                Quaternion.Euler(DataStore.instance.SelectedLevel.TeleportRotation)
            );
            Player.enabled = true;
        }
    }
    
}
