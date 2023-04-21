using UnityEngine;
using UnityEngine.UI;

public class PictureDisplayer : MonoBehaviour {
    
    public static PictureDisplayer instance;
    public void Start() => instance = this;

    public void ShowSelectedPicture() {
        if (DataStore.instance.PictureIndex < 0 || DataStore.instance.PictureIndex >= DataStore.instance.Pictures.Count)
            return;
        
        var texture = new Texture2D(Screen.width, Screen.height);
        texture.LoadImage(DataStore.instance.Pictures[DataStore.instance.PictureIndex]);
        
        var rawImage = GetComponent<RawImage>();
        rawImage.texture = texture;
    }

}
