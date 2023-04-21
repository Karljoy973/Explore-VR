using System;
using TMPro;
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
    [SerializeField] private TMP_Text TextInteractTogglePictureMode;
    [SerializeField] private TMP_Text TextInteractTakePicture;
    [SerializeField] private TMP_Text TextControls;
    [SerializeField] private RawImage CameraLiveFeedTarget;
    [SerializeField] private float CameraLiveFeedResolution = 500f;

    private string _textInteractTogglePictureModeContents;
    private string _textInteractTakePictureContents;
    private string _textControlsContents;
    
    private void Start() {
        _textInteractTogglePictureModeContents = TextInteractTogglePictureMode.text;
        TextInteractTogglePictureMode.text = "";
        
        _textInteractTakePictureContents = TextInteractTakePicture.text;
        TextInteractTakePicture.text = "";
        
        _textControlsContents = TextControls.text;
    }

    public override void OnInteract() {  // on interact, pick up camera
        // teleport so player carries it
        DataStore.instance.PickedUpAppareilPhoto = true;
        transform.parent = CameraCarryPointParent;
        transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);

        // enable livestream to camera object
        EnableLiveStreamToCameraScreen();
        
        // show UI
        TextInteractTogglePictureMode.text = _textInteractTogglePictureModeContents;
    }

    public override bool CanInteract() {  // disallow raycast interaction if the camera has already been picked up
        return !DataStore.instance.PickedUpAppareilPhoto;
    }

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
                AppareilPhotoCamera.targetTexture = null;  // disable livestream to camera object
                TextInteractTakePicture.text = _textInteractTakePictureContents;
                TextControls.text = "";
            }
            else {
                EnableLiveStreamToCameraScreen();  // enable livestream to camera object
                TextInteractTakePicture.text = "";
                TextControls.text = _textControlsContents;
            }
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

}
