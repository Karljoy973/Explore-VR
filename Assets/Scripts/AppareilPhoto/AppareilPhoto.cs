using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class AppareilPhoto : InteractableObject {

    public static AppareilPhoto instance;

    [Header("Player")]
    [SerializeField] private FirstPersonController Player;
    [SerializeField] private Camera PlayerCamera;

    [Header("Appareil Photo")]
    [SerializeField] private Camera AppareilPhotoCamera;
    [SerializeField] private Transform CameraCarryPointParent;

    [Header("UI and Camera Live Feed")]
    [SerializeField] private Image UIDot;
    [SerializeField] public TMP_Text TextInteractTogglePictureMode;
    [SerializeField] private TMP_Text TextInteractTakePicture;
    [SerializeField] private TMP_Text TextControls;
    [SerializeField] private RawImage CameraLiveFeedTarget;
    [SerializeField] private float CameraLiveFeedResolution = 500f;

    [NonSerialized] public Vector3 InitialPosition;
    [NonSerialized] public Quaternion InitialRotation;

    private string _textInteractTogglePictureModeContents;
    private string _textInteractTakePictureContents;
    private string _textControlsContents;

    [SerializeField] private AudioClip pictureSound;
    private AudioSource _audioSource;

    private void Start() {
        instance = this;

        InitialPosition = transform.position;
        InitialRotation = transform.rotation;

        _textInteractTogglePictureModeContents = TextInteractTogglePictureMode.text;
        _textInteractTakePictureContents = TextInteractTakePicture.text;
        _textControlsContents = TextControls.text;
        TextInteractTogglePictureMode.text = "";
        TextInteractTakePicture.text = "";

        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = pictureSound;
        
        PlayerCamera.depth = 1;  // depth 1 = set as main camera
        AppareilPhotoCamera.depth = 0;
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
        return base.CanInteract() && !DataStore.instance.PickedUpAppareilPhoto;
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
            } else {
                EnableLiveStreamToCameraScreen();  // enable livestream to camera object
                TextInteractTakePicture.text = "";
                TextControls.text = _textControlsContents;
            }
        }
        // take picture in picture mode
        else if (DataStore.instance.PictureMode && Input.GetKeyDown(KeyCode.Space)) {

            // disable UI texts
            TextInteractTakePicture.text = "";
            TextControls.text = "";
            TextInteractTogglePictureMode.text = "";

            // render camera in new texture once and save screenshot
            var renderTexture = new RenderTexture(Screen.width, Screen.height, 24);
            AppareilPhotoCamera.targetTexture = renderTexture;
            AppareilPhotoCamera.Render();

            var screenshot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
            RenderTexture.active = renderTexture;
            screenshot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
            screenshot.Apply();

            // save screenshot and show on displayer
            DataStore.instance.Pictures.Add(screenshot.EncodeToPNG());
            DataStore.instance.PictureIndex = DataStore.instance.Pictures.Count - 1;
            PictureDisplayer.instance.ShowSelectedPicture();

            // put camera back
            AppareilPhotoCamera.targetTexture = null;

            // put UI back
            TextInteractTakePicture.text = _textInteractTakePictureContents;
            TextInteractTogglePictureMode.text = _textInteractTogglePictureModeContents;

            // play sound
            _audioSource.Play();
        }
    }

    private void EnableLiveStreamToCameraScreen() {
        var rect = CameraLiveFeedTarget.rectTransform.rect;
        var renderTexture = new RenderTexture((int)(CameraLiveFeedResolution * rect.width), (int)(CameraLiveFeedResolution * rect.height), 24);
        AppareilPhotoCamera.targetTexture = renderTexture;
        CameraLiveFeedTarget.texture = renderTexture;
    }

}
