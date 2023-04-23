using UnityEngine;

public class RoomTeleporter : InteractableObject {

    [SerializeField] private CharacterController Player;
    [SerializeField] public Vector3 TeleportPosition;
    [SerializeField] public Vector3 TeleportRotation;
    [SerializeField] private AudioClip TeleportSound;
    
    private AudioSource _audioSource;
    
    private void Start() {
        _audioSource = GetComponent<AudioSource>();
    }

    public override void OnInteract() {
        Player.enabled = false;
        Player.transform.SetPositionAndRotation(TeleportPosition, Quaternion.Euler(TeleportRotation));
        Player.enabled = true;
        
        _audioSource.clip = TeleportSound;
        _audioSource.Play();
        
        if (DataStore.instance.PickedUpAppareilPhoto) {
            AppareilPhoto.instance.transform.parent = null;
            AppareilPhoto.instance.transform.SetPositionAndRotation(AppareilPhoto.instance.InitialPosition, AppareilPhoto.instance.InitialRotation);

            DataStore.instance.PickedUpAppareilPhoto = false;
            AppareilPhoto.instance.TextInteractTogglePictureMode.text = "";
        }
    }
    
}
