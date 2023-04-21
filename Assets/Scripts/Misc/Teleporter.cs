using UnityEngine;

public class Teleporter : InteractableObject {

    [SerializeField] private CharacterController Player;
    [SerializeField] public Vector3 TeleportPosition;
    [SerializeField] public Vector3 TeleportRotation;
    
    public override void OnInteract() {
        Player.enabled = false;
        Player.transform.SetPositionAndRotation(TeleportPosition, Quaternion.Euler(TeleportRotation));
        Player.enabled = true;
    }
    
}
