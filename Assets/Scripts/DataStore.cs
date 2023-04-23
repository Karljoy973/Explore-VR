using System.Collections.Generic;
using UnityEngine;

public class DataStore : MonoBehaviour {

    public static DataStore instance;
    public void Start() => instance = this;

    public bool PickedUpAppareilPhoto { get; set; }
    public bool PictureMode { get; set; }

    public int LevelIndex { get; set; } = 0;

    public List<byte[]> Pictures { get; } = new();
    public int PictureIndex = 0;
    
    public PopupSelector SelectedPopup { get; set; }

}
