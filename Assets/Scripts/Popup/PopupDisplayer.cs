using System;
using UnityEngine;
using UnityEngine.UI;

public class PopupDisplayer : MonoBehaviour {

    public static PopupDisplayer instance;
    public void Start() => instance = this;

    [SerializeField] private Image PopupImage;

    private DateTime _lastPopupTime;

    public void ShowCurrentPopup() {
        if (DataStore.instance.SelectedPopup == null) {
            PopupImage.enabled = false;
        } else {
            var texture = DataStore.instance.SelectedPopup.PopupTexture2D;
            var sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            PopupImage.sprite = sprite;
            PopupImage.enabled = true;
            _lastPopupTime = DateTime.Now;
        }
    }

    private void Update() {
        if (DataStore.instance.SelectedPopup != null && Input.GetMouseButtonDown(0)) {
            if (DateTime.Now - _lastPopupTime >= TimeSpan.FromSeconds(0.33f)) {
                DataStore.instance.SelectedPopup = null;
                ShowCurrentPopup();
            }
        }
    }
    
}
