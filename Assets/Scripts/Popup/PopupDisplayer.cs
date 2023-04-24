using UnityEngine;
using UnityEngine.UI;

public class PopupDisplayer : MonoBehaviour {

    public static PopupDisplayer instance;
    public void Start() => instance = this;

    [SerializeField] private Image PopupImage;

    public void ShowCurrentPopup() {
        if (DataStore.instance.SelectedPopup == null) {
            PopupImage.enabled = false;
        } else {
            Texture2D texture = DataStore.instance.SelectedPopup.PopupTexture2D;
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            PopupImage.sprite = sprite;
            PopupImage.enabled = true;
        }
    }
    
}
