using UnityEngine;

public class DataStore : MonoBehaviour {

    public static DataStore instance;
    public void Start() => instance = this;

    private bool _pickedUpAppareilPhoto;
    public bool PickedUpAppareilPhoto {
        get => _pickedUpAppareilPhoto;
        set {
            _pickedUpAppareilPhoto = value;
            // TODO : put camera in inventory
        }
    }

    private MenuLevel _selectedLevel;
    public MenuLevel SelectedLevel {
        get => _selectedLevel;
        set {
            _selectedLevel = value;
            // TODO : update selected image on door ?
        }
    }

}
