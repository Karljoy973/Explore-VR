using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class AppareilPhoto : InteractableObject {

    [Header("Player")]
    [SerializeField] private FirstPersonController Player;
    [SerializeField] private Camera PlayerCamera;
    
    [Header("Appareil Photo")]
    [SerializeField] private Camera AppareilPhotoCamera;
    [SerializeField] private Transform CameraCarryPointParent;
    
    [Header("UI and Camera Live Feed")]
    [SerializeField] private Image UIDot;
    [SerializeField] private RawImage CameraLiveFeedTarget;
    [SerializeField] private float CameraLiveFeedResolution = 500f;
    
    private void Update() {
        if (!DataStore.instance.PickedUpAppareilPhoto)
            return;
        
        // toggle picture mode
        if (Input.GetMouseButtonDown(1)) {
            var pictureMode = !DataStore.instance.PictureMode;
            DataStore.instance.PictureMode = pictureMode;
            
            Player.enabled = !pictureMode;
            PlayerCamera.depth = pictureMode ? 0 : 1;  // depth 1 = set as main camera
            AppareilPhotoCamera.depth = pictureMode ? 1 : 0;  // depth 1 = set as main camera
            UIDot.enabled = !pictureMode;

            if (pictureMode) {
                AppareilPhotoCamera.transform.localRotation = PlayerCamera.transform.localRotation;
                AppareilPhotoCamera.targetTexture = null;
            }
            // disable picture mode : enable live stream of appareil photo to object screen in-game
            else
                EnableLiveStreamToCameraScreen();
        }
        // take picture
        else if (Input.GetKeyDown(KeyCode.Space)) {
            // TODO : save image from camera
        }
    }

    private void EnableLiveStreamToCameraScreen() {
        var rect = CameraLiveFeedTarget.rectTransform.rect;
        var renderTexture = new RenderTexture((int) (CameraLiveFeedResolution * rect.width), (int) (CameraLiveFeedResolution * rect.height), 24);
        AppareilPhotoCamera.targetTexture = renderTexture;
        CameraLiveFeedTarget.texture = renderTexture;
    }

    // on interact, pick up camera
    public override void OnInteract() {
        // teleport so player carries it
        DataStore.instance.PickedUpAppareilPhoto = true;
        transform.parent = CameraCarryPointParent;
        transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);

        // enable livestream to camera object
        EnableLiveStreamToCameraScreen();
    }

    // disallow raycast interaction if the camera has already been picked up
    public override bool CanInteract() => !DataStore.instance.PickedUpAppareilPhoto;

}
