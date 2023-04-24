using System;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class PopupDisplayer : MonoBehaviour {

    public static PopupDisplayer instance;
    public void Start() => instance = this;

    [SerializeField] private Image PopupImage;
    [SerializeField] private FirstPersonController Player;

    public DateTime LastPopupUpdate;

    public void UpdatePopup() {
        if (DataStore.instance.SelectedPopup == null) {
            PopupImage.enabled = false;
            Player.enabled = true;
        }
        else {
            var texture = DataStore.instance.SelectedPopup.PopupTexture2D;
            var sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            PopupImage.sprite = sprite;
            PopupImage.enabled = true;
            Player.enabled = false;
        }
        
        LastPopupUpdate = DateTime.Now;
    }

    private void Update() {
        if (DataStore.instance.SelectedPopup != null && Input.GetMouseButtonDown(0)) {
            if (DateTime.Now - LastPopupUpdate >= TimeSpan.FromSeconds(0.25f)) {
                DataStore.instance.SelectedPopup = null;
                UpdatePopup();
            }
        }
    }
    
}
