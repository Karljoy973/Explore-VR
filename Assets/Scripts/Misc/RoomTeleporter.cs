using UnityEngine;

public class RoomTeleporter : InteractableObject {

    [SerializeField] private CharacterController Player;
    [SerializeField] public Vector3 TeleportPosition;
    [SerializeField] public Vector3 TeleportRotation;
    
    public override void OnInteract() {
        Player.enabled = false;
        Player.transform.SetPositionAndRotation(TeleportPosition, Quaternion.Euler(TeleportRotation));
        Player.enabled = true;
        
        if (DataStore.instance.PickedUpAppareilPhoto) {
            var appareilPhoto = FindObjectOfType<AppareilPhoto>();
            appareilPhoto.transform.parent = null;
            appareilPhoto.transform.SetPositionAndRotation(appareilPhoto.InitialPosition, appareilPhoto.InitialRotation);

            DataStore.instance.PickedUpAppareilPhoto = false;
        }
    }
    
}
