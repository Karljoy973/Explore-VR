using UnityEngine;

public class DataStore : MonoBehaviour {

    public static DataStore instance;
    public void Start() => instance = this;

    public bool PickedUpAppareilPhoto { get; set; }
    public bool PictureMode { get; set; }

    private MenuLevel _selectedLevel;
    public MenuLevel SelectedLevel {
        get => _selectedLevel;
        set {
            _selectedLevel = value;
            // TODO : update selected image on door ?
        }
    }

}
