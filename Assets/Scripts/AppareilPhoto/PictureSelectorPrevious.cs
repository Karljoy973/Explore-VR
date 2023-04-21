public class PictureSelectorPrevious : InteractableObject {
    
    public override bool CanInteract() {
        return base.CanInteract() && DataStore.instance.PictureIndex > 0;
    }

    public override void OnInteract() {
        --DataStore.instance.PictureIndex;
        PictureDisplayer.instance.ShowSelectedPicture();
    }
    
}
