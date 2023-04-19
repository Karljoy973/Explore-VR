using UnityEngine;

public class Menu : MonoBehaviour {

    public static Menu instance;
    public void Start() => instance = this;

    private MenuLevel _selectedLevel;
    public MenuLevel SelectedLevel {
        get => _selectedLevel;
        set {
            _selectedLevel = value;
            // TODO : update selected image on door ?
        }
    }

}
