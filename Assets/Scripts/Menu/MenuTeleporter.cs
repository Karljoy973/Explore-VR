using UnityEngine;

public class MenuTeleporter : InteractableObject {

    [SerializeField] private CharacterController Player;
    [SerializeField] private int MuseumLevelIndex;
    [SerializeField] private Vector3 MuseumTeleportPosition;
    [SerializeField] private Quaternion MuseumTeleportRotation;
    [SerializeField] private AudioClip WrongLevelSound;
    
    private AudioSource _audioSource;

    private void Start() {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = WrongLevelSound;
    }

    public override void OnInteract() {
        if (DataStore.instance.LevelIndex == MuseumLevelIndex) {
            Player.enabled = false;
            Player.transform.SetPositionAndRotation(MuseumTeleportPosition, MuseumTeleportRotation);
            Player.enabled = true;
        } else
            _audioSource.Play();
    }
    
}
