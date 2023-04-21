public class PictureSelectorNext : InteractableObject {
    
    public override bool CanInteract() {
        return base.CanInteract() && DataStore.instance.PictureIndex < DataStore.instance.Pictures.Count - 1;
    }

    public override void OnInteract() {
        ++DataStore.instance.PictureIndex;
        PictureDisplayer.instance.ShowSelectedPicture();
    }
    
}
